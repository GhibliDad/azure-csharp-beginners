HTTP Status Codes are standard codes that should be returned from an API that gives the caller information about what went wrong. There are many codes, some more commonly used than others. It's a good practice to return correct codes if possible.

Common codes:
* 200 OK
* 202 Accepted
* 301 Moved permanently
* 400 Bad request
* 401 Unauthenticated
* 403 Forbidden
* 404 Not found
* 500 Internal server error
* 503 Service unavailable

In general: 
* `2xx` codes are good responses
* `3xx` require the client to make another request (e.g. follow redirection)
* `4xx` indicates something in the client request was wrong (e.g. wrong url, wrong credentials, wrong input data)
* `5xx` indicates something in the server was wrong (e.g. unhandled exception, network error to down stream application)

In a production scenario be careful what error information is returned to the client, too detailed error information can be used by a malicious client to exploit the system.

### Goal
- HTTP status codes
- Error handling

### Steps
1. Update the return type on `Get(Guid id)` from `Greeting` to `IActionResult`. `IActionResult` allows us to explicitly state which http status to return.
2. Update logic: 
     - When a greeting with `id` is found, return the `Greeting` with http status `200 OK` 
     - When no greeting with `id` id found, return http status `404 not found`
3. Update the `Post()` method to return `IActionResult`
     - Return `202 Accepted` if succeeded
     - Return `409 Conflict` if `id` already exists
4.  Update `Put()` method
     - Return `202 Accepted` if succeeded
     - Return `404 Not found` if `id` does not exist in repo
5. Try throwing an exception in one of the methods like this: `throw new Exception("some error");`. What status code is returned? 

### Example output
Not found
![image](https://user-images.githubusercontent.com/2921523/146200083-4ed1fb95-9906-4523-9bcf-a3bc3e849c95.png)

Conflict
![image](https://user-images.githubusercontent.com/2921523/146200222-84204218-6cbc-4f4a-8ad9-63967f6865df.png)

Not found
![image](https://user-images.githubusercontent.com/2921523/146200281-0f7ddb7d-45b0-4351-b6ba-33dee58e296b.png)

Accepted:
![image](https://user-images.githubusercontent.com/2921523/146200371-c4f6ca25-dd60-4ba2-90c3-473786b0e015.png)

Also possible to override the default response body with custom messag:
![image](https://user-images.githubusercontent.com/2921523/146200686-9c0e2f33-7946-469f-9fec-94e3408e72e3.png)
