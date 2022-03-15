Add HTTP endpoints to our Azure Functions API for:
* Get User
* Create User
* Update User
* Delete User

`CRUD` = `create`, `read`, `update`, `delete`

### Steps
1. Add the required methods to `IUserService`
2. Implement the new methods in `SqlUserService`
    - Also add the methods to all other classes that implements `IUserService` but you can leave them with `throw new NotImplementedException();` if you want to

### Example output
<img width="1475" alt="image" src="https://user-images.githubusercontent.com/2921523/154081094-7c810ffd-17b4-4d09-8b2d-1adabd97f9db.png">

<img width="879" alt="image" src="https://user-images.githubusercontent.com/2921523/154081153-1e4514fd-7bde-407e-b414-5b29e64eaa62.png">
