Bad practice to duplicate code too much. Also takes time to rewrite same logic repeatedly. Refactor `Settings` logic and reuse in all places.

### Goal
- Refactor code

### Steps
1. Create new method `public static Settings InitializeSettings()` in `Program.cs`
2. Move `Settings` logic to this method
3. Replace duplicated code with call to this method