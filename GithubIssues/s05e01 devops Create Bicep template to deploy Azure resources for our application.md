To achieve a high velocity development cycle where new features (code) can be deployed to production, we typically use an automated deploy pipeline that packages our application and deploys it to the target environment. Using Infrastructure as Code we can not only deploy our application code but also all (or large parts) of the cloud infrastructure required to run our application. 

Here are some of the benefits of using Infrastructure as Code:
* Repeatability: Each deploy is exactly the same as opposed to performing the steps manually by a human
* Version controlled infrastructure: The deploy templates are code that should be source controlled which gives us all the benefits of source control (when and why did who change what)
* Quickly deploy new environments or create temporary environments for testing specific features
* etc

Infrastructure as Code for Azure is typically built using one of these languages/solutions:
* ARM templates
* Bicep templates
* Terraform (third party solution)

In this exercise we'll build a Bicep template. Bicep is a `Domain Specific Language (DSL)` and under the hood it's always translated to an `ARM` template before it's executed in Azure. Since Bicep is a `DSL` it also has its own syntax. Bicep (and ARM) is a declarative language which means we declare what we want the results to be and the platform determines the best way to execute it. There is no order or flow that can be debugged, be prepared for a lot of trial and error when working with Bicep templates.

The process for creating Bicep templates might look similar to this:
1. Make a change in the Bicep file
2. Deploy the Bicep file (with Azure CLI or other tool)
3. Check results in portal.azure.com
4. Repeat 

Bicep template:
https://docs.microsoft.com/en-us/azure/azure-resource-manager/bicep/overview 

ARM template:
https://docs.microsoft.com/en-us/azure/azure-resource-manager/templates/overview

Infrastructure as code:
https://docs.microsoft.com/en-us/devops/deliver/what-is-infrastructure-as-code

Work with your assigned `resource group`. Try not to destroy each other's resource groups. Or try to destroy and see what happens, as long as you've discussed with the target resource group owner. :)

We will not focus too much on naming conventions for Azure resources. Different organisations have different naming conventions and there is no right or wrong. You would typically want to adapt the resource names in the Bicep template to follow your organisation's established naming convention.

You also don't need to be able to write a Bicep template by hand from scratch. Almost always possible to copy from a source online or previous projects and adapt to your use case.

You've probably heard the term `DevOps` before. If you ask 5 people what DevOps is you will probably get 5 different responses. For med DevOps means what the name implies: `Development` + `Operations`. Back in the day developers developed applications and handed it over to operations for deployment to production. This created barriers on multiple levels (communication, competence, organisation, responsibility, workflow, etc) between building the application and running the application in production and resulted in memes like this:
<img width="496" alt="image" src="https://user-images.githubusercontent.com/2921523/151510968-8d0444b1-0f8b-48ed-a5c4-3bfa3ce4e3ce.png">

For me, one of the most important parts of DevOps is: 
If you ship it, you run it. If you don't want to debug production in Christmas, don't deploy bad stuff.

Continuous integration, Continuous delivery, test automation, infrastructure as code etc are all part of the DevOps domain.

### Goal
- Infrastructure as Code (IaC)
- Bicep
- DevOps

### Steps
1. Create a new file named `azure_resources.bicep` located in the root of the repo and open it in `VS Code`
2. Install the `Bicep` extension in `VS Code` if it's not already installed
3. Read and follow the steps here on how to create our first Bicep template for our `Function app`: https://markheath.net/post/azure-functions-bicep
    * At least these resources need to be added to the Bicep file: `Function App`, `App Service Plan`, `Storage Account`, `Application Insights`
4. Read and follow the steps here on how to deploy our Bicep template to Azure: https://docs.microsoft.com/en-us/azure/azure-resource-manager/bicep/deploy-cli
    * The most relevant sections are: `Prerequisites`, `Deploy local Bicep file`

### Example output
<img width="1604" alt="image" src="https://user-images.githubusercontent.com/2921523/150154276-40426274-ccfd-4b71-82f4-09f41e6d2be7.png">

Output from Azure CLI command:
```console
PS C:\git\azure-csharp-beginners> az deployment group create -g keen-rg-dev --template-file azure_resources.bicep --parameters appName=keentestdev
C:\git\azure-csharp-beginners\azure_resources.bicep(8,23) : Warning simplify-interpolation: Remove unnecessary string interpolation. [https://aka.ms/bicep/linter/simplify-interpolation]

{
  "id": "/subscriptions/5bb1b2d9-ed37-4ee3-9053-56954eaa90c7/resourceGroups/keen-rg-dev/providers/Microsoft.Resources/deployments/azure_resources",
  "location": null,
  "name": "azure_resources",
  "properties": {
    "correlationId": "15eea888-29a1-4547-baa5-bad5dca6757f",
    "debugSetting": null,
    "dependencies": [
      {
        "dependsOn": [
          {
            "id": "/subscriptions/5bb1b2d9-ed37-4ee3-9053-56954eaa90c7/resourceGroups/keen-rg-dev/providers/Microsoft.Insights/components/keentestdevzppvh642sa7fg",
            "resourceGroup": "keen-rg-dev",
            "resourceName": "keentestdevzppvh642sa7fg",
            "resourceType": "Microsoft.Insights/components"
          },
          {
            "id": "/subscriptions/5bb1b2d9-ed37-4ee3-9053-56954eaa90c7/resourceGroups/keen-rg-dev/providers/Microsoft.Web/serverfarms/keentestdevzppvh642sa7fg",
            "resourceGroup": "keen-rg-dev",
            "resourceName": "keentestdevzppvh642sa7fg",
            "resourceType": "Microsoft.Web/serverfarms"
          },
          {
            "id": "/subscriptions/5bb1b2d9-ed37-4ee3-9053-56954eaa90c7/resourceGroups/keen-rg-dev/providers/Microsoft.Storage/storageAccounts/keentestdezppvh642sa7fg",
            "resourceGroup": "keen-rg-dev",
            "resourceName": "keentestdezppvh642sa7fg",
            "resourceType": "Microsoft.Storage/storageAccounts"
          },
          {
            "actionName": "listKeys",
            "apiVersion": "2019-06-01",
            "id": "/subscriptions/5bb1b2d9-ed37-4ee3-9053-56954eaa90c7/resourceGroups/keen-rg-dev/providers/Microsoft.Storage/storageAccounts/keentestdezppvh642sa7fg",
            "resourceGroup": "keen-rg-dev",
            "resourceName": "keentestdezppvh642sa7fg",
            "resourceType": "Microsoft.Storage/storageAccounts"
          }
        ],
        "id": "/subscriptions/5bb1b2d9-ed37-4ee3-9053-56954eaa90c7/resourceGroups/keen-rg-dev/providers/Microsoft.Web/sites/keentestdev",
        "resourceGroup": "keen-rg-dev",
        "resourceName": "keentestdev",
        "resourceType": "Microsoft.Web/sites"
      }
    ],
    "duration": "PT1M58.6494457S",
    "error": null,
    "mode": "Incremental",
    "onErrorDeployment": null,
    "outputResources": [
      {
        "id": "/subscriptions/5bb1b2d9-ed37-4ee3-9053-56954eaa90c7/resourceGroups/keen-rg-dev/providers/Microsoft.Insights/components/keentestdevzppvh642sa7fg",
        "resourceGroup": "keen-rg-dev"
      },
      {
        "id": "/subscriptions/5bb1b2d9-ed37-4ee3-9053-56954eaa90c7/resourceGroups/keen-rg-dev/providers/Microsoft.Storage/storageAccounts/keentestdezppvh642sa7fg",
        "resourceGroup": "keen-rg-dev"
      },
      {
        "id": "/subscriptions/5bb1b2d9-ed37-4ee3-9053-56954eaa90c7/resourceGroups/keen-rg-dev/providers/Microsoft.Web/serverfarms/keentestdevzppvh642sa7fg",
        "resourceGroup": "keen-rg-dev"
      },
      {
        "id": "/subscriptions/5bb1b2d9-ed37-4ee3-9053-56954eaa90c7/resourceGroups/keen-rg-dev/providers/Microsoft.Web/sites/keentestdev",
        "resourceGroup": "keen-rg-dev"
      }
    ],
    "outputs": null,
    "parameters": {
      "appName": {
        "type": "String",
        "value": "keentestdev"
      },
      "location": {
        "type": "String",
        "value": "westeurope"
      }
    },
    "parametersLink": null,
    "providers": [
      {
        "id": null,
        "namespace": "Microsoft.Storage",
        "registrationPolicy": null,
        "registrationState": null,
        "resourceTypes": [
          {
            "aliases": null,
            "apiProfiles": null,
            "apiVersions": null,
            "capabilities": null,
            "defaultApiVersion": null,
            "locationMappings": null,
            "locations": [
              "westeurope"
            ],
            "properties": null,
            "resourceType": "storageAccounts"
          }
        ]
      },
      {
        "id": null,
        "namespace": "Microsoft.Insights",
        "registrationPolicy": null,
        "registrationState": null,
        "resourceTypes": [
          {
            "aliases": null,
            "apiProfiles": null,
            "apiVersions": null,
            "capabilities": null,
            "defaultApiVersion": null,
            "locationMappings": null,
            "locations": [
              "westeurope"
            ],
            "properties": null,
            "resourceType": "components"
          }
        ]
      },
      {
        "id": null,
        "namespace": "Microsoft.Web",
        "registrationPolicy": null,
        "registrationState": null,
        "resourceTypes": [
          {
            "aliases": null,
            "apiProfiles": null,
            "apiVersions": null,
            "capabilities": null,
            "defaultApiVersion": null,
            "locationMappings": null,
            "locations": [
              "westeurope"
            ],
            "properties": null,
            "resourceType": "serverfarms"
          },
          {
            "aliases": null,
            "apiProfiles": null,
            "apiVersions": null,
            "capabilities": null,
            "defaultApiVersion": null,
            "locationMappings": null,
            "locations": [
              "westeurope"
            ],
            "properties": null,
            "resourceType": "sites"
          }
        ]
      }
    ],
    "provisioningState": "Succeeded",
    "templateHash": "11496304622936773372",
    "templateLink": null,
    "timestamp": "2022-01-19T14:41:57.728444+00:00",
    "validatedResources": null
  },
  "resourceGroup": "keen-rg-dev",
  "tags": null,
  "type": "Microsoft.Resources/deployments"
}
PS C:\git\azure-csharp-beginners>
```