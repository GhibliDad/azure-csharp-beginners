Build unit tests for every `public` method in `GreetingTemplateRepository`

### Goal
- Unit tests

### Steps
1. Create a new folder named `GreetingConsoleAppTest` located here: `GreetingConsoleApp\GreetingConsoleAppTest`
2. Create a new `Visual Studio Project` in the new folder
3. Add a reference to `GreetingConsoleApp` project 
5. In the `Solution Explorer`: navigate to the file `UnitTest1.cs` in the test project and rename the file to `GreetingTemplateRepositoryTest.cs`
6. Rename the class `UnitTest1` -> `GreetingTemplateRepositoryTest`
7. Write unit tests for all `public` methods in `GreetingTemplateRepository` class

### Example output
```console
Keens-MBP:GreetingConsoleAppTest keenfann$ dotnet test
  Determining projects to restore...
  All projects are up-to-date for restore.
  GreetingConsoleApp -> /Users/keenfann/Documents/GitHub/azure-csharp-beginners/GreetingConsoleApp/GreetingConsoleApp/bin/Debug/net6.0/GreetingConsoleApp.dll
  GreetingConsoleAppTest -> /Users/keenfann/Documents/GitHub/azure-csharp-beginners/GreetingConsoleApp/GreetingConsoleAppTest/bin/Debug/net6.0/GreetingConsoleAppTest.dll
Test run for /Users/keenfann/Documents/GitHub/azure-csharp-beginners/GreetingConsoleApp/GreetingConsoleAppTest/bin/Debug/net6.0/GreetingConsoleAppTest.dll (.NETCoreApp,Version=v6.0)
Microsoft (R) Test Execution Command Line Tool Version 17.0.0
Copyright (c) Microsoft Corporation.  All rights reserved.

Starting test execution, please wait...
A total of 1 test files matched the specified pattern.

Passed!  - Failed:     0, Passed:     9, Skipped:     0, Total:     9, Duration: 39 ms - /Users/keenfann/Documents/GitHub/azure-csharp-beginners/GreetingConsoleApp/GreetingConsoleAppTest/bin/Debug/net6.0/GreetingConsoleAppTest.dll (net6.0)
```