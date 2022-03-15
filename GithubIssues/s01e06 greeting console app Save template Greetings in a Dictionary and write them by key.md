Support saving some default greetings in a Dictionary that we can retrieve by key. Add multiple greetings into a `Dictionary` and write specific greetings from the by getting them by key.

### Goal
- Collections
- Dictionary
- Generics

### Steps
1. Create a new file named `GreetingTemplateRepository.cs` containing the class `GreetingTemplateRepository`
2. Add a property named `GreetingTemplates` with the type `Dictionary<int, Greeting>`
3. Implement a constructor for `GreetingTemplateRepository` that initialises `GreetingTemplates`
    - Also insert a couple `Greeting` objects into `GreetingTemplates` in the constructor
4. Implement method `public Greeting GetGreetingTemplate(int id)` that returns a `Greeting` from the repository
5. Implement method `public int SaveGreetingTemplate(int id, Greeting greeting)` that adds a `Greeting` to the repository
6. Add exception handling to method `GetGreetingTemplate()` when no template with input `id` exists
7. Add exception handling to method `SaveGreetingTemplate()` when input `id` already exists in the repository
8. Implement a new method named `PrintTemplate()` in `Program` that does the following:
    1. Prints available templates
    2. Asks the user to enter a key
    3. Prints the correct template with the entered key

### Example output
```console
Available templates:
ID: 1 - Message: A generic christmas greeting!
ID: 2 - Message: A generic new year greeting!

Enter template ID to print:
2
06/12/2021 14:49:18:
A generic new year greeting!
Looking forward to 2022!

Done!
```