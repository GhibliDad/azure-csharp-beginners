We want to be able to export all greetings from `GreetingService` and save them as `XML` to simulate an integration scenario where a down stream legacy application can only import `XML` files.

### Goal
- REST API
- Transform `JSON` to `XML`

### Steps
1. Implement a new command in `GreetingService.API.Client` that exports all greetings to an `xml` file on disk

### Example output
```xml
<?xml version="1.0" encoding="utf-8"?>
<ArrayOfGreeting xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema">
  <Greeting>
    <id>7f2c1599-a933-49de-b126-80853db5abca</id>
    <message>hello</message>
    <from>Batman</from>
    <to>Superman</to>
    <timestamp>2021-12-15T11:02:33.0242599+01:00</timestamp>
  </Greeting>
  <Greeting>
    <id>529d0ba0-38ef-4501-a22f-48e067e6c4ac</id>
    <message>hello</message>
    <from>Batman</from>
    <to>Superman</to>
    <timestamp>2021-12-15T11:02:33.0242856+01:00</timestamp>
  </Greeting>
  <Greeting>
    <id>316244e1-5681-4e61-8706-2e14dd83445a</id>
    <message>hello</message>
    <from>Batman</from>
    <to>Superman</to>
    <timestamp>2021-12-15T11:02:33.0242904+01:00</timestamp>
  </Greeting>
</ArrayOfGreeting>
```