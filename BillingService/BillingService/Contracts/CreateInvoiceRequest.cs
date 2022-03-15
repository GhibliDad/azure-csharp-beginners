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
    [OpenApiExample(typeof(CreateInvoiceRequestExample))]
    public class CreateInvoiceRequest
    {
        public int year { get; set; }
        public int month { get; set; }
        public string customer { get; set; }
        public double amount { get; set; }
        public string currency { get; set; }
        public IEnumerable<InvoiceRow> invoice_rows { get; set; }
    }

    public class CreateInvoiceRequestExample : OpenApiExample<CreateInvoiceRequest>
    {
        public override IOpenApiExample<CreateInvoiceRequest> Build(NamingStrategy namingStrategy = null)
        {
            var request = new CreateInvoiceRequest
            {
                amount = 12,
                currency = "sek",
                customer = "user@domain.com",
                invoice_rows = new InvoiceRow[]
                {
                    new InvoiceRow{ amount = 12, count = 1, description = "Hello there!"},
                },
                month = 1,
                year = 2022,
            };

            Examples.Add(OpenApiExampleResolver.Resolve("CreateInvoiceRequestExample", request, namingStrategy));
            return this;
        }
    }
}
