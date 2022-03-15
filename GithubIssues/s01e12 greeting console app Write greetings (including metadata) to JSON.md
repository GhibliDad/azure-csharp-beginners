 It's too hard to parse and work with `.log` file created by the current `FileGreetingWriter`. We need to save the greetings in a structured `json` file. We'll also implement method to write batches of greetings to the same file.

### Goal
- JSON
- Serialization

### Steps
1. Add method: `public void Write(IEnumerable<Greeting> greetings);` to `IGreetingWriter`
  - Implement this method in existing `GreetingWriter` classes
1. Create new file: `JsonGreetingWriter` and implement `IGreetingWriter`
2. Add a using statement:
  - `using System.Text.Json;`
  - `using Microsoft.Extensions.Configuration;`
3. Implement config logic to read config from `appsettings.json`. Duplicate the code for now from previous places
4. Implement the methods in the interface `IGreetingWriter`
5. Use `JsonSerializer.Serialize()` method to serialize all greetings to a `string`
6. Write the content of the `string` to a file on disk
  - We want to save each and every greeting in one or more files, how can we build the filename to avoid overwriting existing files?
7. Update `CreateGreetingWriter()` to create our new `JsonGreetingWriter`
8. Update `appsettings.json` to use `JsonGreetingWriter`
  - Also update filename
9. It's hard for a human to read unindented `json` files, use `JsonSerializerOptions` to configure the serializer to indent the `json` content when serializing
10. Test our logic with `GenerateGreetings()` method

### Example output
```json
[
  {
    "Message": "This is greeting no 1",
    "From": null,
    "To": null,
    "Timestamp": "2021-12-08T15:00:58.607047+01:00"
  },
  {
    "Message": "This is greeting no 2",
    "From": null,
    "To": null,
    "Timestamp": "2021-12-08T15:00:58.607052+01:00"
  },
  {
    "Message": "This is greeting no 3",
    "From": null,
    "To": null,
    "Timestamp": "2021-12-08T15:00:58.607052+01:00"
  },
  {
    "Message": "This is greeting no 4",
    "From": null,
    "To": null,
    "Timestamp": "2021-12-08T15:00:58.607052+01:00"
  },
  {
    "Message": "This is greeting no 5",
    "From": null,
    "To": null,
    "Timestamp": "2021-12-08T15:00:58.607052+01:00"
  }
]
```