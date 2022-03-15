Azure Service Bus is a messaging solution with queues and topics that can be used to decouple applications or parts of the same application. There are two main parts of Azure Service Bus:
* Queues
* Topics & Subscriptions

Queues are First In First Out (FIFO) queues and provides us with an intermediate storage solution for messages before processing.
<img width="777" alt="image" src="https://user-images.githubusercontent.com/2921523/155997101-0957111c-81d7-4629-88bc-dd0e431b0267.png">

Topics & Subscriptions makes it possible for us to implement the publish/subscribe pattern. We can send one message to the `topic` and then create one or more `subscriptions` that has filters that matches certain properties on a message and each `subscription` that matches a `message` will get a copy of the `message`. This allows us to isolate application logic using different subscriptions. 
<img width="775" alt="image" src="https://user-images.githubusercontent.com/2921523/155997127-11b0ba2d-fcbd-4482-9e41-8b4114015b6d.png">

We will focus on `Topics` & `Subscriptions` in Azure Service Bus in this season.

Check out the documentation here:
https://docs.microsoft.com/en-us/azure/service-bus-messaging/service-bus-messaging-overview

Also check out this blog post that covers many areas of Azure Service Bus:
https://www.serverless360.com/azure-service-bus

### Goal
Use Service Bus as a messaging backbone for `POST Greeting` API endpoint. When receiving a `POST Greeting` request, instead of writing directly to the db, send the `Greeting` object to a Service Bus `topic`. This adds a layer between receiving our API to writing to DB which gives us the following benefits (in exchange for some added complexity):
* Our API can still receive `POST Greeting` calls even if the database is down (e.g. incident, planned maintenance, etc)
* Our API can handle much greater volume of `POST Greeting` requests since sending to Service Bus `topic` is faster than inserting data to db.
* If we want to add for when a `Greeting` is created we can easily add a `subscription` to the `topic` and `Azure Function` to process that logic without mixing different logic in our application code 

### Steps
1. In the Azure Portal: Create an Azure Service Bus namespace named: `keen-sb-dev` in your resource group (replace `keen` with your own name) 
    - Choose the `Standard` tier (we need this tier to be able to create `topics`)
<img width="851" alt="image" src="https://user-images.githubusercontent.com/2921523/156057864-98faf6c9-43e3-438a-a578-a9419d636066.png">

2. Create a new `topic` in the namespace and name it `main` or something similarly generic. We send all messages through this topic
<img width="456" alt="image" src="https://user-images.githubusercontent.com/2921523/156058812-2f92e018-4d34-4e35-8545-01f5505f5654.png">

3. In the `main` topic, create a new subscription named `greeting_create`
<img width="619" alt="image" src="https://user-images.githubusercontent.com/2921523/156060952-57f2e71e-eac6-4f4b-9c67-7d0e06827f8c.png">

4. In the `greeting_create` subscription, remove the rule named `$Default` and create a new rule named `subject` and configure it like this. This rule will route all messages with the property `subject=NewGreeting` to this subscription: 
<img width="1836" alt="image" src="https://user-images.githubusercontent.com/2921523/156251525-2f110390-d503-4c1f-8863-fa1105fa0821.png">


5. Install the nuget package `Azure.Messaging.ServiceBus` in `GreetingService.Infrastructure` 

6. Create a new interface in `GreetingService.Core` named `IMessagingService` containing the method `Task SendAsync<T>(T message, string subject)` 
  - This is a generic method that we will use to send any object to Service Bus

7. Create a new class in `GreetingService.Infrastructure` named `ServiceBusMessagingService` and implement `IMessagingService`
    - Check out documentation for the nuget package: https://github.com/Azure/azure-sdk-for-net/blob/Azure.Messaging.ServiceBus_7.6.0/sdk/servicebus/Azure.Messaging.ServiceBus/README.md
    - Our goal is to send the `Greeting` object to the Service Bus `main` topic when receiving a `POST Greeting`
    - The message we send must have property named `subject` have the string value `NewGreeting` to match the filter of our `greeting_create` subscription
    - According to the documentation, the related clients should be reused for the life time of the application

8. Update `POST Greeting` Function to send the greeting to service bus with the `IMessagingService` instead of  inserting it to the database with `IGreetingRepository`

9. Create a Service Bus triggered `Azure Function` named `SbCreateGreeting` and make it trigger on messages from the `greeting_create` `subscription` in Service Bus and insert the `Greeting` to the db using `IGreetingRepository`
<img width="797" alt="image" src="https://user-images.githubusercontent.com/2921523/156187733-f462d04b-e5b1-424f-a0b1-2eb986f7ebe3.png">

10. Test it locally, set break points in `PostGreeting` and `SbCreateGreeting` functions and verify with Postman that new `Greetings` are flowing: `HTTP Triggered Function` -> `Service Bus Topic` -> `Service Bus Subscription` -> `Service Bus Triggered Function` -> `Azure SQL`

### Example output
One message in the topic `greeting_create` before our Service Bus triggered Function `SbCreateGreeting` receives it from the subscription:
<img width="1374" alt="image" src="https://user-images.githubusercontent.com/2921523/156243639-caf00e9c-015f-450f-996d-e2fe921b2973.png">
