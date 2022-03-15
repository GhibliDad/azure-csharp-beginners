Some application can only support `XML` files. Implement `XmlGreetingWriter` in a similar way as `JsonGreetingWriter` that outputs `xml` instead of `json`.

### Goal
- XML
- Serialization

### Steps
1. Create file `XmlGreetingWriter.cs`
2. Implement `IGreetingWriter`
3. Add using statements: 
  - `using System.Xml.Serialization;`
  - `using Microsoft.Extensions.Configuration;`
4. Use this code to serialize to `xml` (don't memorize, always google this stuff)
```c#
var xmlWriterSettings = new XmlWriterSettings 
{
    Indent = true,
};
using var xmlWriter = XmlWriter.Create(_settings.GreetingWriterOutputFilePath, xmlWriterSettings);
var serializer = new XmlSerializer(typeof(List<Greeting>));                             //this xml serializer does not support serializing interfaces, need to convert to a concrete class
serializer.Serialize(xmlWriter, greetings.ToList());                                   //convert our greetings of type IEnumerable (interface) to List (concrete class)
        
Console.WriteLine($"Wrote {greetings.Count()} greeting(s) to {filename}");
```

### Example output
```xml
<?xml version="1.0" encoding="utf-8"?>
<ArrayOfGreeting xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <Greeting>
    <Message>This is greeting no 1</Message>
    <From>Keen</From>
    <To>Anton</To>
    <Timestamp>2021-12-08T15:32:25.427167+01:00</Timestamp>
  </Greeting>
  <Greeting>
    <Message>This is greeting no 2</Message>
    <From>Keen</From>
    <To>Anton</To>
    <Timestamp>2021-12-08T15:32:25.427255+01:00</Timestamp>
  </Greeting>
  <Greeting>
    <Message>This is greeting no 3</Message>
    <From>Keen</From>
    <To>Anton</To>
    <Timestamp>2021-12-08T15:32:25.427256+01:00</Timestamp>
  </Greeting>
  <Greeting>
    <Message>This is greeting no 4</Message>
    <From>Keen</From>
    <To>Anton</To>
    <Timestamp>2021-12-08T15:32:25.427256+01:00</Timestamp>
  </Greeting>
  <Greeting>
    <Message>This is greeting no 5</Message>
    <From>Keen</From>
    <To>Anton</To>
    <Timestamp>2021-12-08T15:32:25.427256+01:00</Timestamp>
  </Greeting>
</ArrayOfGreeting>
```