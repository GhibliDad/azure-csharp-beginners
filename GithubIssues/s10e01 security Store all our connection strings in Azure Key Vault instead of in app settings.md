`Azure Key Vault` is a secure store for sensitive information such as passwords, connection strings, api keys, certificates, etc. So, instead of storing these in local.settings.json and in Azure Functions App Settings, we will store these in Azure Key Vault and add Key Vault.

Unfortunately we won't be able to replace all app settings with secrets in Key Vault but we can replace many, especially secrets for third party resources outside of Azure or Microsoft.

This also gives us the added benefit that we can relatively easy point our `local.settings.json` to the Key Vault of a different environment (e.g. from `dev` to `acceptans testing`) and debug using all secrets for that environment.

However to access the Key Vault also requires permissions but instead of accessing the Key Vault using a `Connection String` or `API Key` of some sort, we instead use a feature in `Azure Active Directory` named `Managed Identity`. Notice at the end of this exercise that your Function App does not have a connection string or api key to the Key Vault in App Settings. It will authenticate with Managed Identity.

More info about Managed Identity:
https://docs.microsoft.com/en-us/azure/active-directory/managed-identities-azure-resources/overview

More info about Azure Key Vault:
https://docs.microsoft.com/en-us/azure/key-vault/general/basic-concepts

Best practices for Key Vault:
https://docs.microsoft.com/en-us/azure/key-vault/general/best-practices

Azure Functions and Key Vault integration:
https://docs.microsoft.com/en-us/azure/app-service/app-service-key-vault-references

What we want to achieve in this exercise is to user Azure Key Vault as a provider to our `IConfiguration` so that we can get our connection strings and other values from Key Vault using the same syntax as we currently do, for example: 
```c#
var connectionString = config["GreetingDbConnectionString"];
```

We will configure this both in Azure Portal and in Bicep.

### Goal
* Azure Key Vault
* Managed Identity
* App Configuration
* Secret management

### Steps
1. Create a Key Vault resource in Azure Portal
<img width="815" alt="image" src="https://user-images.githubusercontent.com/2921523/157245336-5bb3f9f4-692b-4fbe-9a93-68c3c1945fe1.png">

<img width="1828" alt="image" src="https://user-images.githubusercontent.com/2921523/157245477-b94d056f-1583-4088-bbf9-8112a89ab13e.png">

<img width="774" alt="image" src="https://user-images.githubusercontent.com/2921523/157245534-df056dcf-001e-4e41-bcd3-2ef1100fb83d.png">

<img width="589" alt="image" src="https://user-images.githubusercontent.com/2921523/157245567-b0ae66f4-d206-4491-a3cb-feb396033ca5.png">

2. Navigate to `Access Policies` and ensure that your user account has at least the `Get`, `List`, `Set` permissions for `Secrets`

3. Navigate to `Secrets` and add all connection strings and secrets from `local.settings.json` using the same key and value for each. 

4. Find out what nuget packages you need and how to add Key Vault as a provider to `IConfiguration` in Azure Functions.

5. Comment out all connection strings and secrets from `local.settings.json` (or wherever you stored them) and run your application locally in Visual Studio and verify that all app settings are available and that the application still works

6. You will receive this warning when running your application locally: 
```console
[2022-03-09T09:48:01.502Z] The Functions scale controller may not scale the following functions correctly because some configuration values were modified in an external startup class.
[2022-03-09T09:48:01.508Z]   Function 'ConvertGreetingToCsv' uses the modified key(s): LoggingStorageAccount
[2022-03-09T09:48:01.512Z]   Function 'SbBeginUserApproval' uses the modified key(s): ServiceBusConnectionString
[2022-03-09T09:48:01.516Z]   Function 'SbComputeInvoiceForGreeting' uses the modified key(s): ServiceBusConnectionString
[2022-03-09T09:48:01.520Z]   Function 'SbCreateGreeting' uses the modified key(s): ServiceBusConnectionString
[2022-03-09T09:48:01.527Z]   Function 'SbCreateUser' uses the modified key(s): ServiceBusConnectionString
[2022-03-09T09:48:01.531Z]   Function 'SbUpdateGreeting' uses the modified key(s): ServiceBusConnectionString
[2022-03-09T09:48:01.534Z]   Function 'SbUpdateUser' uses the modified key(s): ServiceBusConnectionString
```

7. This is an issue inherent in the `Consumption` plan in Azure Functions. Azure Functions running the `Premium` plans shouldn't have this issue. This issue only exists for `connection strings` that we use in our `Triggers`: `LoggingStorageAccount` and `ServiceBusConnectionString` since we're using the `Blob Trigger` and `Service Bus Trigger`. To handle this for the Consumption plan we can use `Azure Functions Key Vault references` for these two app settings, more info here. Try to figure out how to configure these two app settings using this documentation:
    - `https://docs.microsoft.com/en-us/azure/app-service/app-service-key-vault-references`
    - The warnings might still appear, this should be fine if you followed this documentation

8. Your application should now work locally, test run it in Visual Studio

9. Now we need to enable `Managed Identity` for the Function app to make it possible for it to access the Key Vault
    - Navigate to your Function App in Azure Portal
    - Navigate to `Identity`
    - Set `Status` to `On` for `System Assigned` identity 
    - This will give this Function app an identity that we can assign permissions to

10. Navigate to your Key Vault in Azure Portal

11. Navigate to `Access Policies`

12. Add `Access Policy`
    - Choose the secret permissions: `Get`, `List`
    - On `Select Principal` find the identity of your function app
![image](https://user-images.githubusercontent.com/2921523/157513277-e83f56e7-c4d6-46d4-9a84-66f6f62a1a16.png)

![image](https://user-images.githubusercontent.com/2921523/157513325-7a86959e-5a03-4e70-98bd-55369eca6372.png)

13. Now we want to configure our Bicep to do this configuration for us so that we can deploy our application (try the `Export` template of a resource if you are unsure what values to use)
    - Add Key Vault resource to bicep
        - If `tenantId` is needed create and use this variable: `var asurgentTenantId = '9583541d-47a0-4deb-9e14-541050ac8bc1'`
        - Also add Access Policy for the Identity of your Function app (don't hard code the GUID, get this using a built in features of Bicep)
    - Enable Managed Identity (System Assigned) on Function app
    - Update Function App App Settings accordingly
        - Leave the storage account connection string for Azure Functions as is, these are needed for the Function app itself to work and cannot be referenced in Azure Key Vault (for now at least): `WEBSITE_CONTENTAZUREFILECONNECTIONSTRING` & `AzureWebJobsStorage`



### Example output
![image](https://user-images.githubusercontent.com/2921523/157525403-744097b6-fc11-47f8-9e23-25d54cb2b3f3.png)

![image](https://user-images.githubusercontent.com/2921523/157525313-cb43c8c5-7c50-4321-bb81-a52de4620236.png)

![image](https://user-images.githubusercontent.com/2921523/157525498-6d8d5004-1763-450f-af9c-3ab25c94be74.png)



