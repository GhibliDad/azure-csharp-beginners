It's time to build an integration between our `GreetingService` and another application. An integration between applications is basically a solution (like a Logic App or another application) that communicates and uses data from both applications in some way. 

Build a Logic App that fetches invoices from the `GreetingService` and sends them to a `BillingService`. 

The logical workflow should look something like this:
1. Trigger by timer
2. Call `GreetingService` and get invoices for the current month
3. Call `BillingService` and get all invoice for each user and month
4. If an invoice already exists for a user and month -> Update the invoice in `BillingService`
5. If no invoice exists for a user and month -> Create the invoice in `BillingService`

`BillingService` is a new service that we haven't implemented in any previous exercises. As an option, try to implement this service from scratch. Alternatively use this service as a target for sending Invoices to:
https://greeting-billing-service-function-dev.azurewebsites.net/api/swagger/ui#/invoice/Run

### Steps
1. (Optional) Implement the `BillingService` from scratch in a new solution and deploy it to Azure using things you've learned in all the previous exercises
    - Needs HTTP endpoints for get, create, update of invoices
    - Needs a persistent storage (e.g. blob store, table store, sql)
    - Needs some sort of authentication (Basic Auth, Azure Function keys etc)
    - Can skip CI/CD pipeline with Bicep with this one, publish from Visual Studio 
 
Example of `Invoice` for the `BillingService`
```json
[
    {
        "id": "1fc4d869-e061-4e86-948f-3a5d80943756",
        "year": 2022,
        "month": 1,
        "customer": "user@domain.com",
        "amount": 13.0,
        "currency": "sek",
        "invoice_rows": [
            {
                "description": "Hello there!",
                "count": 1.0,
                "amount": 13.0
            }
        ]
    },
    {
        "id": "5d83652f-11d9-4b3a-b21c-b9a331ecee5d",
        "year": 2022,
        "month": 2,
        "customer": "user@domain.com",
        "amount": 13.0,
        "currency": "sek",
        "invoice_rows": [
            {
                "description": "Hello there!",
                "count": 1.0,
                "amount": 13.0
            }
        ]
    }
]
```

2. Copy the file `keen-la-send-weather-greeting.json` to a new file named `keen-la-send-greeting-invoice-to-billing-service.json` to create a new Logic App in the `LogicApp` project (remember to change `keen` to your own name)
    - Also copy the `keen-la-send-weather-greeting.parameters.json` to a new file named `keen-la-send-greeting-invoice-to-billing-service.parameters.json`
    - This Logic App will need a new parameter for the `GreetingService` api credential, open the `keen-la-send-greeting-invoice-to-billing-service.json` and add another `SecureString` parameter and name it `billingServicePassword`. This parameter needs to be configured in a couple places in the ARM template, look how the `password` parameter is configured and follow that pattern. Also rename the `password` parameter to `greetingServicePassword` to make the purpose of the parameter more obvious
    -  Open the file `keen-la-send-weather-greeting.parameters.json` and update the `logicAppName` parameter to `keen-la-send-greeting-invoice-to-billing-service` 

3. Right click on the new file and open with `Open with Logic App Designer`
4. Remove the `Get current weather` and `HTTP` steps
5. Add a new `HTTP` step and configure it to call the `GetInvoices` endpoint
![image](https://user-images.githubusercontent.com/2921523/155411927-75b32663-dee9-48c9-b2a5-09df5d2dd5f3.png)

6. To be able to use the HTTP Response we first need to parse the JSON using the `Parse JSON` action
    - Use the `Use sample payload to generate schema` function to generate a json schema from our HTTP Response 
![image](https://user-images.githubusercontent.com/2921523/155412063-0696b78d-3b6c-4833-a5d2-98894b099ff0.png)

7. Add a `Foreach` action to iterate over each invoice from the `GreetingService`
    - Use the `Body` from the `Parse JSON` action
![image](https://user-images.githubusercontent.com/2921523/155412418-df3657da-4e9b-4af0-9a5e-551be81310aa.png)

8. To be able to use properties from each individual `invoice` we need to parse this as well using another `Parse JSON` action
![image](https://user-images.githubusercontent.com/2921523/155412569-c7f6b00d-a00b-4aba-bc2a-e1f990d0ab83.png)

9. Then we need to transform the `greetings` array from the invoices from `GreetingService` into the `invoice_rows` array in the request to the `BillingService`. We can to this with the `Select` action.
![image](https://user-images.githubusercontent.com/2921523/155413540-442d1a00-a4e3-4620-8bdf-53b33a0660c8.png)

10. Use properties from our previous steps to construct an HTTP call to the `BillingService` to get existing invoices for the user and month (if you built your own `BillingService` your endpoints might differ)
![image](https://user-images.githubusercontent.com/2921523/155412713-1a097efb-58e4-41ae-8c8c-62d540dbbb13.png)

11. Use a `Condition` shape to branch into two paths depending on if the call to `BillingService` returned `200 OK` or not
![image](https://user-images.githubusercontent.com/2921523/155412962-b9643346-7bba-4020-9a67-1e9d6066a595.png)

12. To make sure the workflow continues even if `BillingService` returns an error code such as `404 NOT FOUND`, click on the `...` on the `Condition` action and select `Configure run after` and check `is successful` and `has failed`
![image](https://user-images.githubusercontent.com/2921523/155413219-c90ece67-9890-4b74-b26c-710b23afc4bf.png)

13. In the `True` path, we first need to parse the response body from the `BillingService` using a `Parse JSON` action again
![image](https://user-images.githubusercontent.com/2921523/155413368-1371d269-2e85-445d-b090-cde08e01fb9f.png)

14. Then we construct the `HTTP PUT` request to `BillingService` for updating the invoice
![image](https://user-images.githubusercontent.com/2921523/155413650-203d0388-70e6-4141-9e7a-071a3949e8c2.png)

15. In the `False` path, we only need to construct an `HTTP POST` request to `BillingService` for creating a new invoice
<img width="723" alt="image" src="https://user-images.githubusercontent.com/2921523/155415123-03e61f55-8b0c-4ab7-9bc2-92202871b68e.png">

16. Add a job to the `YML` pipeline for deploying this Logic App
    - This logic app has a new secret: the api key for `BillingService` needs to be added to Github Repo secrets and input to the parameter `billingServicePassword` when deploying the `ARM template` in the `yml` pipeline


### Example output
Logic App in the designer in Visual Studio 2019:
<img width="1130" alt="image" src="https://user-images.githubusercontent.com/2921523/155418426-19f2918c-fd48-4650-bd38-aec1d5056fac.png">

Logic App run in Azure Portal:
<img width="1246" alt="image" src="https://user-images.githubusercontent.com/2921523/155418387-f2b0486f-bb42-441e-ad46-240f4c6c8acc.png">

<img width="1092" alt="image" src="https://user-images.githubusercontent.com/2921523/155418531-cded7440-14b3-4c5c-a0ba-d99298a47c3e.png">


Github Action CI/CD pipeline:
<img width="1115" alt="image" src="https://user-images.githubusercontent.com/2921523/155416884-8c6a9f4f-4549-49cd-a7d0-3e6d6ffc089d.png">
