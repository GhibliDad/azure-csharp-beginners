HTTP requests can contain a `request header` named `Accept` that specifies what format on the response the client prefers. Add support to `GreetingService` to return `xml` if the `Accept` header has the value `application/xml` and default to `json` otherwise.

An `HTTP request` typically contains the following parts:
  - `uri`
  - `headers`
  - `body`

All three parts can be used to transport relevant information. This time we focus on HTTP headers.

This is called `content negotiation`.

### Goal
- Content negotiation
- HTTP Request headers
- Accept header

### Steps
1. Add support! Can you figure it out without looking at the solution?
  * Hint: we should be able to solve this by adding one line in the dependency injection config in `Program.cs`
  * Use the search terms `ASP.NET Accept header` if you want to try to google the solution :)
2. Use Postman or similar tool to test

### Example output
Accept: application/xml
![image](https://user-images.githubusercontent.com/2921523/146167735-41020c51-ec11-4d7d-89cb-dd7b89eee7d9.png)

Accept: application/json
![image](https://user-images.githubusercontent.com/2921523/146168229-d78cd4ed-c60c-439b-bd80-de511e04fbe9.png)
