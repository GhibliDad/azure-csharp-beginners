Now that we have our `bicep` template for deploying all needed Azure infrastructure for running our application, it's time to automate deployment of our code to the application in Azure. 

The easier it is to deploy, the more we will deploy, the more we deploy, the more features we add to our application for our stakeholders. Automated code deploys reduces risks of "missing a click" when deploying, also all deploys will have the same result no matter who performs the deploy, lowering dependencies on individuals in the team. Deploying code from source control instead of from local development machines also ensures that all deployed code is always source controlled, minimising risk of losing code.

Automated deploys are often called `Continuous Integration/Continuous Delivery` or `CI/CD`.

Github actions are written in a language called `YML`. Don't attempt to write a pipeline by hand from scratch, always start with a template or a copy from another project. Indentations in `YML` are significant, always correctly indent you `YML` code. I prefer to use `VS Code` or the built in editor in Github for writing `YML` for Github Actions but any text editor should work.

### Goal
- Infrastructure as Code (IaC)
- CI/CD
- Bicep
- DevOps
- Github Actions
- YML

### Steps
1. Navigate to your own repo in your personal Github account on github.com
2. Navigate to `Actions` in the repo
<img width="1861" alt="image" src="https://user-images.githubusercontent.com/2921523/151528470-fdd5f533-2204-4185-ae4c-e5c8a5d42c2d.png">

3. Search for the workflow named `Deploy a .NET Core app to an Azure Web App` and click on `Configure`
4. Read the comments in the generated yml file
5. Update the variable values under `env:` in the yml file, here are some example values, use values that correspond to your own app:
```yml
env:
  AZURE_WEBAPP_NAME: keentestdev    # set this to the name of your Azure Web App
  AZURE_WEBAPP_PACKAGE_PATH: '.'      # set this to the path to your web app project, defaults to the repository root
  DOTNET_VERSION: '6.0.x'                 # set this to the .NET Core version to use
```

6. Add this step to the YML under the `deploy` job:
```yml
      - name: 'Login via Azure CLI'
        uses: azure/login@v1
        with:
          creds: ${{ secrets.AZURE_CREDENTIALS }}
```
![image](https://user-images.githubusercontent.com/2921523/151547066-06123b03-2e85-41ee-aa2e-bbe88073a504.png)

6. Click on `Start commit` to commit to the `main` branch in the repo
    - This will create a file here: `[repoRootFolder]/.github/workflows/azure-webapps-dotnet-core.yml`
    - Your `[repoRootFolder]` is where you cloned your repo on disk
    - The filename might vary depending on how you created this file
    - This file is source controlled like any other file and can (and should) be edited with an editor like `VS Code`

<img width="1679" alt="image" src="https://user-images.githubusercontent.com/2921523/151530212-6a7c7028-01e0-45d7-a225-a2d43313547b.png">

7. Navigate to `Settings` in the repo and to the `Secrets` and `Actions` area
8. Click on `New repository secret`
    - Give it the name: `AZURE_CREDENTIALS`
    - And paste this json as value (value for `clientSecret` will be provided for you separately):
```json
{
    "clientId": "2302ae74-a67a-4b5c-bdf9-521f8b8d7a69",
    "clientSecret": "THE_CLIENT_SECRET",
    "subscriptionId": "5bb1b2d9-ed37-4ee3-9053-56954eaa90c7",
    "tenantId": "9583541d-47a0-4deb-9e14-541050ac8bc1"
}
```
<img width="1117" alt="image" src="https://user-images.githubusercontent.com/2921523/151546876-a13e2981-33e2-44f0-aecf-976bb63c230b.png">

9. Update a file and push the changes to `main` branch to trigger this workflow and verify that it runs successfully. Setting up CI/CD pipelines always require lots of trial and error.

10. Make a code change in one of the endpoints in the GreetingService.API.Function` project, commit, push to `main`, ensure that the flow succeeds and try calling the endpoint the verify that the changes were correctly deployed to your `Function app` in Azure.



### Example output
<img width="1357" alt="image" src="https://user-images.githubusercontent.com/2921523/151547512-49d00809-a906-452b-8e58-bcc78311a71a.png">

<img width="1491" alt="image" src="https://user-images.githubusercontent.com/2921523/151547543-d2be05a9-c709-4956-b1a0-265385ab28f0.png">

<img width="1471" alt="image" src="https://user-images.githubusercontent.com/2921523/151547581-d21d21a3-b401-4985-bf5a-3c6b770616b7.png">
