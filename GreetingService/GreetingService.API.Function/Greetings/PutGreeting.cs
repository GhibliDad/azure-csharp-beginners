using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using GreetingService.API.Function.Authentication;
using GreetingService.Core.Entities;
using GreetingService.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace GreetingService.API.Function.Greetings
{
    public class PutGreeting
    {
        private readonly ILogger<PutGreeting> _logger;
        private readonly IAuthHandler _authHandler;
        private readonly IMessagingService _messagingService;

        public PutGreeting(ILogger<PutGreeting> log, IAuthHandler authHandler, IMessagingService messagingService)
        {
            _logger = log;
            _authHandler = authHandler;
            _messagingService = messagingService;
        }

        [FunctionName("PutGreeting")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "Greeting" })]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.Accepted, Description = "Accepted")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "greeting")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            if (!await _authHandler.IsAuthorizedAsync(req))
                return new UnauthorizedResult();

            Greeting greeting;
            try
            {
                var body = await req.ReadAsStringAsync();
                greeting = JsonSerializer.Deserialize<Greeting>(body, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }

            try
            {
                await _messagingService.SendAsync(greeting, Core.Enums.MessagingServiceSubject.UpdateGreeting);
            }
            catch
            {
                return new NotFoundResult();
            }

            return new AcceptedResult();
        }
    }
}

