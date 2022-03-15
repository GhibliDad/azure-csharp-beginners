We need to be able to run our application in different environments, one `test` environment and one `production` environment. Make it configurable (without hardcoding) which `IGreetingWriter` is used and which path the `FileGreetingWriter` writes to.

### App Settings
App settings (or app configuration) is used all the time in all programming languages and platforms. 

Examples where app settings might be used: 
When you build a web application, the `url` might be an app setting so that different environments can listen on different addresses (e.g. `mywebapp.mycompany.com`, `mywebapp-test.mycompany.com`, `mywebapp-dev.mycompany.com`).

Typically a database `connection string` (adress + credentials to a database) is configured in app settings (instead of hard coding it in the code). This makes it possible for a test environment to connect to a test database and a production environment to connect to a production database. Only need to change the app setting when deploying to a different environment.

File paths are also common to have in app settings to make it possible for the user to change where a certain file should be located on disk.

### Deserialization
Objects, properties, variables in our application code are stored in a platform specific binary format that only our application can understand and have access to. Whenever we want to import data from outside into our application or export data from our application to the outside the data needs to be translated into a format that the outside world understands. Common formats are `JSON`, `XML`, `CSV`. 

We `serialize` our internal objects into an external format (JSON/XML/CSV etc) when exporting data from our application. 

We `deserialize` data from external formats (JSON/XML/CSV etc) to our internal objects when importing data into our application.

`Serialization` and `deserialization` makes it possible to communicate with other applications regardless of the programmining language the other applications are written in as long as both applications can use the same message format (JSON/XML/CSV etc).

More info about serialization here:
https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/serialization/
https://en.wikipedia.org/wiki/Serialization

### Goal
- App configuration
- Deserialization

### Steps
1. Add the following nuget packages with either `Nuget Package Manager` or `dotnet cli` with these commands:
    - `dotnet add package Microsoft.Extensions.Configuration.Binder`
    - `dotnet add package Microsoft.Extensions.Configuration.Json`
    - `dotnet add package Microsoft.Extensions.Configuration.EnvironmentVariables`
2. Create a new file named: `appsettings.json` containing the following `json`
```json
{
    "Settings": {
        "GreetingWriterClassName": "FileGreetingWriter",
        "GreetingWriterOutputFilePath": "greetings.log"
    }
}
```
3. Add this code block to `GreetingConsoleApp.csproj`
```xml
  <ItemGroup>
    <Content Include="appsettings.json">
      <CopyToOutputDirectory>Always</CopyToOutputDirectory>
    </Content>
  </ItemGroup>
```
4. Create a new file named `Settings.cs` containing the properties:
  - `string GreetingWriterClassName`
  - `string GreetingWriterOutputFilePath `
5. Add a constructor to `Program`:
```c#
    static Program()
    {
        //Lets read our configuration from appsettings.json
        // Build a config object, using JSON provider.
        IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")                        //appsettings.json is our settings file
            .Build();

        // Get values from the config given their key and their target type.
        _settings = config.GetRequiredSection("Settings").Get<Settings>();      //Get the section named "Settings" in our settings file and deserialize it to an object named _settings of type Settings
    }
```
6. Implement a new method: `public static IGreetingWriter CreateGreetingWriter()` that respects the configured value in `appsettings.json` and returns a writer of the configured type
7. Test it out by writing with the `IGreetingWriter` return from `CreateGreetingWriter()`
8. Implement logic for `FileGreetingWriter` to use the configured filename in `GreetingWriterOutputFilePath ` in `appsettings.json`
  - Copy app config logic to constructor in class `FileGreetingWriter`
