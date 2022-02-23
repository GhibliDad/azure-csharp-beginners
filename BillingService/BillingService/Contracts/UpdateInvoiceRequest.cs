using BillingService.Contracts;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Resolvers;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingService
{
    [OpenApiExample(typeof(UpdateInvoiceRequestExample))]
    public class UpdateInvoiceRequest
    {
        public Guid id { get; set; }
        public decimal amount { get; set; }
        public string currency { get; set; }
        public IEnumerable<InvoiceRow> invoice_rows { get; set; }
    }

    public class UpdateInvoiceRequestExample : OpenApiExample<UpdateInvoiceRequest>
    {
        public override IOpenApiExample<UpdateInvoiceRequest> Build(NamingStrategy namingStrategy = null)
        {
            var request = new UpdateInvoiceRequest
            {
                id = Guid.NewGuid(),
                amount = 12,
                currency = "sek",
                invoice_rows = new InvoiceRow[]
                {
                    new InvoiceRow{ amount = 12, count = 1, description = "Hello there!"},
                },
            };

            Examples.Add(OpenApiExampleResolver.Resolve("UpdateInvoiceRequestExample", request, namingStrategy));
            return this;
        }
    }
}
