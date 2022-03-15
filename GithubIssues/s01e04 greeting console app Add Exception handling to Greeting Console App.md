Handle exceptions when `GreetingWriter` is `null`. Prevent application crash, all valid greetings should be written. Write an error message if a greeting cannot be written.

### Goal
- Exception handling
- Exceptions
- Uncaught exceptions

### Steps
1. On one of the Greeting objects, comment out the line where we create a `GreetingWriter`
2. The program will crash
3. Add exception handling in the `WriteMessage()` method by wrapping the logic with a `try/catch` block
4. Test the program, verify that the exception is handled and the program continues to write remaining greetings

### Example output
```console
03/12/2021 14:29:22:
How are you?

ERROR: Failed to write greeting. GreetingWriter or something was null

03/12/2021 14:29:22:
Merry Christmas!
Here's your present: SDNA13215


Done!

Keens-MBP:GreetingConsoleApp keenfann$ 
```