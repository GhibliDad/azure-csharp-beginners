Now we implement an API client that calls our Greeting service. Don't focus too much on `Console.WriteLine` formatting, focus on understanding `HttpClient`, `await async`, and http requests. 

### Goal
- REST API client
- sync/async
- await async
- Create contracts (classes) for API

### Steps
1. Create a new project named `GreetingService.API.Client`. Use the template for `Console Application`
2. Add the `namespace`, `class`, `Main()` declarations
3. Create a static field of the `HttpClient` like this: `private static HttpClient _httpClient = new();`
4. Create a new class named `Greeting`
5. Copy a `Greeting` json (from either postman, swagger, file repository) and use the Visual Studio menu option `Edit -> Paste Special -> Paste Json as Classes`  into this class
6. Rename `Rootobject` to `Greeting` 
7. Implement these methods:
    - `private static async Task GetGreetingsAsync()`
    - `private static async Task GetGreetingAsync(Guid id)`
    - `private static async Task WriteGreetingAsync(string message)`
    - `private static async Task UpdateGreetingAsync(Guid id, string message)`
    - Notice the `async` key word and `Task` return type

### Example output
```console
Welcome to command line Greeting client
Enter name of greeting sender:
John
Enter name of greeting recipient:
Ellen
Available commands:
get greetings
get greeting  [id]
write greeting  [message]
update greeting  [id] [message]

Write command and press [enter] to execute
write greeting Hello Ellen!
Wrote greeting. Service responded with: OK

Available commands:
get greetings
get greeting  [id]
write greeting  [message]
update greeting  [id] [message]

Write command and press [enter] to execute
write greeting Whats up?
Wrote greeting. Service responded with: OK

Available commands:
get greetings
get greeting  [id]
write greeting  [message]
update greeting  [id] [message]

Write command and press [enter] to execute
get greetings
[c9a1f640-2bf9-4ba7-a213-3a57fc3a88d1] [2021-12-15 10:46:35] (John -> Ellen) - Hello Ellen!
[78434579-d517-44f3-aa1a-2cf87818a687] [2021-12-15 10:46:35] (John -> Ellen) - Whats up?

Available commands:
get greetings
get greeting  [id]
write greeting  [message]
update greeting  [id] [message]

Write command and press [enter] to execute



```