Another common way to exchange structured data is with a `csv` (comma separated values). A nice property of `csv` files is that they are easy to open in `Excel`.

### Goal
- CSV
- Serialization

### Steps
1. Create new file: `CsvGreetingWriter.cs` and implement interface `IGreetingWriter`
2. Build the string one line at a time, first line should contain headers
  - use semicolon `;` as separator

### Example output
```csv
timestamp;from;to;message
08/12/2021 16:17:56;Keen;Anton;This is greeting no 1
08/12/2021 16:17:56;Keen;Anton;This is greeting no 2
08/12/2021 16:17:56;Keen;Anton;This is greeting no 3
08/12/2021 16:17:56;Keen;Anton;This is greeting no 4
08/12/2021 16:17:56;Keen;Anton;This is greeting no 5
08/12/2021 16:17:56;Keen;Anton;This is greeting no 6
08/12/2021 16:17:56;Keen;Anton;This is greeting no 7
08/12/2021 16:17:56;Keen;Anton;This is greeting no 8
08/12/2021 16:17:56;Keen;Anton;This is greeting no 9
08/12/2021 16:17:56;Keen;Anton;This is greeting no 10
08/12/2021 16:17:56;Keen;Anton;This is greeting no 11
08/12/2021 16:17:56;Keen;Anton;This is greeting no 12
08/12/2021 16:17:56;Keen;Anton;This is greeting no 13
08/12/2021 16:17:56;Keen;Anton;This is greeting no 14
08/12/2021 16:17:56;Keen;Anton;This is greeting no 15
08/12/2021 16:17:56;Keen;Anton;This is greeting no 16
08/12/2021 16:17:56;Keen;Anton;This is greeting no 17
08/12/2021 16:17:56;Keen;Anton;This is greeting no 18
08/12/2021 16:17:56;Keen;Anton;This is greeting no 19
08/12/2021 16:17:56;Keen;Anton;This is greeting no 20
08/12/2021 16:17:56;Keen;Anton;This is greeting no 21
08/12/2021 16:17:56;Keen;Anton;This is greeting no 22
08/12/2021 16:17:56;Keen;Anton;This is greeting no 23
08/12/2021 16:17:56;Keen;Anton;This is greeting no 24
08/12/2021 16:17:56;Keen;Anton;This is greeting no 25
08/12/2021 16:17:56;Keen;Anton;This is greeting no 26
08/12/2021 16:17:56;Keen;Anton;This is greeting no 27
08/12/2021 16:17:56;Keen;Anton;This is greeting no 28
08/12/2021 16:17:56;Keen;Anton;This is greeting no 29
08/12/2021 16:17:56;Keen;Anton;This is greeting no 30
08/12/2021 16:17:56;Keen;Anton;This is greeting no 31
08/12/2021 16:17:56;Keen;Anton;This is greeting no 32
08/12/2021 16:17:56;Keen;Anton;This is greeting no 33
08/12/2021 16:17:56;Keen;Anton;This is greeting no 34
```