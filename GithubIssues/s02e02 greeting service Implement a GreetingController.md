Implement a new `GreetingController` that will contain all our `Greeting` related endpoints in our API. We'll focus on the API endpoints and routing for now. No business logic for processing `Greeting` objects. Return hard coded responses for now.

General guidelines regarding `HTTP methods`. Use these as a starting point, there will be scenarios where the below guidelines are not a good fit, always be pragmatic!
 - `GET` = get data
 - `POST` = create
 - `PUT` = update
 - `DELETE` = remove/disable 

General guidelines for `project references` in this solution:
  - `GreetingService.API` can reference all projects
  - `GreetingService.Infrastructure` can reference `GreetingService.Core`
  - `GreetingService.Core` cannot have any project references 

### Goal
- Controller
- REST API endpoints

### Steps
1. Create a new `Controller` named `GreetingController` using the template `API Controller with read/write actions`
2. Create a new folder `Entities` in the project `GreetingService.Core`
3. Create a new file `Greeting.cs` in `Entities` folder 
4. Copy the properties from the `Greeting` class in our previous implementation
    - Add a new property named `Id` with the type `Guid` 
5. Update `GreetingController.Post()` method to accept a `Greeting` object as input parameter from body and write `greeting.Message` to console
6. Update `GreetingController.Put()` method to accept a `Greeting` object as input parameter from body and write `greeting.Message` to console
6. Update `GreetingController.Get()` to return a new collection of hard coded `Greeting` objects
7. and `GreetingController.Get(int id)` to return a specific `Greeting`
7. Remove the `Delete(int id)` method, we'll not support deletes at this time

### Example output
![image](https://user-images.githubusercontent.com/2921523/145559097-6a57e487-6da0-4c34-8907-060c84f20752.png)
