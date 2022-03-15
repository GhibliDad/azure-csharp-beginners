We currently store some app config in `appsettings.json` file. This file is source controlled which makes it not great for storing credentials and other secrets such as `connections strings` to databases, `api keys` to external apis etc. 

Add support for storing configuration values inside the Application Settings of the App Service.

Create a new implementation of IUserService that gets valid usernames and passwords from Application Settings in App Service.

Application Settings in an App Service are automatically mapped to `environment variables` that we can access from our code. And `environment variables` are automatically mapped by default in `ASP.NET Core` dependency injection.

### Goal
- Environment variables
- App Service app config

### Steps
1. Create a new class `AppSettingsUserService` that implements `IUserService`
2. Register this class as the implementation of `IUserService` in our dependency injection config (`Program.cs`)
3. Add a dependency to `IConfiguration` to `AppSettingsUserService` by creating a constructor that takes an `IConfiguration` as a parameter.
4. Get the valid usernames and passwords from our `IConfiguration` and validate the credentials.
5. Test locally by adding a username and password in `appsettings.json` but be sure to never store real credentials in this file if it's committed to source control
6. Deploy to Azure App Service, add an app setting and try it out
    - Updating app settings restarts the application, storing user credentials like this 

### Example output
<img width="1853" alt="image" src="https://user-images.githubusercontent.com/2921523/146450462-fb2b9d2a-76af-4ce4-97d4-e425dd9a1096.png">
