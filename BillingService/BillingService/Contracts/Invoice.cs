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
    [OpenApiExample(typeof(InvoiceExample))]
    public class Invoice
    {
        public Guid id { get; set; }
        public int year { get; set; }
        public int month { get; set; }
        public string customer { get; set; }
        public double amount { get; set; }
        public string currency { get; set; }

        private IEnumerable<InvoiceRow> _invoiceRows;
        public IEnumerable<InvoiceRow> invoice_rows
        {
            get
            {
                return _invoiceRows ?? Enumerable.Empty<InvoiceRow>();
            }
            set
            {
                _invoiceRows = value;
            }
        }
    }

    public class InvoiceExample : OpenApiExample<Invoice>
    {
        public override IOpenApiExample<Invoice> Build(NamingStrategy namingStrategy = null)
        {
            var request = new Invoice
            {
                id = Guid.NewGuid(),
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

            Examples.Add(OpenApiExampleResolver.Resolve("InvoieExample", request, namingStrategy));
            return this;
        }
    }
}
