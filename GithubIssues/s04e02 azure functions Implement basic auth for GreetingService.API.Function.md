Azure Functions does not support Basic Authentication out of the box. There are built in Function Keys that can be used for simpler scenarios. A common scenario is to implement custom authentication logic. Let's try to implement a simple basic auth support in our Function app.

### Goal
- Basic Autentication
- Azure Functions

### Steps
1. Create a new folder `Authentication` in the function project
2. Add the files `IAuthHandler` and `BasicAuthHander`
    - Add the method `public bool IsAuthorized(HttpRequest req);` to the interface and implement it in `BasicAuthHandler`
    - Copy and adapt the logic from our previous implementation in `GreetingService.API`
3. Add `IAuthHandler` to the constructors of our Functions
4. Configure `IAuthHandler` and `BasicAuthHandler` in dependency injection in `Startup.cs`
5. Test it
    - Use the file `local.settings.json` (create it if in the function project if it doesn't exist) to add app settings to our app (e.g. file path, user, password for basic auth etc) 

### Example output
![image](https://user-images.githubusercontent.com/2921523/147000248-2f049f4f-a31c-4503-a957-5ce7068f7168.png)
