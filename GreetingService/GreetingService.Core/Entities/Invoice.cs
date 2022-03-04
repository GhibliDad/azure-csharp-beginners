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

        /// <summary>
        /// A relationship to User
        /// </summary>
        public User Sender { get; set; }

        /// <summary>
        /// A relationship to Greeting
        /// </summary>
        public IEnumerable<Greeting> Greetings { get; set; }
        public int Year { get; set; }
        public int Month { get; set; }

        /// <summary>
        /// Default value = 21
        /// </summary>
        public decimal AmountPerGreeting { get; set; } = 21;

        private decimal _totalAmount;

        /// <summary>
        /// This is computed from Greetings.Count() * AmountPerGreeting
        /// </summary>
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

        /// <summary>
        /// Default value = "sek"
        /// </summary>
        public string Currency { get; set; } = "sek";
    }
}
