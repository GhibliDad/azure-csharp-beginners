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
    public class UpdateInvoice
    {
        private readonly ILogger<UpdateInvoice> _logger;
        private readonly BlobContainerClient _blobContainerClient;

        public UpdateInvoice(ILogger<UpdateInvoice> log, IConfiguration configuration)
        {
            _logger = log;
            _blobContainerClient = new BlobContainerClient(configuration["AzureWebJobsStorage"], "invoice");
            _blobContainerClient.CreateIfNotExists();
        }

        [FunctionName("UpdateInvoice")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "invoice" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(UpdateInvoiceResponse), Description = "The OK response")]
        [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(UpdateInvoiceRequest), Description = "The create invoice request", Required = true, Example = typeof(UpdateInvoiceRequestExample))]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Admin, "put", Route = "invoice")] HttpRequest req)
        {
            UpdateInvoiceRequest request;
            try
            {
                var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                request = JsonConvert.DeserializeObject<UpdateInvoiceRequest>(requestBody);
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

            var blobs = _blobContainerClient.GetBlobsAsync();
            var blob = await blobs.FirstOrDefaultAsync(x => x.Name.EndsWith(request.id.ToString()));

            var blobClient = _blobContainerClient.GetBlobClient(blob.Name);
            var content = await blobClient.DownloadContentAsync();
            var invoice = content.Value.Content.ToObjectFromJson<Invoice>();
            
            invoice.amount = request.amount;
            invoice.currency = request.currency;
            invoice.invoice_rows = request.invoice_rows;

            await blobClient.DeleteAsync();
            await blobClient.UploadAsync(new BinaryData(invoice, new System.Text.Json.JsonSerializerOptions { WriteIndented = true }));
            _logger.LogInformation("Successfully updated invoice {id} for {customer}, {year}, {month}", invoice.id, invoice.customer, invoice.year, invoice.month);

            return new OkObjectResult(new UpdateInvoiceResponse { id = invoice.id });
        }
    }
}

