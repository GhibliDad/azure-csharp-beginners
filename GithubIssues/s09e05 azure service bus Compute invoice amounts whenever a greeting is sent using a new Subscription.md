We want to compute billing info in the `GreetingService` more in real time instead of waiting for the timer trigger. Add a `subscription` to the Service Bus `topic` named `greeting_compute_billing`

Using publish/subscribe pattern we can add new features to our application while keeping the logic separate in the application. Posting a Greeting should now trigger two actions: 
1. Insert the Greeting to the db
2. Compute invoice for the sender

This gives us some benefits:
* Exception handling is easier, getting an error in one of the actions should not affect the other
* Updating logic in one action should not affect the other
* It's more clear what the role of each action does
* We get a more clear separation of concern in our methods and classes
* Adding more features is easy, just add another subscription
* Removing features is easy, just remove the subscription

An alternative is to add this logic for both actions to the `PostGreeting` function or to the `GreetingRepository.CreateAsync()` method but there are some disadvantages with this:
* We would need to mix different logic in the same method which increases the complexity of that function
* There is no clear separation of concern
* Updating one forces us to also update the other since the logic is contained in the same method
* Which one should we execute first? What happens if the first one fails, do we still execute the second? We need more complex exception handling
* Adding more features is harder, need to add more code to existing methods
* Removing features is harder, code to be removed is embedded in the same methods as other features


### Goal
* Azure Service Bus
* Publish/Subscribe

### Steps
1. Add another subscription named `greeting_compute_billing` in the `Bicep` file with the same `rule` as `greeting_create` and deploy it to Azure 
2. Create a new Service Bus triggered Function named `SbComputeInvoiceForGreeting` that triggers when a message is added to the `greeting_compute_invoice` `subscription`
3. Implement logic to update the invoice for the `From` user 
4. You can keep the Timer triggered Function `ComputeInvoices` if you want to but update it to trigger only once per day or less often. This Function can be kept for redundancy for computing invoices

### Example output
```json
{
    "id": 5,
    "sender": {
        "firstName": "John",
        "lastName": "Doe",
        "email": "user1@domain.com",
        "password": "summer2022",
        "created": "2022-02-16T21:32:12.5895414",
        "modified": "2022-02-16T21:32:12.5895443"
    },
    "greetings": [
        {
            "id": "3d9df875-b61a-4357-be6a-252073dbc8cc",
            "message": "Hello",
            "from": "user1@domain.com",
            "to": "user1@domain.com",
            "timestamp": "2022-03-01T21:18:16.6621257"
        },
        {
            "id": "998f742f-daa0-4a25-b19d-53ba533dc02e",
            "message": "Hello",
            "from": "user1@domain.com",
            "to": "user1@domain.com",
            "timestamp": "2022-03-01T23:01:55.0381611"
        },
        {
            "id": "18b3328b-2b75-4d92-94de-9c1b7b4e3e6e",
            "message": "Hello",
            "from": "user1@domain.com",
            "to": "user1@domain.com",
            "timestamp": "2022-03-01T22:28:20.76047"
        },
        {
            "id": "248a2274-3943-46ad-94e2-9e6c82ffc44f",
            "message": "Hello",
            "from": "user1@domain.com",
            "to": "user1@domain.com",
            "timestamp": "2022-03-04T10:34:10.4787624"
        },
        {
            "id": "ad05b3e1-478a-4669-9968-b0c5d6a70d45",
            "message": "Hello",
            "from": "user1@domain.com",
            "to": "user1@domain.com",
            "timestamp": "2022-03-04T11:38:08.9790413"
        },
        {
            "id": "286ae089-73a1-4bda-8bba-ca98ef14257d",
            "message": "Hello",
            "from": "user1@domain.com",
            "to": "user1@domain.com",
            "timestamp": "2022-03-01T21:17:18.6765466"
        },
        {
            "id": "7955135c-d7d6-47ef-8046-da704864185c",
            "message": "Hello",
            "from": "user1@domain.com",
            "to": "user1@domain.com",
            "timestamp": "2022-03-01T21:18:21.7431552"
        }
    ],
    "year": 2022,
    "month": 3,
    "amountPerGreeting": 21.00,
    "totalAmount": 147.00,
    "currency": "kr"
}
```

<img width="695" alt="image" src="https://user-images.githubusercontent.com/2921523/156756541-69baf604-963b-4748-aa72-ada51c4be7dd.png">

