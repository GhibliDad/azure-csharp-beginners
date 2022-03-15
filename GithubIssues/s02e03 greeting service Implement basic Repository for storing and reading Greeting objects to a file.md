Now we want to implement a data store for `Greeting` objects. We want to be able to do the following operations:
* List all existing `Greeting` objects
* Get a specific `Greeting` object
* Save a new `Greeting` object
* Update an existing `Greeting` object

We'll implement a basic `Repository pattern` for our data access logic.

### Goal
- REST API
- ASP.NET web api
- Data access (repository pattern)

### Steps
1. Create a new interface in `GreetingService.Core` named `IGreetingRepository`
2. Add the following methods to the interface:
    - `public Greeting Get(Guid id);`
    - `public IEnumerable<Greeting> Get();`
    - `public void Create(Greeting greeting);`
    - `public void Update(Greeting greeting);`
3. Create a class named `FileGreetingRepository` in `GreetingService.Infrastructure` that implements the interface `IGreetingRepository` 
    - Read and write `Greeting` objects to a `json` file with a hard coded path for now
    - `Get()` should return all `Greeting` objects from the `json` file
    - `Get(Guid id)` should return a single `Greeting` object with `id`. Throw exception if `id` does not exist
    - `Create()` should add the `Greeting` object to the file
    - `Update()` should retrieve the `Greeting` from the file, update it with the updated `Greeting` and save to the file
4. Create a new project named `GreetingService.Infrastructure.Test` using the template `xUnit Test Project ` and create a unit test file named `FileGreetingRepositoryTest` contining unit tests for `FileGreetingRepositoryTest`  

### Example output
Unit tests:
![image](https://user-images.githubusercontent.com/2921523/145779849-3d012c07-4d4f-4a79-8a39-11e2d69cd05a.png)

Json file repo content:
```json
[
  {
    "Id": "1bd23b07-75e5-4e88-b143-cf36db8cd7fa",
    "Message": "message1",
    "From": "from1",
    "To": "to1",
    "Timestamp": "2021-12-13T09:40:17.9784526+01:00"
  },
  {
    "Id": "6bc259e6-2d70-40c4-b840-20dee66fef8f",
    "Message": "message2",
    "From": "from2",
    "To": "to2",
    "Timestamp": "2021-12-13T09:40:17.9784554+01:00"
  },
  {
    "Id": "70c4d464-ba2a-42f8-a051-7087d5d72035",
    "Message": "message3",
    "From": "from3",
    "To": "to3",
    "Timestamp": "2021-12-13T09:40:17.9784567+01:00"
  },
  {
    "Id": "14a9dbb6-5e83-4544-a9fa-e38d4a817cd9",
    "Message": "message4",
    "From": "from4",
    "To": "to4",
    "Timestamp": "2021-12-13T09:40:17.9784571+01:00"
  }
]
```