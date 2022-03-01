using Azure.Messaging.ServiceBus;
using GreetingService.Core.Entities;
using GreetingService.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GreetingService.Infrastructure.MessagingService
{
    public class ServiceBusMessagingService : IMessagingService
    {
        private readonly ServiceBusSender _serviceBusSender;

        public ServiceBusMessagingService(ServiceBusSender serviceBusSender)
        {
            _serviceBusSender = serviceBusSender;
        }

        public async Task SendAsync(Greeting greeting)
        {
            var message = new ServiceBusMessage(JsonSerializer.Serialize(greeting))
            {
                Subject = "greeting"
            };
            await _serviceBusSender.SendMessageAsync(message);
        }
    }
}
