Try to read a `json` file containing greetings and print them on the console. To do this we have to `deserialize` a string to our object.

### Goal
- JSON
- Deserialization

### Steps
1.  Create new file named `GreetingReader` (we'll skip building with interface `IGreetingReader` for now)
2. Create method: `public IEnumerable<Greeting> ReadGreetingsFromFile(string path)`
2. Use method `File.ReadAllText()` to read a file and store contents in a `string` variable
3. Use method `JsonSerializer.Deserialize()` to deserialize a `string` to a `IEnumerable<Greeting>` variable
4. Add exception handling to handle errors (e.g. file not found, file is not a valid json with greetings etc)
  - Return empty collection on error

### Example output
```console
08/12/2021 16:38:45:
This is greeting no 95

08/12/2021 16:38:45:
This is greeting no 96

08/12/2021 16:38:45:
This is greeting no 97

08/12/2021 16:38:45:
This is greeting no 98

08/12/2021 16:38:45:
This is greeting no 99

08/12/2021 16:38:45:
This is greeting no 100

Done!
```