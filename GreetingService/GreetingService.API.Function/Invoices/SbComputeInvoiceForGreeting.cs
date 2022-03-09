using System;
using System.Linq;
using System.Threading.Tasks;
using GreetingService.Core.Entities;
using GreetingService.Core.Exceptions;
using GreetingService.Core.Interfaces;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace GreetingService.API.Function.Greetings
{
    public class SbComputeInvoiceForGreeting
    {
        private readonly ILogger<SbComputeInvoiceForGreeting> _logger;
        private readonly IGreetingRepository _greetingRepository;
        private readonly IInvoiceService _invoiceService;
        private readonly IUserService _userService;

        public SbComputeInvoiceForGreeting(ILogger<SbComputeInvoiceForGreeting> log, IGreetingRepository greetingRepository, IInvoiceService invoiceService, IUserService userService)
        {
            _logger = log;
            _greetingRepository = greetingRepository;
            _invoiceService = invoiceService;
            _userService = userService;
        }

        [FunctionName("SbComputeInvoiceForGreeting")]
        public async Task Run([ServiceBusTrigger("main", "greeting_compute_invoice", Connection = "ServiceBusConnectionString")] Greeting greeting)
        {
            _logger.LogInformation($"C# ServiceBus topic trigger function processed message: {greeting}");

            try
            {
                var invoice = await _invoiceService.GetInvoiceAsync(greeting.Timestamp.Year, greeting.Timestamp.Month, greeting.From);          //This method returns null if invoice not found
                var user = await _userService.GetUserAsync(greeting.From);

                if (invoice == null)                                                        //Invoice does not exist, create a new invoice
                {
                    try
                    {
                        invoice = new Invoice
                        {
                            Month = greeting.Timestamp.Month,
                            Year = greeting.Timestamp.Year,
                            Sender = user,
                        };
                        await _invoiceService.CreateOrUpdateInvoiceAsync(invoice);

                        invoice = await _invoiceService.GetInvoiceAsync(greeting.Timestamp.Year, greeting.Timestamp.Month, greeting.From);
                        invoice.Greetings = invoice.Greetings.Append(greeting).ToList();
                        await _invoiceService.CreateOrUpdateInvoiceAsync(invoice);
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(e, "Failed to create new invoice for Greeting {id}", greeting.Id);
                        throw;
                    }
                }
                else if (!invoice.Greetings.Any(x => x.Id == greeting.Id))                  //Invoice is not null (it exists) and it does not already contain this Greeting, update existing invoice
                {
                    try
                    {
                        invoice.Greetings = invoice.Greetings.Append(greeting).ToList();
                        await _invoiceService.CreateOrUpdateInvoiceAsync(invoice);
                    }
                    catch (Exception e)
                    {
                        _logger.LogError(e, "Failed to update invoice {id} with new Greeting {greetingId}", invoice.Id, greeting.Id);
                        throw;
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed compute invoice for new greeting {id}", greeting.Id);
                throw;
            }
        }
    }
}
