We received a new business requirement to also store each Greeting as `csv` in a separate blob container. Implement this logic with a blob triggered Azure Function.

Azure Functions can be triggered by a blob (input binding) and can also have write output to a blob (output binding).

`Triggers` are the entry points to an Azure Function. Previously we build Functions with the `HTTP` trigger. `HTTP` triggered Functions listens for HTTP requests and triggers each Function based on the `HTTP Method` and `HTTP Path` of the request. 

There are many various types of triggers in Azure Functions, see list here:
https://docs.microsoft.com/en-us/azure/azure-functions/functions-triggers-bindings?tabs=csharp

Using `Triggers` we can build applications that react to certain events, for example:
* an HTTP Request is received on this address -> Trigger this Function and perform some logic
* a Blob is created in this container -> Trigger this Function and perform some logic
* A message is received in the Queue -> Trigger this Function and perform som logic

Besides `triggers` there are also `Input Bindings` and `Output Bindings`. We can use input/output bindings to receive/send data from/to external sources. Using these bindings allows us to focus on our business logic and let the binding in Azure Functions runtime handle the communication to the external sources.

In this exercise we'll use the `Blob trigger` to trigger our Function whenever a new blob is created in our `greeting` container. We'll use a `Blob output binding` to send the converted data to a new blob in a separate container. Notice that we don't have to add any custom code for communicating with the blobs using triggers and bindings, the technical communication: creating blob clients, downloading blobs, uploading blobs etc are handled for us. We can focus on our business logic, which is converting `Greetings` from `json` to `csv`.

The logic for this solutions will look like this:
1. A http client posts a `Greeting` to our API
2. A `greeting` is saved as `json` in the container `greetings` in the storage account 
3. Whenever a new blob is created, our Function will be automatically triggered using the `Blob trigger` for Azure Functions
4.  Our Function will convert the `Greeting` from `json` to `csv` and write it to a new blob in the container `greetings-csv` using `Blob output binding` in Azure Functions 

### Goal
- Azure Blob Store
- Azure Functions
- Azure Functions Blob Trigger

### Steps
1. Create a new Azure Function class by right clicking on the `GreetingService.API.Function` project and select `Add`
2. Choose the template Azure Functions and name it `ConvertGreetingToCsv`
<img width="1241" alt="image" src="https://user-images.githubusercontent.com/2921523/152359960-4c591d46-9840-4d1c-a946-06bc753dda43.png">

3. Choose the `Blob trigger` and configure it like this: 
<img width="1054" alt="image" src="https://user-images.githubusercontent.com/2921523/152360072-e5433680-630c-415c-9a83-14eb8bcc8efa.png">

4. Choose `Azure Storage` and select your storage account that we created before
<img width="1048" alt="image" src="https://user-images.githubusercontent.com/2921523/152360370-81dd5664-3348-4096-ae41-c5ab3d989735.png">

5. Configure the storage like this:
<img width="1054" alt="image" src="https://user-images.githubusercontent.com/2921523/152360683-841bcf03-8a4e-47fc-a5a8-c4aa3ccbef76.png">

6. Update the generated `Run()` method to async by replacing `void` with `async Task`
7.  Ensure the method has at least these input parameters. The first one is the blob trigger, we trigger this function when a new blob is written to the path `greetings/{name}`. The second one is a blob output binding, writing data to this stream will write the data to a new blob in the path `greetings/{name}`:
    - `[BlobTrigger("greetings/{name}", Connection = "LoggingStorageAccount")]Stream greetingJsonBlob`
    - `[Blob("greetings-csv/{name}", FileAccess.Write)] Stream greetingCsvBlob` 
8. Can you figure out how to use `greetingJsonBlob` to deserialize to a variable of type `Greeting`?
9. Can you figure out how to create `csv` rows from our `Greeting` and write it to the stream `greetingCsvBlob``
    - Hint: use `StreamWriter` to write to a `Stream`
10: If you get an error that says something about missing `AzureWebJobsStorage`, ensure that the connection string to the `Azure Functions Storage Account` is stored in `local.settings.json`, the file should look similar to this:
    - The storage account connection string can be retrieved from the Storage account in Azure Portal
![image](https://user-images.githubusercontent.com/2921523/152431954-b59f9d5e-0656-4b30-9f20-37dac1f64e5c.png)



### Example output
![image](https://user-images.githubusercontent.com/2921523/152432125-22df7eb9-3df5-43a7-bc76-d79b75d8eec1.png)

![image](https://user-images.githubusercontent.com/2921523/152432158-10af037e-a1d0-4852-b520-a4d6a7b252bf.png)

![image](https://user-images.githubusercontent.com/2921523/152432202-36c7b1bc-4006-4dc8-8133-1d3893a93945.png)

