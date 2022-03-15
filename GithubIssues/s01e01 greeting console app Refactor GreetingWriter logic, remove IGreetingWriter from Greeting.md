It doesn't make sense for each `Greeting` to have it's own reference to an `IGreetingWriter`. Refactor the code to remove `IGreetingWriter` from `Greeting` and update the logic to always use the configured writer.

Refactoring code is the process of rewriting (refactoring) existing code. Examples of when we refactor code:
* Logic has changed/grown so much extracting logic into its own `method` makes it easier to read/debug
* External dependencies have changed forcing us to adapt (refactor) our code to work with the changes
* New feature requires changes in existing code to make sense
* Bug fixes
* Existing code works but is hard to read/understand/follow/debug
* ...

There are many reasons to refactor code and this is a very common activity for developers. It's a lot easier to refactor code if there are automated tests (unit tests etc) that cover the parts we want to refactor.

### Goal
- Refactoring code

### Steps
1. Organize our files into folders
  - Move all `GreetingWriter` files into a new folder named `GreetingWriter`
  - Move all `Greeting` files into a new folder named `Greetings`
2. Remove `GreetingWriters` property from `Greeting`
3. Remove `WriteMessage()` method from `Greeting`
4. Add method `public void Write(Greeting greeting);` to `IGreetingWriter` interface
5. Implement the new method in all classes that implements `IGreetingWriter`
6. Create a `IGreetingWriter` in the constructor of `Program` and save this a property `_greetingWriter`. We'll use this writer in the lifetime of the application
7. Fix compile errors in all files
