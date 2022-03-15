Deploy our Functions to a `Function App` in Azure.

### Goal
- Azure Functions

### Steps
1. Right click on the Function project and follow the wizard to deploy to Azure (`Publish...`)
    - Choose `Azure Function App (Windows)` and name it `[firstname]-function-dev` 
    - Place it in the same resource group as our app service deploy
    - Create a new storage account and name it `[firstname]funstodev` (must be globally unique and cannot exceed 24 characters)
    - Choose `Consumption` plan
![image](https://user-images.githubusercontent.com/2921523/147001505-56e5c5ce-7427-4d6b-8039-f9ff9d92ce07.png)
2. Add required app settings values to the `Function App` in Azure
    - `FileRepositoryFilePath`
        - Use a path like this: `/home/site/wwwroot/greeting.json` to use the correct folder (will get access denied error otherwise)
    - username/password for basic auth
3. Browse to the function app in the Azure Portal and navigate to `Functions` and into one of your functions
4. Click on `Code`, `Test` and fold up  `Logs` on the bottom of the page
5. Once the logs are connected, try calling the function and view the output
6. Explore different menu items in  a Function app
7. Connect the Function app to our existing application insights instance
<img width="1877" alt="image" src="https://user-images.githubusercontent.com/2921523/147004014-efbc5989-471d-4bd1-87fc-1b4429fa0e8d.png">
8. Use our `GreetingService.API.Client` console app to call our function app repeatedly, how many req/s can you achieve?

9. Will the function scale to handle to load? Have Application Insights Live Metrics view open while testing

### Example output
<img width="1835" alt="image" src="https://user-images.githubusercontent.com/2921523/147004470-382579a9-1440-47f6-b322-a08590c46dae.png">

```console
Response: OK - Call: 9966 - latency: 58 ms - rate/s: 848,8443035060438
Response: OK - Call: 9965 - latency: 59 ms - rate/s: 848,7120481671896
Response: OK - Call: 9972 - latency: 54 ms - rate/s: 849,2577608954632
Response: OK - Call: 9975 - latency: 52 ms - rate/s: 849,4609925170364
Response: OK - Call: 9976 - latency: 52 ms - rate/s: 849,4615651765308
Response: OK - Call: 9979 - latency: 50 ms - rate/s: 849,5200139559989
Response: OK - Call: 9973 - latency: 58 ms - rate/s: 848,9380426067984
Response: OK - Call: 9974 - latency: 58 ms - rate/s: 849,0065729315968
Response: OK - Call: 9980 - latency: 49 ms - rate/s: 849,3819210317713
Response: OK - Call: 9977 - latency: 56 ms - rate/s: 849,1136672900138
Response: OK - Call: 9982 - latency: 50 ms - rate/s: 849,4316964118523
Response: OK - Call: 9981 - latency: 51 ms - rate/s: 849,268339286522
Response: OK - Call: 9983 - latency: 54 ms - rate/s: 849,1786427394784
Response: OK - Call: 9984 - latency: 52 ms - rate/s: 849,1190177570824
Response: OK - Call: 9978 - latency: 65 ms - rate/s: 848,4104895983241
Response: OK - Call: 9986 - latency: 53 ms - rate/s: 849,0884547378332
Response: OK - Call: 9989 - latency: 50 ms - rate/s: 849,3132946183985
Response: OK - Call: 9991 - latency: 51 ms - rate/s: 849,3542217287185
Response: OK - Call: 9987 - latency: 52 ms - rate/s: 849,0042498668729
Response: OK - Call: 9992 - latency: 50 ms - rate/s: 849,3747528200037
Response: OK - Call: 9990 - latency: 53 ms - rate/s: 849,0835931721781
Response: OK - Call: 9993 - latency: 52 ms - rate/s: 849,3091720214662
Response: OK - Call: 9988 - latency: 54 ms - rate/s: 848,887740758115
Response: OK - Call: 9985 - latency: 60 ms - rate/s: 848,6217621924532
Response: OK - Call: 9994 - latency: 54 ms - rate/s: 849,2281426252918
Response: OK - Call: 9995 - latency: 50 ms - rate/s: 849,2322222750425
Response: OK - Call: 9996 - latency: 50 ms - rate/s: 849,3037875533357
Response: OK - Call: 9999 - latency: 49 ms - rate/s: 849,2507619135724
Response: OK - Call: 9997 - latency: 52 ms - rate/s: 849,0413989681545
Response: OK - Call: 9998 - latency: 52 ms - rate/s: 849,1250449245156
```