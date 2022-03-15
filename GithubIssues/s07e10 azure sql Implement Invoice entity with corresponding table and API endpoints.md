Sending `Greetings` now costs money. Add a new entity named `Invoice` with corresponding table in the db and API endpoints for getting invoice data.

An `Invoice` should have the following properties:
* ID (use `int` for invoice id, easier for humans to read)
* `User`
* List of `Greetings`
* Invoice year
* Invoice month
* Cost per `Greeting`
* Total cost
* Currency

We need the following invoice api endpoints:

* GET invoice one user and an invoice period
    - path: `/invoice/{year}/{month}/{email}`

* GET invoices for all users for an invoice period
    - path: `/invoice/{year}/{month}`

We'll also add a new `Timer` triggered function to help us calculate the invoice amounts every 5 minutes.

### Steps
1. Create a new class named `Invoice` and put in the `Entities` folder in `GreetingService.Core`
    - Add the relevant properties to the class
2. Add a DbSet<Invoice> to `GreetingDbContext`
3. Run the `EF Core` tool command to create a migration and update the database (you might have to remove rows from `Greeting` table before update-database will succeed because of the new constraints in the migration)
    - `Add-Migration CreatedInvoiceTable -project GreetingService.Infrastructure`
    - `update-database -project GreetingService.Infrastructure`
4. Create a new interface named `IInvoiceService` in `Interfaces` folder in `GreetingService.Core` with these methods:
```c#
        public Task<IEnumerable<Invoice>> GetInvoices(int year, int month);
        public Task<Invoice> GetInvoice(int year, int month, string email);
        public Task CreateOrUpdateInvoice(Invoice invoice);
```
5. Create a new class named `SqlInvoiceService` in `GreetingService.Infrastructure` that implements `IInvoiceService` and implement all methods in the interface
    - `CreateOrUpdateInvoiceAsync` method should as the name implies insert a new row if the invoice for the user and period does not already exist or update the existing row if it already exists
    - Don't forget to add `SqlInvoiceService` and `IInvoiceService` to dependency injection
6. Create a new `Timer` triggered Azure Function and make it get all existing `Greetings` and create corresponding `Invoices`
    - Timer triggers use `Cron` expressions to trigger on a schedule. `Cron` is a `Unix` feature and is widely used to trigger jobs on a schedule.
    - Use the cron expression `0 */5 * * * *` to make it trigger every 5 minutes
7. Create 2 new `http` triggered Azure Functions for getting 
    - all invoices for a period (year, month)
    - invoice for a user for a period (year, month, email)



### Example output

```json
[
    {
        "id": 1,
        "sender": {
            "firstName": "keen",
            "lastName": "fann",
            "email": "keen.fann@asurgent.se",
            "password": "summer2022",
            "created": "2022-02-16T12:25:15.45",
            "modified": "2022-02-16T12:25:15.45"
        },
        "greetings": [
            {
                "id": "b0206193-64e1-41ab-9875-3512a882d9cc",
                "message": "Hello",
                "from": "keen.fann@asurgent.se",
                "to": "user1@domain.com",
                "timestamp": "2022-02-17T14:44:14.5559713"
            },
            {
                "id": "b250b60b-ae73-473c-9cd8-42d8a58491c7",
                "message": "Hello",
                "from": "keen.fann@asurgent.se",
                "to": "user2@domain.com",
                "timestamp": "2022-02-16T21:33:37.0704423"
            },
            {
                "id": "a5d836f3-b07d-42a8-b0d8-6bbf18f96fa3",
                "message": "Hello",
                "from": "keen.fann@asurgent.se",
                "to": "user1@domain.com",
                "timestamp": "2022-02-17T14:44:42.2665264"
            },
            {
                "id": "aa111a61-6ff2-4bdf-9cdf-7be5452e4005",
                "message": "Hello",
                "from": "keen.fann@asurgent.se",
                "to": "user1@domain.com",
                "timestamp": "2022-02-17T14:44:37.443868"
            }
        ],
        "year": 2022,
        "month": 2,
        "amountPerGreeting": 21.00,
        "totalAmount": 84.00,
        "currency": "kr"
    },
    {
        "id": 2,
        "sender": {
            "firstName": "Kalle",
            "lastName": "Anka",
            "email": "user2@domain.com",
            "password": "summer2022",
            "created": "2022-02-16T21:32:02.6199317",
            "modified": "2022-02-16T21:32:02.6200349"
        },
        "greetings": [
            {
                "id": "6532ff4f-3018-4409-839a-a5cd5afd340a",
                "message": "Hello",
                "from": "user2@domain.com",
                "to": "user1@domain.com",
                "timestamp": "2022-02-17T14:44:55.8911901"
            },
            {
                "id": "7ff4213a-9170-4514-9223-a756d0606de8",
                "message": "Hello",
                "from": "user2@domain.com",
                "to": "user1@domain.com",
                "timestamp": "2022-02-17T14:44:58.9043604"
            }
        ],
        "year": 2022,
        "month": 2,
        "amountPerGreeting": 21.00,
        "totalAmount": 42.00,
        "currency": "kr"
    },
    {
        "id": 3,
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
                "id": "c1a70890-74f7-4d48-a267-4c23b69fc9bc",
                "message": "Hello",
                "from": "user1@domain.com",
                "to": "user1@domain.com",
                "timestamp": "2022-02-17T14:45:04.3848868"
            },
            {
                "id": "76206155-52bf-4618-b7a9-60041d868788",
                "message": "Hello",
                "from": "user1@domain.com",
                "to": "user1@domain.com",
                "timestamp": "2022-02-17T14:45:13.679017"
            },
            {
                "id": "83c0391b-1eed-4b64-8e8e-9d823d95ac50",
                "message": "Hello",
                "from": "user1@domain.com",
                "to": "user1@domain.com",
                "timestamp": "2022-02-17T14:45:16.5993054"
            },
            {
                "id": "5fb176cb-f14e-406f-93c8-b2e68918cbf7",
                "message": "Hello",
                "from": "user1@domain.com",
                "to": "user1@domain.com",
                "timestamp": "2022-02-17T14:45:07.7668532"
            },
            {
                "id": "6e48fa2d-cce8-488c-86b3-ff4690a9ee0a",
                "message": "Hello",
                "from": "user1@domain.com",
                "to": "user1@domain.com",
                "timestamp": "2022-02-17T14:45:10.4687251"
            }
        ],
        "year": 2022,
        "month": 2,
        "amountPerGreeting": 21.00,
        "totalAmount": 105.00,
        "currency": "kr"
    }
]
```