Update remaining POST and PUT endpoints to also use Azure Service Bus:
    - `PutGreeting`
    - `PostUser`
    - `PutUser`

### Goal
* Azure Service Bus
* Refactoring

### Steps
1. Add required `subscriptions` to the `main` topic following the model used in previous exercises
2. Update the endpoints to send messages with `IMessagingService` and create corresponding Azure Functions with Service Bus Trigger to get messages and write to db
3. Set break points in each Service Bus triggered Function and verify that all is connected correctly

### Example output
<img width="1873" alt="image" src="https://user-images.githubusercontent.com/2921523/156256825-88170665-e53e-4243-ad2b-6c9f8d685c05.png">
