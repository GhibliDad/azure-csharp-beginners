It's also possible to author Logic Apps in Visual Studio 2019:
https://docs.microsoft.com/en-us/azure/logic-apps/quickstart-create-logic-apps-with-visual-studio

And in VS Code:
https://docs.microsoft.com/en-us/azure/logic-apps/quickstart-create-logic-apps-visual-studio-code

Unfortunately there is still no Logic Apps support for Visual Studio 2022. For that reason we'll work with Visual Studio 2019. Logic App tools in VS Code are not as fully featured as the tool for Visual Studio 2019.  

In these exercises we'll use Visual Studio 2019 since that tooling is a bit more fully featured.

Logic Apps source code are normal `ARM Templates` that can be deployed using `az deployment group create` command in Azure CLI. Documentation here:
https://docs.microsoft.com/en-us/cli/azure/deployment/group?view=azure-cli-latest#az-deployment-group-create

It's also possible to create Logic Apps with Bicep but since the Logic App tools currently only generate ARM templates we'll stick to that instead of translating from ARM -> Bicep.



### Steps
1.  Install Visual Studio 2019
    - Be sure to check the `Azure` component when installing
    - Visual Studio 2019 can be installed and used side by side with Visual Studio 2022
2. Open Visual Studio 2019 and install the extension `Azure Logic Apps Tools for Visual Studio 2019`
![image](https://user-images.githubusercontent.com/2921523/154988316-414ac25f-4d94-4272-ad96-47a25b0f8cf7.png)

3. Create a new repo in your Github account for the Logic App exercises
    - Remember to add a `.gitignore` file to the repo
4. In Visual Studio 2019, create a new project of type `Azure Resource Group` and place it in the folder of your new repo
![image](https://user-images.githubusercontent.com/2921523/154989324-3780eaf3-4f5b-4d20-a22b-c0965810de89.png)

5. Choose the `Logic App` template
![image](https://user-images.githubusercontent.com/2921523/154989019-76e2a24e-5509-49cf-a971-20ffa4845751.png)

![image](https://user-images.githubusercontent.com/2921523/154989632-5832cc00-aa47-41fd-b2a8-f33c2495c0a6.png)

6. Right click on the generated `LogicApp.json` and choose `Open with Logic App Designer`
![image](https://user-images.githubusercontent.com/2921523/154989797-1830bca3-df6d-4070-a74d-1026a19eb8c1.png)

7. Create a similar Logic App here in Visual Studio as we did in the previous exercise in Azure Portal

8. We need to add the `password` parameter to the Logic App ARM template, try to figure out how to do this

9. Deploy the Logic App using Azure CLI command: `az deployment group create...`

10. Commit the new files to git

### Example output
<img width="426" alt="image" src="https://user-images.githubusercontent.com/2921523/155032052-1aa4c81a-6c36-4c74-9c6d-b0bfb2bc379a.png">

<img width="1911" alt="image" src="https://user-images.githubusercontent.com/2921523/155032127-6c96d5c3-1060-4815-9791-1cdfb5f0dbbf.png">
