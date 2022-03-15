We want an administrator to approve creation of each new user. The approval will be done in the Teams channel `Greeting Service User Verification`. The approval flow should look something like this:
* A new user is POST:ed to our API
* The user will immediately be inserted to the db with the approval status `Pending` (this will require a new column in the `Users` table)
* An approval is sent to the Teams channel where an administrator will inspect the information and approve or reject the user
    - This will trigger a web hook call to our API that updates the approval status on the user in the db to: `Approved` or `Rejected`

The flow will look something like this:
`POST User` -> `Send Approval message to Teams` -> `Administrator approves/rejects` -> `Teams will call the approve or reject endpoints in GreetingService API ` -> `The user in DB is approved or rejected`

This feature will be a bit harder to test since Teams will not be able to call our local greeting service running in `localhost`. We will have to simulate the flow with Postman or similar when testing and debugging locally. 

Also beware that the GreetingService running in Azure will compete for any messages posted to the Service Bus Topic which might interfere with running the service locally. One way to solve this would be to create a new environment (i.e. new resource group and new resources) for the GreetingService that is dedicated for local debugging. Another way is to temporarily stop the GreetingService running in Azure while debugging.

Only way to test the full flow including approve/reject from Teams is to deploy it to Azure and test it there.

### Goal
* Azure Service Bus
* Publish/Subscribe
* Teams web hook

### Steps
1. Create a new Azure Service Bus subscription named `user_approval` with an appropriate filter rule in `Bicep` and deploy it to Azure
2. Add these properties to `User` (and `add-migration` and `update-database`)
    - `ApprovalStatus`
        - `Approved`
        - `Rejected`
        - `Pending`
    - `ApprovalStatusNote`
        - Let this contain a human readable note for the status e.g `Approved by admin` or similar
    - `ApprovalCode`
        - This code will be sent to Teams and will be used to match an incoming `Approve` or `Reject` with an existing user
    - `ApprovalExpiry`
        - We want some form of expiry for an approval, use this property for that (e.g. we wait at most 24h for a response from Teams)
3. Set `ApprovalStatus` to `Pending` when inserting a new user to db
4. Create a new interface `IApprovalService` with a method `BeginUserApprovalAsync(User user)`
5. Create a new class `TeamsApprovalService` that implements `IApprovalService`
6. `BeginUserApprovalAsync(User user)` should send a message to a Teams channel via the `Teams Webhook Connector` that is already configured on the channel `Greeting Service User Verification`

More information about Teams web hooks here:
https://docs.microsoft.com/en-us/microsoftteams/platform/webhooks-and-connectors/how-to/connectors-using?tabs=cURL

The code should look something like this (copied from above url):
```c#
// Please note that response body needs to be extracted and read 
// as Connectors do not throw 429s
try
{
    // Perform Connector POST operation     
    var httpResponseMessage = await _client.PostAsync(IncomingWebhookUrl, new StringContent(content));
    // Read response content
    var responseContent = await httpResponseMessage.Content.ReadAsStringAsync();
    if (responseContent.Contains("Microsoft Teams endpoint returned HTTP error 429")) 
    {
        // initiate retry logic
    }
}
```

`_client` in the above code is an `HttpClient`. Ask for an `HttpClient` in the constructor. Or ask for an `IHttpClientFactory` instead. More info here about `IHttpClientFactory` here:
https://docs.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/use-httpclientfactory-to-implement-resilient-http-requests#benefits-of-using-ihttpclientfactory

The `url` of the web hook will be posted in Teams. This `url` is secret, anyone with the `url` can post to the channel. Do not commit this to source control.

The `json` body can be used to configure how the card should look like in Teams, for example (copied and adapted from https://messagecardplayground.azurewebsites.net) 
     - Adapt the `json` to contain relevant information for the new `user`
     - The `target` uri under each action should point to the uri of our two new approval endpoints that we will implement in later steps. Put a place holder there for now and configure the correct address after you've implemented the endpoints


```json
{
	"@type": "MessageCard",
	"@context": "https://schema.org/extensions",
	"sections": [
		{
			"title": "**Pending approval**",
			"activityImage": "https://upload.wikimedia.org/wikipedia/commons/thumb/7/7c/User_font_awesome.svg/1024px-User_font_awesome.svg.png?20160212005950",
			"activityTitle": "Approve new user in GreetingService: email",
			"activitySubtitle": "First name and Last name",
			"facts": [
				{
					"name": "Date submitted:",
					"value": "06/27/2017, 2:44 PM"
				},
				{
					"name": "Details:",
					"value": "Please approve or reject the new user for the GreetingService"
				}
			]
		},
		{
			"potentialAction": [
				{
                    "@type": "HttpPOST",
                    "name": "Approve",
                    "target": "http://..."
                },
                {
                    "@type": "HttpPOST",
                    "name": "Reject",
                    "target": "http://..."
                }
			]
		}
	]
}
```

7. Add these methods to `IUserService`:  
    - `ApproveUserAsync(string approvalCode)`
    - `RejectUserAsync(string approvalCode)`

8. When clicking on `Approve`: it should call the approve user endpoint in `GreetingService API` that approves the user. Implement this endpoint. The webhook uri must contain `approvalCode` of the approved user
    - Approving a user simply means setting the `ApprovalStatus` to `Approved` and writing a message to `ApprovalNote`
    - The ApproveUser endpoint should not require `BasicAuth`, the `ApprovalCode` in the `url` is enough to know that it's a valid request
9. When clicking on `Reject` it should call the reject user endpoint in `GreetingService API` that rejects the user. Implement this endpoint. The webhook uri must contain `approvalCode` of the rejected user
    - Rejecting a user simply means setting the `ApprovalStatus` to `Rejected` and writing a message to `ApprovalNote`
    - The RejectUser endpoint should not require `BasicAuth`, the `ApprovalCode` in the `url` is enough to know that it's a valid request


### Example output
