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

namespace GreetingService.API.Function.Users
{
    public class PutUser
    {
        private readonly ILogger<PutUser> _logger;
        private readonly IAuthHandler _authHandler;
        private readonly IMessagingService _messagingService;

        public PutUser(ILogger<PutUser> log, IAuthHandler authHandler, IMessagingService messagingService)
        {
            _logger = log;
            _authHandler = authHandler;
            _messagingService = messagingService;
        }

        [FunctionName("PutUser")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "User" })]
        [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(User), Description = "The OK response")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NotFound, Description = "Not found")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "user")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            if (!await _authHandler.IsAuthorizedAsync(req))
                return new UnauthorizedResult();

            User user;
            try
            {
                user = JsonSerializer.Deserialize<User>(req.Body, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }

            await _messagingService.SendAsync(user, Core.Enums.MessagingServiceSubject.UpdateUser);

            return new AcceptedResult();        //accepted status code means: We've received your request and it will be processed in due time, good response for asynchronous flows like this endpoints now has become when using IMessagingService.SendAsync()
        }
    }
}

