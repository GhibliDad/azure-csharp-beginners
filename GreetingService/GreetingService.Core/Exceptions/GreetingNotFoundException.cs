using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreetingService.Core.Exceptions
{
    public class GreetingNotFoundException : Exception
    {
        public GreetingNotFoundException(string? message) : base(message)
        {
        }
    }
}
