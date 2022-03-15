Automate deployment of Azure Service Bus resources in Azure and automatically add the connection string to Azure Functions app settings.

### Goal
* Azure Service Bus
* CI/CD
* Infrastructure As Code

### Steps
1. Add the required Azure Service Bus resources to Bicep:
    - Service Bus Namespace
    - Service Bus Topic `main`
    - Service Bus Subscription `greeting_create`
    - Service Bus Subscription Rule `subject`

2. Add the Service Bus namespace connection string to Function app `App Settings`


### Example output
<img width="1837" alt="image" src="https://user-images.githubusercontent.com/2921523/156248065-72ae654e-808e-4fc8-8401-a492a759bb80.png">
