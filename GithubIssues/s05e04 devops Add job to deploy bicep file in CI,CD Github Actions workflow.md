Add a new job to the Github Actions workflow (the YML file) to deploy our `bicep` file.

### Goal
- Infrastructure as Code (IaC)
- CI/CD
- Bicep
- DevOps
- Github Actions
- YML

### Steps
1. Add this job to the `yml` file. Note the step that runs an `Azure CLI` command to deploy the `bicep` file.
    - Be sure to update the resource group name in the `Azure CLI` step to you resource group!
```yml
  deploy_bicep:
    runs-on: ubuntu-latest
    needs: build
    environment:
      name: 'Development'
      url: ${{ steps.deploy-to-webapp.outputs.webapp-url }}

    steps:
      - uses: actions/checkout@v2

      - name: 'Login via Azure CLI'
        uses: azure/login@v1
        with:
          creds: ${{ secrets.AZURE_CREDENTIALS }}
          
      - name: Azure CLI script
        uses: azure/CLI@v1
        with:
          azcliversion: 2.30.0
          inlineScript: |
            az group create -l westeurope -n keen-rg-dev
            az deployment group create --resource-group keen-rg-dev --template-file ./azure_resources.bicep --parameters appName=keentestdev
```
 2. Update the `needs` field on the `deploy` job in the `yml` file to this to make the jobs run in the correct order:
![image](https://user-images.githubusercontent.com/2921523/151552454-c2560b45-71b8-4acc-a2ca-f8ebf8b9f71e.png)
    - this property refers to a job name and can be used to control which job runs in which order or if the can be run in parallel

3. Push to `main` and verify that the pipeline succeeds
4. Delete the `resource group` containing you application from the Azure Portal (portal.azure.com) and wait until it is deleted
5. Push to `main` again to trigger the pipeline and verify that all Azure resources are deployed and that the application works


### Example output
<img width="1539" alt="image" src="https://user-images.githubusercontent.com/2921523/151556001-52975a81-be0f-43c2-acfc-b15695fa9ab7.png">

<img width="1424" alt="image" src="https://user-images.githubusercontent.com/2921523/151555954-a21e5bb6-1da8-4863-b826-7d294ceeb4e9.png">

<img width="1658" alt="image" src="https://user-images.githubusercontent.com/2921523/151556069-fc463449-4b8d-42ea-b7e9-9bd4e51d1f74.png">

