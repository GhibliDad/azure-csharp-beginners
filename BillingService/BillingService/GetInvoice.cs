using System;
using System.Collections.Generic;
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
    public class GetInvoice
    {
        private readonly ILogger<GetInvoice> _logger;
        private readonly BlobContainerClient _blobContainerClient;

        public GetInvoice(ILogger<GetInvoice> log, IConfiguration configuration)
        {
            _logger = log;
            _blobContainerClient = new BlobContainerClient(configuration["AzureWebJobsStorage"], "invoice");
            _blobContainerClient.CreateIfNotExists();
        }

        [FunctionName("GetInvoice")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "invoice" })]
        [OpenApiSecurity("function_key", SecuritySchemeType.ApiKey, Name = "code", In = OpenApiSecurityLocationType.Query)]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Invoice), Description = "The OK response", Example = typeof(InvoiceExample))]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Admin, "get", Route = "invoice/{customer}/{year}/{month}")] HttpRequest req, string customer, int year, int month)
        {
            var blobs = _blobContainerClient.GetBlobsAsync(prefix: customer);
            var blob = await blobs.FirstOrDefaultAsync(x => x.Name.StartsWith($"{customer}/{year}.{month}"));
            if (blob == null)
                return new NotFoundResult();

            var blobClient = _blobContainerClient.GetBlobClient(blob.Name);
            var content = await blobClient.DownloadContentAsync();
            var invoice = content.Value.Content.ToObjectFromJson<Invoice>();
            return new OkObjectResult(invoice);
        }
    }
}

