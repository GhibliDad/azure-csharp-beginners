Add the `SQL Server` and `SQL Database` resources to our `Bicep` infrastructure as code file to automate deploy and adding the database `connection string` to the Function App app settings.

### Steps
1. Add a resource of type `Microsoft.Sql/servers@2019-06-01-preview` to Bicep, can you figure out how to configure this resource in Bicep?
    - Use Bicep parameters for setting the values of `administratorLogin` and `administratorLoginPassword`
    - Never store credentials in Bicep since this file is source controlled
2. Add a resource of type `Microsoft.Sql/servers/databases@2019-06-01-preview`
    - Make sure to explicitly configure the `sku` to `Basic` to avoid incurring unnecessary costs in the subscription
3. Also add the db `connection string` to the Function app app settings. A SQL db connection string can be formatted with something like this in Bicep, adapt variable names and values to your bicep file:
    - `'Data Source=tcp:${reference(sqlServer.id).fullyQualifiedDomainName},1433;Initial Catalog=${sqlDbName};User Id=${sqlAdminUser};Password=\'${sqlAdminPassword}\';'`
4. Add a secret to your Github repo containing the `sql admin password` and add it to the `parameters` of the command inside the `YML` pipeline where we deploy the Bicep file
5. Commit changes in the bicep file and push to `main` to trigger deploy
6. Verify that the created resources in Azure are correct and that the db connection string is configured in Function App app settings
7. Verify that the GreetingService in the Function App works and can communicate with the db
    - If the db is deleted and recreated, you will have to run the `EF Core Tools` command `Update-Database -project GreetingService.Infrastructure` to create the db table again
    - If the db is deleted and recreated with a new name, you will need to update the `Environment Variable` connection string again, after update, restart `Visual Studio` for the new `Environment Variable` value to take effect
 

### Example output
