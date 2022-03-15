It's relatively common to write log files to blobs in a storage account in Azure. Storage in a storage account is cheaper for long term storage, it's easier to control sampling etc. Let's add `Serilog` to our Function App and inject it into our `ILogger` using dependency injection. Use the `Serilog.Sinks.AzureBlobStorage` sink for logging to blobs. We will also use the package `Serilog.Extensions.Logging` to make it easier to add `Serilog` to our dependency injection config. How do you know what packages to add? Lots of googling, luck, and lots of local testing.

We currently already have a storage account for our application that is used by the Function app. All Function apps in Azure require a Storage Account for its internal workings and it's a best practice to avoid using this Storage account for our own custom use cases. Create another storage account where we can store our own logging data.

### Goal
- Infrastructure as Code (IaC)
- Bicep
- DevOps
- Logging

### Steps
1. Add the the nuget packages to the project `GreetingService.API.Function`:
    * Serilog
    * Serilog.Sinks.AzureBlobStorage
    * Serilog.Extensions.Logging
2. Logging to an Azure Storage Account will require a `connection string` to the storage account, this type of data is typically stored in App Settings and can be available in our code using `IConfiguration`. Get a reference to `IConfiguration` in `Startup` using something like this: `var config = builder.GetContext().Configuration;` and then we can get the connection string value with something like this: `config["LoggingStorageAccount"]` assuming the connection string is stored in the config property named `LoggingStorageAccount`
3. Can you figure out how to add the Serilog Logger as a Logging provider in `Startup.cs` using a combination of the info in these links?
    * How to use Serilog.Sinks.AzureBlobStorage: https://github.com/chriswill/serilog-sinks-azureblobstorage
    * How to add Serilog to the Azure Functions Dependency Injection logging: https://github.com/serilog/serilog-extensions-logging
4. Add code to `azure_resources.bicep` to create our new `Storage Account`
    * Copy the bicep code for the existing storage account and update the name
5. Add the `connection string` for the new storage account to `App Settings` in the `Function App` section in the bicep file.
    * Ensure that the setting name is the same as the value we used inside `Startup` class: `LoggingStorageAccount` 
6. Deploy the bicep template to Azure
7. Deploy the code from Visual Studio to the Function App
8. Go to the new storage account in portal.azure.com and verify that everything works as expected
    * A blob should be created with the name we've configured
    * The blob should contain log messages

### Example output
