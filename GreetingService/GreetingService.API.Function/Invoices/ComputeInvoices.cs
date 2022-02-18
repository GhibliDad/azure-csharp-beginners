using System;
using System.Linq;
using System.Threading.Tasks;
using GreetingService.Core.Entities;
using GreetingService.Core.Interfaces;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace GreetingService.API.Function.Invoices
{
    public class ComputeInvoices
    {
        private readonly IInvoiceService _invoiceService;
        private readonly IGreetingRepository _greetingRepository;
        private readonly IUserService _userService;

        public ComputeInvoices(IInvoiceService invoiceService, IGreetingRepository greetingRepository, IUserService userService)
        {
            _invoiceService = invoiceService;
            _greetingRepository = greetingRepository;
            _userService = userService;
        }

        [FunctionName("ComputeInvoices")]
        public async Task Run([TimerTrigger("*/30 * * * * *")] TimerInfo myTimer, ILogger log)      //cron expression: */30 * * * * * means execute every 30 seconds
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            //First we get all greetings. Here we might have added a new method to only get greetings for a certain month if we want to avoid the overhead of processing all greetings every time
            var greetings = await _greetingRepository.GetAsync();

            //Group greetings on From, Year, Month, we want to create one invoice per combination of From, Year, Month
            var greetingsGroupedByInvoice = greetings.GroupBy(x => new { x.From, x.Timestamp.Year, x.Timestamp.Month });

            //Iterate over each group of From, Year, Month
            foreach (var group in greetingsGroupedByInvoice)
            {
                var user = await _userService.GetUserAsync(group.Key.From);     //get user details
                var invoice = new Invoice
                {
                    Greetings = group,
                    Month = group.Key.Month,
                    Year = group.Key.Year,
                    Sender = user,
                };

                await _invoiceService.CreateOrUpdateInvoiceAsync(invoice);
            }
        }
    }
}
