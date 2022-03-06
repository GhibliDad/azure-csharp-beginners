using GreetingService.API.Function.Authentication;
using GreetingService.Core.Interfaces;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System;
using Serilog;
using GreetingService.Infrastructure.GreetingRepository;
using GreetingService.Infrastructure.UserService;
using GreetingService.Infrastructure;
using Microsoft.EntityFrameworkCore;
using GreetingService.Infrastructure.InvoiceService;
using Microsoft.Extensions.Azure;
using Azure.Messaging.ServiceBus;
using GreetingService.Infrastructure.MessagingService;
using GreetingService.Infrastructure.ApprovalService;

[assembly: FunctionsStartup(typeof(GreetingService.API.Function.Startup))]
namespace GreetingService.API.Function
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var config = builder.GetContext().Configuration;        //Get all configured app settings in an IConfiguration like this

            builder.Services.AddHttpClient();       //this registers both HttpClient and IHttpClientFactory in dependency injection
            builder.Services.AddLogging();

            //Create a Serilog logger and register it as a logger
            //Get the Azure Storage Account connection string from our IConfiguration
            builder.Services.AddLogging(c =>
            {
                var connectionString = config["LoggingStorageAccount"];
                if (string.IsNullOrWhiteSpace(connectionString))
                    return;

                var logName = $"{Assembly.GetCallingAssembly().GetName().Name}.log";
                var logger = new LoggerConfiguration()
                                    .WriteTo.AzureBlobStorage(connectionString,
                                                              restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Information,
                                                              storageFileName: "{yyyy}/{MM}/{dd}/" + logName,
                                                              outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level:u3}] [{SourceContext}] {Message}{NewLine}{Exception}")
                                    .CreateLogger();

                c.AddSerilog(logger, true);
            });

            builder.Services.AddScoped<IGreetingRepository, SqlGreetingRepository>();
            builder.Services.AddScoped<IUserService, SqlUserService>();
            builder.Services.AddScoped<IAuthHandler, BasicAuthHandler>();
            builder.Services.AddScoped<IInvoiceService, SqlInvoiceService>();
            builder.Services.AddScoped<IMessagingService, ServiceBusMessagingService>();
            builder.Services.AddScoped<IApprovalService, TeamsApprovalService>();

            builder.Services.AddDbContext<GreetingDbContext>(options =>
            {
                options.UseSqlServer(config["GreetingDbConnectionString"]);     //make sure that the "GreetingDbConnectionString" app setting contains the connection string value
            });

            //The we only need ServiceBusSender to be able to send messages to Service Bus. This class is thread safe and should be reused for the lifetime of the application - register it as singleton to reuse the same instance in all places we use this class
            //more info here: https://docs.microsoft.com/en-us/azure/service-bus-messaging/service-bus-performance-improvements?tabs=net-standard-sdk-2#reusing-factories-and-clients
            builder.Services.AddSingleton(c =>
            {
                var serviceBusClient = new ServiceBusClient(config["ServiceBusConnectionString"]);      //remember to add this connection to the application configuration
                return serviceBusClient.CreateSender("main");
            });
        }
    }
}
