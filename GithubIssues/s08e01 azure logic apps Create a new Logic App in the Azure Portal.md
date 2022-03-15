`Azure Logic Apps` is a low code/no code offering in Azure for building automated workflows. Logic Apps is often used to build integrations between applications where data is transported between applications, or automated workflows need to be triggered for specific events.

<img width="1193" alt="image" src="https://user-images.githubusercontent.com/2921523/154684976-2d2ecf2d-6e1f-421d-8fe9-41260a239e70.png">

Logic app workflows are graphical workflows that are created with drag and drop from a library of standard components. It's possible to build loops, foreach, if-else, switch etc to control the flow in the workflow. The logic is very similar to our custom code c# code but using Logic Apps we can perform the same logic while writing very little custom code. This increases productivity, we can focus more on the logic instead of the implementation details (as long as the required components exist in Logic Apps that is).

Logic Apps can be built directly in the Azure portal and in Visual Studio. The source code for a `Logic App` is an `ARM template` that is generated in the dev tools in Azure Portal or in Visual Studio.

Example of built in operations:
![image](https://user-images.githubusercontent.com/2921523/154685231-13582dc8-355c-49dd-a9a9-5ce438bb2863.png)


Logic Apps pros:
* Fast development with highly reusable components
* Components are well tested
* Easier to visually understand the logic of the workflow

cons:
* Lower flexibility than custom c# code
* There are limits and quotas in the platform: https://docs.microsoft.com/en-us/azure/logic-apps/logic-apps-limits-and-config?tabs=azure-portal

Logic apps:
https://docs.microsoft.com/en-us/azure/logic-apps/logic-apps-overview

In Logic Apps there is a library of `connectors` that we can use to communicate with external sources. This is similar to `Azure Functions Bindings` but this library is larger and there are more third party connectors available. Using a connector allows us to get or send data to external sources without writing any custom code. There are connectors to communicate with SQL Server, Outlook, One Drive, Sharepoint, SFTP, Blob store, etc.

Connectors:
https://docs.microsoft.com/en-us/azure/connectors/apis-list
https://docs.microsoft.com/en-us/azure/connectors/managed

![image](https://user-images.githubusercontent.com/2921523/154685148-357ec463-3d87-4ab2-9989-053888707f96.png)


### Steps
1. Create a new `Logic App` resource in your resource group
![image](https://user-images.githubusercontent.com/2921523/154685509-0e41dcc0-88d5-4ab2-ad17-6670001b6108.png)
![image](https://user-images.githubusercontent.com/2921523/154685548-36e108fd-8831-4db1-97ff-f99a767214fd.png)

2. Select your resource group, Type = `Consumption` and the correct region and give it a good name
![image](https://user-images.githubusercontent.com/2921523/154685950-2c28397c-0132-4bc9-a176-79a6279884a5.png)

3. Navigate to the Logic App resource and click on `Choose template`
![image](https://user-images.githubusercontent.com/2921523/154686970-a484ddac-8987-4148-b855-cab8fd79185a.png)

4. Choose `Recurrence` template (this is similar to the `Timer` trigger in Azure Functions). This will create a Logic App with the `Recurrence` action
![image](https://user-images.githubusercontent.com/2921523/154690908-57fd9c3a-2b88-4292-bf65-b8996c9520c8.png)

5. Choose 1 hour (which will trigger this Logic App once per hour)
![image](https://user-images.githubusercontent.com/2921523/154691099-ee6847e3-a62a-4555-a17e-d7f09362af62.png)

6. Click on `New step` to create a new step

7. Choose the action `Get current weather` from `MSN Weather` and configure it accordingly
![image](https://user-images.githubusercontent.com/2921523/154704642-34f20b6d-e60f-4eb4-b77c-422438527a7e.png)

8. Click on `New step` to create a new step

9. Choose `HTTP` and configure it to call our `POST Greeting` endpoint in Azure Functions
    - HTTP Method: `GET`
    - URI: `https://keentestdev.azurewebsites.net/api/greeting` (change this to your own Function app uri)
    - Body: A greeting in `JSON` format, copy a greeting from `Postman`. Here we can mix static and dynamic content from previous steps such as `Temperature`, `Wind speed` etc
    - Click on `Add new parameter` drop down, choose `Authentication` and `Basic Auth`
    - Enter the username for your Greeting Service
    - Enter the password for you Greeting Service (we'll write it in clear text for now)
![image](https://user-images.githubusercontent.com/2921523/154705068-ff3f578f-d612-4595-b80a-7cb64d88cdfb.png)

10. Click on `Save` to save the Logic App
11. Click on `Run Trigger` to trigger to logic app
12. Ensure that all steps succeeded (green check mark on each step)

### Example output
![image](https://user-images.githubusercontent.com/2921523/154705326-ae0a104a-3396-4bcb-85fa-eb1091243f35.png)

![image](https://user-images.githubusercontent.com/2921523/154705403-b0234709-1101-4e82-b115-eefa3485e742.png)

![image](https://user-images.githubusercontent.com/2921523/154705490-812db3ec-d1be-4db8-8425-176dcb065f45.png)

Output from GET Greeting endpoint:
![image](https://user-images.githubusercontent.com/2921523/154705660-f20779f7-4279-400a-b7e4-b11b484e0a4c.png)
