`Azure Blob Storage` is part of `Azure Storage Account` and can be used to store anything as `blobs`. There are other components in an `Azure Storage Account` that we will not use now, we will focus on Blobs. Blobs are like files and can contain any data (images, text, json, xml, csv, excel, etc). We've already seen how we can store `Serilog` logs as blobs. Now we're going to use blobs as the backend storage for our `IGreetingRepository`.

Azure Blob Storage is similar to AWS S3 for those of you who have checked out AWS before.

The goal is to store all greetings in one single blob in Azure Blob Store. This implementation is very similar to the `FileGreetingRepository` except instead of reading/writing using the `File` class we will use the sdk `Azure.Storage.Blobs`.

In a real world scenario it might make more sense to store each `Greeting` in its own blob, that is one `Greeting` per blob. This design could easily scale to handle millions of `Greetings` where as using a single large blob for all `Greeting`s would give us scalability issues quite fast. If you are feeling adventurous, try to implement one `Greeting` per blob.

We will reuse the same `Azure Storage Account` that we use for storing `Serilog` logs, no need to create a new storage account for this exercise.

More info about Azure Storage Account:
https://docs.microsoft.com/en-us/azure/storage/common/storage-account-overview

More info about Azure Blob Store:
https://docs.microsoft.com/en-us/azure/storage/blobs/storage-blobs-introduction

Before writing any code, navigate to a storage account in your resource group using the Azure Portal and try creating a `blob container`, uploading/downloading some `blobs` using the portal to get a better understanding of the relationship between `Storage Account`, `Container`, `Blobs`.
<img width="1861" alt="image" src="https://user-images.githubusercontent.com/2921523/151566478-56510a77-a3e9-4f89-9f04-1597dfaac739.png">


### Goal
- Azure Blob Store

### Steps
1. `IGreetingRepository` currently only have normal synchronous methods, update the methods to `async Task` like this:
    - Pro tip: use `ctrl` + `r` + `r` to rename methods in Visual Studio
    - You will need to update existing implementations of `IGreetingRepository` (e.g. `FileGreetingRepository`, `MemoryGreetingRepository`) to also be `async` and return `Task<>` where applicable
```c#
    public interface IGreetingRepository
    {
        public Task<Greeting> GetAsync(Guid id);
        public Task<IEnumerable<Greeting>> GetAsync();
        public Task CreateAsync(Greeting greeting);
        public Task UpdateAsync(Greeting greeting);
    }
```
You might get this warning, this is fine, an `async` method does not need to contain an `await` call.
<img width="763" alt="image" src="https://user-images.githubusercontent.com/2921523/151572191-f6a593b3-5912-4237-85f0-80903af5ee6d.png">

2. Remember to "await all the way". Add the `await` key word to all calls to these methods. Use Visual Studio to find references
    - There are quite a few places to update when making this change, don't miss anything!
<img width="1084" alt="image" src="https://user-images.githubusercontent.com/2921523/151572642-006d4fae-bb7e-4b2f-b0fc-d16067dc2e62.png">

3. In `GreetingService.Infrastructure` project, create a new class named `BlobGreetingRepository` that implements the interface `IGreetingRepository`
4. Add the nuget package `Azure.Storage.Blobs` to `GreetingService.Infrastructure`
5. Check out the documentation for this package: https://docs.microsoft.com/en-us/dotnet/api/overview/azure/storage.blobs-readme
    - Try to implement this repository using this documentation
    - Blobs are organised into `containers` in a Storage Account, it might be a good idea to check/create the `container` in the constructor of `BlobGreetingRepository`
    - The Storage Account connection string should be available in the app configuration (`IConfiguration`). Add an `IConfiguration` parameter to the constructor to get this (dependency injection)
6. Extra exercise: Try to implement one `Greeting` per blob instead of all `Greetings` in one single blob

### Example output
<img width="1628" alt="image" src="https://user-images.githubusercontent.com/2921523/151972834-6c80a856-77cc-4ea9-a26f-23b8605a9c28.png">
