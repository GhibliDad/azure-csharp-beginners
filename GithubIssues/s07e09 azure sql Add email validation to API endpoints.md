We need to ensure that `Greeting.From`, `Greeting.To`, `User.Email` contains actual email addresses to ensure data quality. There are many methods and libraries for validating data, try finding a method that works for you. No need to go overboard, basic validation logic is good enough for now. Return 400 Bad Request if the value is not an `email address` (e.g. `name@domain.com`).

### Steps
1. Add validation to all places where an email is sent in a property (e.g. `PostGreeting`, `PutGreeting`, `PostUser` etc.
    - We can either perform validation in each Function or in the property setters
2. Catch validation exceptions and return a `BadRequestObjectResult` containing info about the validation error

### Example output
![image](https://user-images.githubusercontent.com/2921523/154351009-fd4a9311-e1c0-429f-91b2-2429ffc437fc.png)
