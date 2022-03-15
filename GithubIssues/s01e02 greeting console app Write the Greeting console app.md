This will be our first application that we'll work on for a bit.

The application is called `GreetingConsoleApp` and will be used to print various greetings in the console.

### Goal
- Program with objects
  - Classes
  - Inheritance
  - Constructors
  - Instance methods
- Code reuse

### Steps
1. Create a new `Visual Studio Solution` named `GreetingConsoleApp` in a new folder named `GreetingConsoleApp`
2. Create a new `Visual Studio Project` named `GreetingConsoleApp` in the solution in a folder located at `GreetingConsoleApp\GreetingConsoleApp` (We'll have multiple projects in this folder later on)
3. Add namespace: `GreetingConsoleApp` to `Program.cs`
4. Add class `Program` to `Program.cs`
5. Add `Main` method to `Program` class
6. Create `Greeting.cs` file containing `Greeting` class
    - `Greeting` should contain these properties:
      - Message
      - From
      - To
      - Timestamp
7. Create `ChristmasGreeting.cs` file containing `ChristmasGreeting` class and make it a sub class to `Greeting` class
8. Create `NewYearGreeting.cs` file containing `NewYearGreeting` class and make it a sub class to `Greeting` class
9. Add method `public string GetMessage()` to `Greeting` class
10. Create a `Greeting` object in `Main()` and write `Message` on the console
    - Do the same with `NewYearGreeting` and `ChristmasGreeting`
11. Add special New Year logic to `NewYearGreeting`
12. Add special Christmas logic to `ChristmasGreeting`
13. Also write timestamp with each message

### Example output
```console
Keens-MBP:GreetingConsoleApp keenfann$ dotnet run

Greeting message
03/12/2021 11:44:59:
How are you?

NewYearGreeting message
03/12/2021 11:44:59:
Happy new year!
Looking forward to 2022!

ChristmasGreeting message
Merry Christmas!
Here's your present: SDNA13215

Done!

Keens-MBP:GreetingConsoleApp keenfann$ 
```
