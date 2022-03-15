Support writing greetings in batches. Add multiple greetings into a `Collection` and write all greetings in the collection at the same time.

### Goal
- Collections
- Iterations (loops)

### Steps
1. In `Program.cs` create a new variable with `var greetings = new List<Greeting>()`
2. Create multiple `Greeting` objects and add these to `greetings` variable
3. Iterate over all objects in `greetings` and write the message
4. Refactor the logic into two methods: 
    - `public static void ProcessGreeting(Greeting greeting)`
    - `public static void ProcessGreetings(List<Greeting> greetings)`
5. Call these from `Main()`
6. Implement a new method `public static List<Greeting> GenerateGreetings(int count)`
7. Generate 1000 greetings and process them in `ProcessGreetings()` method

### Example output
```console
03/12/2021 15:37:13:
How are you?

### PROCESSING BATCH OF 3 GREETING(S) ###
03/12/2021 15:37:13:
How are you?

03/12/2021 15:37:13:
Happy new year!
Looking forward to 2022!

03/12/2021 15:37:13:
Merry Christmas!
Here's your present: SDNA13215


Done!
```

### Example output end of 1000 greetings batch
```console
...
03/12/2021 15:45:17:
This is greeting no 993

03/12/2021 15:45:17:
This is greeting no 994

03/12/2021 15:45:17:
This is greeting no 995

03/12/2021 15:45:17:
This is greeting no 996

03/12/2021 15:45:17:
This is greeting no 997

03/12/2021 15:45:17:
This is greeting no 998

03/12/2021 15:45:17:
This is greeting no 999

03/12/2021 15:45:17:
This is greeting no 1000

### FINISHED PROCESSING BATCH OF 1000 GREETING(S) ###

Done!
```
