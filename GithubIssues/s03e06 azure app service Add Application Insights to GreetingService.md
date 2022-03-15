Add monitoring to our application to get insights into performance, requests, errors etc. Configure Application Insights. Configuring logging to work as expected requires a lot of trial and error and depends a lot on sdk, framework, runtime etc.

### Goal
- Application Insights

### Steps
1. Right click on the project `GreetingService.API` and select `Configure Application Insights...` 
2. To the option `...with connection to Azure`, we want to monitor our Azure app
3. Click on the `+` button to add a new Application Insights instance
4. Use the name `[firstname]-appinsights-dev`
5. Choose your resource group
6. Choose `West Europe` as location
7. Click create
8. Finish the wizard with the default options in the remaining steps
9. Deploy the application from Visual Studio
10. Open your instance of Application Insights and go to Live View
11. Send some requests to `GreetingService` and verify that the requests are visible in Live View
    - Are errors visible?
12. Is it possible to configure what we log or don't log?
    - It's not always trivial to get it exactly as we want it in the provided methods in different frameworks such as `ASP.NET Core`. Sometime we need to implement our own logic to log exactly what we require
13. Add `ILogger` to `AppSettingsUserService` and log if user is valid or not

### Example output
<img width="1609" alt="image" src="https://user-images.githubusercontent.com/2921523/146454110-75b57857-4a0f-43c1-98b6-42cff3117967.png">

<img width="1859" alt="image" src="https://user-images.githubusercontent.com/2921523/146529022-8c292320-96a5-42f5-873e-74604d9a72fc.png">

<img width="1872" alt="image" src="https://user-images.githubusercontent.com/2921523/146529131-2baf6cb3-cc07-4577-a90d-0ed92167d7a9.png">


<img width="1870" alt="image" src="https://user-images.githubusercontent.com/2921523/146529157-01bc5d58-a97a-4522-999f-9b3a76d843ac.png">

<img width="1876" alt="image" src="https://user-images.githubusercontent.com/2921523/146529188-ee92984a-22c1-4636-957b-f6e0b4511427.png">

<img width="619" alt="image" src="https://user-images.githubusercontent.com/2921523/146529878-ac60344a-82c7-451a-bb37-433f1ac8eb16.png">

<img width="648" alt="image" src="https://user-images.githubusercontent.com/2921523/146531543-80244075-516a-4a75-8990-b5f54af902e7.png">
