Implement `Basic Authentication` in `GreetingService`.

`Basic Authentication` is a simple authentication scheme build into `HTTP` and is widely supported on the internet. It's not the most secure scheme but it's often good enough.

`Basic Authentication` is sent from the client with the `Authorization` request header and looks similar to this: `Authorization: Basic YWRtaW46cEBzNXcwcmQ=`

`Basic` is the so called `realm`. In our case we'll always use `Basic` here.
`YWRtaW46cEBzNXcwcmQ=` is a `Base64Encoded` string of `username:password`. Note that `Base64Encoding` is not encryption as we'll see when we implement this in `GreetingService`.

Use Postman to test this using the built in support for `Basic Auth`.

Common logic like this is usually readily available on the internet, common practice is to google and copy or get inspiration for a solution.

### Goal
- Basic Autentication
- HTTP request headers
- Class attributes
- ASP.NET pipeline

### Steps
1. Create a new folder: `Authentication` in `GreetingService.API`
2. Create a new class named `BasicAuthAttribute`
    - Copy or get inspiration from: https://codeburst.io/adding-basic-authentication-to-an-asp-net-core-web-api-project-5439c4cf78ee
3. Create a new class named `BasicAuthFilter`
    - Copy or get inspiration from previous link
4. Create new interface `IUserService` in `GreetingService.Core`
    - Add method `public bool IsValidUser(string username, string password);` 
5. Register `HardCodedUserService` as `IUserService` in dependency injection 
5. Create new class `HardCodedUserService` in `GreetingService.Infrastructure` that implements `IUserService`  
    - Hard code a collection of username/password in a `Dictionary<string, string>` and use this to validate user
6. Add attribute `[BasicAuth]` to the class `GreetingController` to ensure all endpoints require authentication. It's also possible to only add the attribute to individual methods to only require auth for those specific endpoints 

### Example output
Correct credentials:
![image](https://user-images.githubusercontent.com/2921523/146175085-83adb2bc-80f5-49d6-b76f-5ed9dd60a3fd.png)

Wrong password:
![image](https://user-images.githubusercontent.com/2921523/146175141-8685b998-44f7-4a7b-b17e-bd9cb9816099.png)

No auth:
![image](https://user-images.githubusercontent.com/2921523/146175302-b04912e2-402b-43f5-bd49-ec624371112b.png)

Authorization header looks like this:
![image](https://user-images.githubusercontent.com/2921523/146193061-b24b49a8-c81d-4d29-8ee0-273902d138ae.png)
