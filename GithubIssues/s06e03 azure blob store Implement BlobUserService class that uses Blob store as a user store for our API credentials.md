Let's store our API credentials (username/password) in a `json` blob in Azure Blob Store. If we format our `json` as a key/value object we should be able to deserialize it to a `Dictionary<string, string>` and use that for authenticating our HTTP requests.

### Goal
- Azure Blob Store

### Steps
1. Create a new class `BlobUserService` in `GreetingService.Infrastructure` that implements `IUserService`
2. Checkout the logic in `BlobGreetingRepository` and see if you can use the same logic to initialize a `BlobContainerClient`
3. Use the path `users/users.json` for the user blob
    - First part is the `container` name, remaining parts is the `blob` name. There can be multiple `/` in the blob name, this constructs a sort of virtual hierarchy similar to folders in a file system.
    - Container name = `users`
    - Blob name = `users.json`
4. If the blob doesn't exist, there are no users which means the method should always return false
5. Update dependency injection to use `BlobUserService` before testing
6. Create folders `GreetingRepository` and `UserService` in `GreetingService.Infrastructure` and move implementations of either interface into these folders. Remember to update namespace in each class to reflect folder structure
    - Use Visual Studio quick action to update namespace 

### Example output
`users/users.json` blob:
```json
{
    "keen": "summer2022",
    "anton": "winter2022"
}
```

![image](https://user-images.githubusercontent.com/2921523/152494052-5ad3c72e-d3a7-406c-a168-4027686339c9.png)
