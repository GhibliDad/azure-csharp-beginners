It's a good practice to use `async await` for all I/O operations such as calling a db. Refactor `IUserService` to `async` and update all implementing classes.

Remember the principle of async all the way.

### Steps
1. Update all methods in `IUserService` to return `Task` or `Task<T>`
2. Update all implementing classes to `async await`
3. Update all classes that uses `IUserService` to await the calls
3. Verify that all endpoints still work as intended

### Example output
