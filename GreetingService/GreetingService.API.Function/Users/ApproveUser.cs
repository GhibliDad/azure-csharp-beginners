using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using GreetingService.API.Function.Authentication;
using GreetingService.Core.Entities;
using GreetingService.Core.Exceptions;
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
    public class ApproveUser
    {
        private readonly ILogger<ApproveUser> _logger;
        private readonly IUserService _userService;

        public ApproveUser(ILogger<ApproveUser> log, IUserService userService)
        {
            _logger = log;
            _userService = userService;
        }

        [FunctionName("ApproveUser")]
        [OpenApiOperation(operationId: "Run", tags: new[] { "User" })]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.Accepted, Description = "Request accepted")]
        [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.NotFound, Description = "Not found")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "user/approve/{code}")] HttpRequest req, string code)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            try
            {
                await _userService.ApproveUserAsync(code);
            }
            catch (UserNotFoundException e)
            {
                return new NotFoundObjectResult(e.Message);
            }

            return new AcceptedResult();        //accepted status code means: We've received your request and it will be processed in due time, good response for asynchronous flows like this endpoints now has become when using IMessagingService.SendAsync()
        }
    }
}

