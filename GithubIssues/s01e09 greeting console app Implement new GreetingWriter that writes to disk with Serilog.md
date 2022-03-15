Need to be able to write greetings to a file on disk. Create a new implementation of `IGreetingWriter` that uses the `serilog` to write greetings to a file on disk.

We will install the nuget packages for `serilog` and use this package to help us write greetings to disk. 

Serilog consists of a core package and multiple `sinks` that help write to various targets:
https://github.com/serilog/serilog/wiki/Provided-Sinks

Serilog:
https://github.com/serilog/serilog/wiki/Getting-Started

Serilog file sink:
https://github.com/serilog/serilog-sinks-file

### Goal
- Nuget packages
- Use external libraries

### Steps
1. Add nuget packages `serilog` and `serilog.sinks.file` either via `Nuget Package Manager` gui or via `dotnet cli` with:
  - `dotnet add package serilog`
  - `dotnet add package serilog.sinks.file`
4. Create a new file named `FileGreetingWriter.cs` containing the class `FileGreetingWriter`
5. Make `FileGreetingWriter` implement the interface `IGreetingWriter`
6. Create a readonly field for our logger: `private readonly Logger _logger`
7. Create a constructor and initialise `_logger`
8. Implement the `Write()` method and write the `message` with our `_logger
9. Consider also write to console in the `Write()` method to make it easier to see what's going on when running the program

### Example output
```console
2022-01-13 10:03:09.446 +01:00 [INF] [2022-01-13T10:01:39.0155186+01:00] - Hello file!
2022-01-13 10:10:22.382 +01:00 [INF] [2022-01-13T10:10:22.1813112+01:00] - Hello file!
2022-01-13 10:15:29.775 +01:00 [INF] [2022-01-13T10:15:29.5924259+01:00] - Hello file!
2022-01-13 10:16:22.897 +01:00 [INF] [2022-01-13T10:16:22.6434012+01:00] - Hello file!
2022-01-13 10:16:35.098 +01:00 [INF] [2022-01-13T10:16:34.9260495+01:00] - Hello file!
2022-01-13 10:19:27.975 +01:00 [INF] [2022-01-13T10:19:27.6553174+01:00] - Hello file!
2022-01-13 10:20:03.802 +01:00 [INF] [2022-01-13T10:20:03.5346939+01:00] - Hello file!
2022-01-13 10:20:03.827 +01:00 [WRN] [2022-01-13T10:20:03.5346939+01:00] - Hello file!
2022-01-13 10:20:03.830 +01:00 [ERR] [2022-01-13T10:20:03.5346939+01:00] - Hello file!
2022-01-13 10:20:03.832 +01:00 [FTL] [2022-01-13T10:20:03.5346939+01:00] - Hello file!
2022-01-13 10:26:41.292 +01:00 [ERR] Cannot divide by zero!
System.DivideByZeroException: Attempted to divide by zero.
   at GreetingConsoleApp.FileGreetingWriter.Write(String message) in C:\Users\keenfann\Downloads\GreetingConsoleApp\GreetingConsoleApp\FileGreetingWriter.cs:line 28
2022-01-13 10:31:46.811 +01:00 [ERR] Cannot divide by zero!
System.DivideByZeroException: Attempted to divide by zero.
   at GreetingConsoleApp.FileGreetingWriter.Write(String message) in C:\Users\keenfann\Downloads\GreetingConsoleApp\GreetingConsoleApp\FileGreetingWriter.cs:line 28
2022-01-13 10:31:46.881 +01:00 [ERR] Cannot divide by zero! Exception was System.DivideByZeroException: Attempted to divide by zero.
   at GreetingConsoleApp.FileGreetingWriter.Write(String message) in C:\Users\keenfann\Downloads\GreetingConsoleApp\GreetingConsoleApp\FileGreetingWriter.cs:line 28
2022-01-13 10:35:32.580 +01:00 [ERR] Cannot divide by zero!

System.DivideByZeroException: Attempted to divide by zero.
   at GreetingConsoleApp.FileGreetingWriter.Write(String message) in C:\Users\keenfann\Downloads\GreetingConsoleApp\GreetingConsoleApp\FileGreetingWriter.cs:line 28
2022-01-13 10:35:32.653 +01:00 [ERR] Cannot divide by zero! Exception was System.DivideByZeroException: Attempted to divide by zero.
   at GreetingConsoleApp.FileGreetingWriter.Write(String message) in C:\Users\keenfann\Downloads\GreetingConsoleApp\GreetingConsoleApp\FileGreetingWriter.cs:line 28

2022-01-13 10:38:04.521 +01:00 [ERR] [Context] Cannot divide by zero!
System.DivideByZeroException: Attempted to divide by zero.
   at GreetingConsoleApp.FileGreetingWriter.Write(String message) in C:\Users\keenfann\Downloads\GreetingConsoleApp\GreetingConsoleApp\FileGreetingWriter.cs:line 29
2022-01-13 10:38:04.584 +01:00 [ERR] [Context] Cannot divide by zero! Exception was System.DivideByZeroException: Attempted to divide by zero.
   at GreetingConsoleApp.FileGreetingWriter.Write(String message) in C:\Users\keenfann\Downloads\GreetingConsoleApp\GreetingConsoleApp\FileGreetingWriter.cs:line 29

```