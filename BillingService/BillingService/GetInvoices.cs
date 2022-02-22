using System;
using System.Collections.Generic;
using System.IO;
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
    public class GetInvoices
    {
        private readonly ILogger<GetInvoices> _logger;
        private readonly BlobContainerClient _blobContainerClient;

        public GetInvoices(ILogger<GetInvoices> log, IConfiguration configuration)
        {
            _logger = log;
            _blobContainerClient = new BlobContainerClient(configuration["AzureWebJobsStorage"], "invoice");
            _blobContainerClient.CreateIfNotExists();
        }

        [FunctionName("GetInvoices")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "invoice" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<Invoice>), Description = "The OK response", Example = typeof(InvoiceExample))]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Admin, "get", Route = "invoice/{customer}")] HttpRequest req, string customer)
        {
            var invoices = new List<Invoice>();
            var blobs = _blobContainerClient.GetBlobsAsync(prefix: customer);
            await foreach (var blob in blobs)
            {
                var blobClient = _blobContainerClient.GetBlobClient(blob.Name);
                var content = await blobClient.DownloadContentAsync();
                var invoice = content.Value.Content.ToObjectFromJson<Invoice>(new System.Text.Json.JsonSerializerOptions { WriteIndented = true });
                invoices.Add(invoice);
            }

            return new OkObjectResult(invoices);
        }
    }
}

