Another common scenario is to use Managed Identity in Logic Apps. Lets update our Weather report logic app to get the password for the GreetingService from Azure Key Vault instead. The same concept can be used for authenticating the Logic App to other Azure Resources such as `Azure SQL`, `Azure Storage Account`, etc.

### Goal
* Azure Logic Apps
* Key Vault
* Managed Identity

### Steps
1. Remove the `password` parameter from the Logic App
2. Add the `password` as a secret in Key Vault 
3. Enable Managed Identity in the Logic App 
4. Add an `Access Policy` for the logic app identity to the Key Vault
5. Add the `Azure Key Vault` action to get a secret and configure it accordingly
6. Test it out

### Example output
<img width="678" alt="image" src="https://user-images.githubusercontent.com/2921523/157648860-2cd5fa37-f8fb-440a-9253-6bf471f70844.png">
