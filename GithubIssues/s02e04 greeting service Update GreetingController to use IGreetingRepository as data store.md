Wire everything up so that our API Controller reads and writes to our `IGreetingRepository`. We will touch on the concept of `Dependency Injection` in this step.  

### Goal
- REST API
- ASP.NET web api
- Dependency Injection

### Steps
1. Add a contructor to `GreetingController` that takes an `IGreetingRepository` as parameter and save this to a `private readonly field` 
2. Update each method to use the corresponding method in `IGreetingRepository`
3. What happens when we run our application? 
4. We are missing some `dependency injection` config
    - Need to wire up so that our application uses the implementation `FileGreetingRepository` as `IGreetingRepository`
    - Need to add config value for `FileRepositoryFilePath` to `appsettings.json` and read this config value on startup
Add this code to `Program.cs`
```charp
builder.Services.AddScoped<IGreetingRepository, FileGreetingRepository>(c => {
    var config = c.GetService<IConfiguration>();
    return new FileGreetingRepository(config["FileRepositoryFilePath"]);
});
```



### Example output
![image](https://user-images.githubusercontent.com/2921523/145891011-98e8e505-13ca-4c95-a69d-6f152e8994f9.png)
