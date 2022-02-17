using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreetingService.Core.Entities
{
    public class Invoice
    {
        public int? Id { get; set; }
        public User Sender { get; set; }
        public IEnumerable<Greeting> Greetings { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }
        public decimal AmountPerGreeting { get; set; } = 21;

        private decimal _totalAmount;
        public decimal TotalAmount 
        {
            get
            {
                if (Greetings == null)
                    return 0;

                return Greetings.Count() * AmountPerGreeting;
            }
            set
            {
                _totalAmount = value;
            }
        }
        public string Currency { get; set; } = "kr";
    }
}
