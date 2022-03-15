using GreetingService.Core.Exceptions;
using GreetingService.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace GreetingService.Core.Entities
{
    public class Greeting
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Message { get; set; }

        private string _from;
        public string From 
        {
            get
            {
                return _from;
            }

            set
            {
                if (!InputValidationHelper.IsValidEmail(value))
                    throw new InvalidEmailException($"{value} is not a valid email");

                _from = value;
            }
        }
        
        private string _to;
        public string To
        {
            get 
            {
                return _to;
            }

            set 
            {
                if (!InputValidationHelper.IsValidEmail(value))
                    throw new InvalidEmailException($"{value} is not a valid email");

                _to = value;
            }
        }
        public DateTime Timestamp { get; set;  } = DateTime.Now;
    }
}
