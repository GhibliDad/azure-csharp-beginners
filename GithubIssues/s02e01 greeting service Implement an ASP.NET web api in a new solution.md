Time to move on from our `console app` to build a web api that interacts with `Greetings`. We'll start fresh with a new `Visual Studio Solution` and will use concepts we've covered so far together with new `ASP.NET` concepts to build a web api. We'll place different types of logic in different projects this time.

Web API logic will be located in `GreetingService.API`.
Core and Business logic will be located in `GreetingService.Core`
Infrastructural logic (storage, network, etc) will be located in `GreetingService.Infrastructure` 

This project setup is called `Clean Architecture`:
https://docs.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures

https://www.youtube.com/watch?v=lkmvnjypENw

https://ardalis.com/clean-architecture-asp-net-core/

We'll begin by setting up our Visual Studio solution and projects.

### Goal
- REST API
- ASP.NET web api
- Project structure
- Postman, Swagger UI, or similar tool for calling REST API:s

### Steps
1. Create a new folder: `GreetingService`
2. Create a blank solution in this folder named `GreetingService` 
3. Create a new project named `GreetingService.API` using the template `ASP.NET Core Web API`
    - Uncheck `Configure for HTTPS` in the wizard
4. Create 2 more projects using the template `Class Library` named:
    - `GreetingService.Core`
    - `GreetingService.Infrastructure`
5. Take some time to explore the files that are created with the template:
    - `Program.cs` is the entry point of the application
    - `WeatherForecast.cs` is an object that is returned in the API
    - `WeatherForecastController.cs` is a controller containing logic for an endpoint in the API
6. Press `F5` to run the application in Visual Studio and try calling the `weatherforecast` endpoint
    -  To call the endpoint, send an `GET` request to `http://localhost:[port]/WeatherForecast` using a browser, Swagger UI, or a tool like Postman. The port number is random and will be different every time a project is created. The port number can be seen in the console when starting the application:
<img width="889" alt="image" src="https://user-images.githubusercontent.com/2921523/145539838-c81d93a3-2df9-4cc9-b71e-81c2784b922b.png">

7. If you encounter HTTP Response `415 Unsupported Media Type` response from the API, try adding the Request Header: `Content-Type` with the value `application/json`

### Example output
Postman:
![image](https://user-images.githubusercontent.com/2921523/145540442-65e5f56e-8231-403e-802e-7c9348d0366c.png)

Swagger UI:
![image](https://user-images.githubusercontent.com/2921523/145558776-bc92ce84-dfda-4b3d-bb5f-5a80e1c634b7.png)

Browser:
![image](https://user-images.githubusercontent.com/2921523/145558947-6ca9075d-d789-40cd-8f21-4eff54d68056.png)
