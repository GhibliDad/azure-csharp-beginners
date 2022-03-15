We will continue with our `GreetingConsoleApp`. We need a way to be able to write our greetings with different methods. Let's do this with an `Interface`!

### Goal
- Interfaces
- Be able to use different logic depending on use case or requirements
- Null
- Debug code
- Constructor

### Steps
1. Create file `IGreetingWriter.cs` containing interface `IGreetingWriter`
2. Add `public void Write()` method to `IGreetingWriter`
3. Create file `BlackWhiteGreetingWriter.cs` containing class `BlackWhiteGreetingWriter` that implements `IGreetingWriter`
4. Implement the `Write()` method in `BlackWhiteGreetingWriter` to write to console
5. Add property of type `IGreetingWriter` with name `GreetingWriter` to `Greeting` class
6. Add `WriteMessage()` method to `Greeting` class
7. Add logic in `Main()` method to write with the new implementation of `IGreetingWriter`
8. Why do we get `System.NullReferenceException`? Can you debug the problem?
    - Set a break point on the first line of code in the `Main()` method
    - Go to `Run` -> `Start Debugging (F5)`
    - Use the `Step into (F11)` command to step though the program line by line
9. Create file `ColorGreetingWriter.cs` containing the class `ColorGreetingWriter` that implements `IGreetingWriter` 
10. Implement `WriteMessage()` in `ColorGreetingColor`, make it write to console in a color
11. Add logic in `Main()` method to write with the new implementation of `IGreetingWriter`
12. Does this logic also work with `NewYearGreeting` and `ChristmasGreeting`?
13. Try writing in this sequence:
    1. Create a `Greeting` (or a sub type) and write with `BlackWhiteGreetingWriter`
    2. Create another `Greeting` (or a sub type) and write with `ColorGreetingWriter`
    3. Create a third `Greeting` (or a sub type)` and write with `BlackWhiteGreetingWriter`
    4. Everything works as expected?

### Example output
![image](https://user-images.githubusercontent.com/2921523/144608132-8a4546eb-0bf5-47c0-80f3-cc2ae072c3bf.png)
