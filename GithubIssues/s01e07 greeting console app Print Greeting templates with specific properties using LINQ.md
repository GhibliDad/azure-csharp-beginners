Add filter methods to print Greeting templates with specific properties using LINQ, Lambda, Foreach loops. LINQ and Lambda expressions are concepts that we haven't encountered before and will take some effort to understand. We will achieve the same results with three different methods to get a feel for each method.

LINQ: 
https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/

Lambda expressions: 
https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/lambda-expressions

### Goal
- LINQ
- Lambda

### Steps
1. In `GreetingTemplateRepository` class implement 3 methods to get templates from `GreetingTemplates` with message longer than an input value using:
    - LINQ
    - Lambda
    - Foreach

2. In `GreetingTemplateRepository` class implement 3 methods to get templates from `GreetingTemplates` with message containing a input string using:
    - LINQ
    - Lambda
    - Foreach

3. In `Program` class implement a method named `PrintTemplatesWithLinq()` that prints the result of all above methods

4. Implement more methods and try out different types of queries/filter.

### Example output
```console
Messages >= 29 - LINQ
07/12/2021 09:59:08:
A generic christmas greeting!
Here's your present: DSAN13284

Messages >= 29 - Lambda expression
07/12/2021 09:59:08:
A generic christmas greeting!
Here's your present: DSAN13284

Messages >= 29 - Foreach
07/12/2021 09:59:08:
A generic christmas greeting!
Here's your present: DSAN13284

Messages containing 'year' - LINQ
07/12/2021 09:59:08:
A generic new year greeting!
Looking forward to 2022!

Messages containing 'year' - Lambda
07/12/2021 09:59:08:
A generic new year greeting!
Looking forward to 2022!

Messages containing 'year' - Foreach
07/12/2021 09:59:08:
A generic new year greeting!
Looking forward to 2022!

Done!
```