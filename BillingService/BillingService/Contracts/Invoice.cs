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
    [OpenApiExample(typeof(InvoiceExample))]
    public class Invoice
    {
        public Guid id { get; set; }
        public int year { get; set; }
        public int month { get; set; }
        public string customer { get; set; }
        public decimal amount { get; set; }
        public string currency { get; set; }
        public IEnumerable<InvoiceRow> invoice_rows { get; set; }
    }

    public class InvoiceExample : OpenApiExample<Invoice>
    {
        public override IOpenApiExample<Invoice> Build(NamingStrategy namingStrategy = null)
        {
            var request = new Invoice
            {
                id = Guid.NewGuid(),
                amount = 123,
                currency = "sek",
                customer = "user@domain.com",
                invoice_rows = new InvoiceRow[]
                {
                    new InvoiceRow{ amount = 12, count = 2, description = "Hello there!"},
                },
                month = 1,
                year = 2022,
            };

            Examples.Add(OpenApiExampleResolver.Resolve("InvoieExample", request, namingStrategy));
            return this;
        }
    }
}
