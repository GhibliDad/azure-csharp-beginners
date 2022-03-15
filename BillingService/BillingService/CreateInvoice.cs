using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace BillingService
{
    public class CreateInvoice
    {
        private readonly ILogger<CreateInvoice> _logger;
        private readonly BlobContainerClient _blobContainerClient;

        public CreateInvoice(ILogger<CreateInvoice> log, IConfiguration configuration)
        {
            _logger = log;
            _blobContainerClient = new BlobContainerClient(configuration["AzureWebJobsStorage"], "invoice");
            _blobContainerClient.CreateIfNotExists();
        }

        [FunctionName("CreateInvoice")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "invoice" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(CreateInvoiceResponse), Description = "The OK response")]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(CreateInvoiceRequest), Description = "The create invoice request", Required = true, Example = typeof(CreateInvoiceRequestExample))]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Admin, "post", Route = "invoice")] HttpRequest req)
        {
            CreateInvoiceRequest request;
            try
            {
                var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                request = JsonConvert.DeserializeObject<CreateInvoiceRequest>(requestBody);
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }

            var invoiceRowAmountSum = request.invoice_rows.Sum(x => x.count * x.amount);
            if (request.amount != invoiceRowAmountSum)
            {
                return new BadRequestObjectResult($"Invoice amount: {request.amount} does not match invoice rows sum: {invoiceRowAmountSum}");
            }

            var invoice = new Invoice
            {
                amount = request.amount,
                currency = request.currency,
                customer = request.customer,
                id = Guid.NewGuid(),
                invoice_rows = request.invoice_rows,
                month = request.month,
                year = request.year,
            };

            var blobClient = _blobContainerClient.GetBlobClient($"{invoice.customer}/{invoice.year}.{invoice.month}.{invoice.id}");
            await blobClient.UploadAsync(new BinaryData(invoice, new System.Text.Json.JsonSerializerOptions { WriteIndented = true }));
            _logger.LogInformation("Successfully created invoice {id} for {customer}, {year}, {month}", invoice.id, invoice.customer, invoice.year, invoice.month);

            return new OkObjectResult(new CreateInvoiceResponse { id = invoice.id });
        }
    }
}

