Adapt `GreetingService.API` to Azure Functions and deploy it to a new `Function App` in Azure. We'll do this by creating a new project in the `GreetingService` solution named `GreetingService.API.Function` and have this side by side with our existing projects so that we can choose what to use and also compare the different projects.  

### Goal
- Local Azure Functions development

### Steps
1. Create a new project using the `Azure Functions` template and name it `GreetingService.API.Function`
    - Be sure to choose `.Net 6` in the runtime drop down when creating the project
2. Select the `Http trigger with OpenAPI` template and set Authorization level to `Anonymous`
3. Run the function app project and call the generated `Function1` endpoint to test it
4. First we need to set up dependency injection to be able to use all our required interfaces without breaking our project dependency rules. Do this by creating a new file named `Startup.cs` in `GreetingService.API.Function`
5. Follow this guide to setup dependency injection in Azure Functions:
    - https://docs.microsoft.com/en-us/azure/azure-functions/functions-dotnet-dependency-injection
`Startup.cs` should something like this:
```csharp
using GreetingService.Core.Interfaces;
using GreetingService.Infrastructure;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(GreetingService.API.Function.Startup))]
namespace GreetingService.API.Function
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddHttpClient();
            
            builder.Services.AddScoped<IGreetingRepository, FileGreetingRepository>(c =>
            {
                var config = c.GetService<IConfiguration>();
                return new FileGreetingRepository(config["FileRepositoryFilePath"]);
            });

            builder.Services.AddScoped<IUserService, AppSettingsUserService>();
        }
    }
}
```
6. Rename `Function1.cs` to `GetGreetings.cs` (also update class name)
7. Update the `Run` method to only accept `Get` requests (remove `Post`)
8. Replace the `Route = null` with `Route = "greeting"`
9. Add logic for getting greetings from `IGreetingRepository`
    - Also add the `FileRepositoryFilePath` configuration to `local.settings.json`. This file is only used locally and should not be committed to source control
10. Create `Functions` (one per file) for the remaining endpoints in our API
    - `GetGreeting`
    - `PostGreeting`
    - `PutGreeting`
11. Ensure relative uri:s are the same as in our `ASP.NET Core` controller
12. Return correct http status codes for various scenarios


### Example output
![image](https://user-images.githubusercontent.com/2921523/146557635-8b7582c7-9b03-44e6-8e90-e4329796a90d.png)
