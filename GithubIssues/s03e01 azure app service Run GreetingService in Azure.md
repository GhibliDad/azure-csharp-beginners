Instead of running our app locally, it's time to run our app in Azure. Deploy it to Azure from within Visual Studio.

### Goal
- Azure App Service
- Visual Studio publish and deploy

### Steps
1. Right click on `GreetingService.API` project in the `Solution Explorer` and choose `Publish...`
![image](https://user-images.githubusercontent.com/2921523/146207499-80d01f7e-6965-4237-8d80-62abe9132784.png)

2. Choose to publish to `Azure`
![image](https://user-images.githubusercontent.com/2921523/146207640-f43589ec-bac8-44a3-b334-a5f70f0336d5.png)

3. Choose `Azure App Service (Windows)`
![image](https://user-images.githubusercontent.com/2921523/146207737-e388bc4d-129b-4b45-be9f-f2ad4282d82a.png)

4. Choose the correct `subscription` and click on the `+` to create a new `App Service`
![image](https://user-images.githubusercontent.com/2921523/146208048-98ccb1c5-eaae-4718-b524-10b796768425.png)

5. Name the App Service using this convention: `[firstname-appservice-dev]`. My `app service` would be named `keen-appservice-dev`. 
![image](https://user-images.githubusercontent.com/2921523/146210750-921dc1e3-49d9-43d5-9fa8-aafc2df752f6.png)

6. Create a new `Resource Group` using the naming convention `[firstname-rg-dev]`. My `resource group` would be named `keen-rg-dev`  

7. Create a new `Hosting Plan` (a.k.a. `App Service Plan`) and name it `[firstname-asp-dev]`. My `app service plan` would be `keen-asp-dev`

8. Choose the `Free` plan and click on `Create`

9. Check the `Deploy as ZIP package` checkbox

10. Click next

11. Check the `Skip this step` on the `Api Management` view and click next

12. Choose `Publish (generates pubxml file)` and click Finish

13. Click on `Publish` to deploy our application to our chosen `App Service` in Azure

14. Browse to https://portal.azure.com and find you resource group and app service
<img width="762" alt="image" src="https://user-images.githubusercontent.com/2921523/146211084-c00b1611-9e4a-4197-ae68-2fd10101ef80.png">

15. Click on your app service

16. Copy the url
<img width="562" alt="image" src="https://user-images.githubusercontent.com/2921523/146211224-ebeb0184-3027-4e80-b747-1c53840cf7b4.png">

17. Paste the base url into postman and try your API running in Azure! My full url was: `https://keen-appservice-dev.azurewebsites.net/api/greeting` 



### Example output
![image](https://user-images.githubusercontent.com/2921523/146220772-0e9265bc-29d2-4f17-a747-c4574fa7caa8.png)
