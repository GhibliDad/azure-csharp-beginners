using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingService.Contracts
{
    public class InvoiceRow
    {
        public string description { get; set; }
        public double count { get; set; }
        public decimal amount { get; set; }
    }
}
