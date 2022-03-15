A `query string` in a `uri` is the part after the question mark: `?` in the `uri`. 

In this uri:
`http://localhost:7071/api/greeting?from=keen&to=anton`

The query string is:
`from=keen&to=anton`

We can use query strings to send more parameters to an `http` endpoint. A query string is a `key=value` pair and multiple pairs are separated with `&` character.

Get the values from the `query string` in an `Azure Function` method like this:
`var from = req.Query["from"]`

Note that `from` and `to` are optional in the query string, these values can be `null`. Make sure we support all combinations:
* Both `to` & `from` have no values
* `to` has no value & `from` has a value
* `to` has value & `from` has no value
* Both `to` & `from` have values

### Goal
* Azure Blob Store
* Blob name hierarchy using `/`
* Query string parameters

### Steps
1. Add a new method: `public Task<IEnumerable<Greeting>> GetAsync(string from, string to);` to `IGreetingRepository`
2. Implement this method in all classes that implement `IGreetingRepository`
    - In `FileGreetingRepository` & `MemoryGreetingRepository` you can use `LINQ` queries to return relevant `Greeting` objects
    - In `BlobGreetingRepository` you can use `prefix` to filter `blobs` by `prefix`
4. Add support for query string parameters to `GetGreetings` Function as filters:
    - Query string parameter `to` should be optional and adding this should only return `Greetings` with `To` = this value
    - Query string parameter `from` should be optional and adding this should only return `Greetings` with `From` = this value


### Example output
`http://localhost:7071/api/greeting?from=Kalle` return all greetings from Kalle:
```json
[
    {
        "id": "f42c3764-740c-4b0a-b430-4823479d4937",
        "message": "Hello",
        "from": "Kalle",
        "to": "Anka",
        "timestamp": "2022-02-07T20:45:26.7440893+01:00"
    },
    {
        "id": "eeb77863-9537-48d6-a782-12c47733c2f1",
        "message": "Hello",
        "from": "Kalle",
        "to": "Kajsa",
        "timestamp": "2022-02-07T22:07:17.2522733+01:00"
    }
]
```

`http://localhost:7071/api/greeting?to=Kajsa` returns all greetings to Kajsa:
```json
[
    {
        "id": "eeb77863-9537-48d6-a782-12c47733c2f1",
        "message": "Hello",
        "from": "Kalle",
        "to": "Kajsa",
        "timestamp": "2022-02-07T22:07:17.2522733+01:00"
    },
    {
        "id": "48835f44-f41c-4388-a7c5-8c4827f42432",
        "message": "Hello",
        "from": "Keen",
        "to": "Kajsa",
        "timestamp": "2022-02-07T20:46:17.5511643+01:00"
    },
    {
        "id": "281152d3-5e13-47fc-abb1-910287efe15b",
        "message": "Hello",
        "from": "Mimmi",
        "to": "Kajsa",
        "timestamp": "2022-02-07T20:45:41.7317211+01:00"
    }
]
```

`http://localhost:7071/api/greeting?from=Kalle&to=Kajsa` returns all greetings from Kalle to Kajsa:
```json
[
    {
        "id": "eeb77863-9537-48d6-a782-12c47733c2f1",
        "message": "Hello",
        "from": "Kalle",
        "to": "Kajsa",
        "timestamp": "2022-02-07T22:07:17.2522733+01:00"
    }
]
```

`http://localhost:7071/api/greeting` returns all greetings:
```json
[
    {
        "id": "f42c3764-740c-4b0a-b430-4823479d4937",
        "message": "Hello",
        "from": "Kalle",
        "to": "Anka",
        "timestamp": "2022-02-07T20:45:26.7440893+01:00"
    },
    {
        "id": "eeb77863-9537-48d6-a782-12c47733c2f1",
        "message": "Hello",
        "from": "Kalle",
        "to": "Kajsa",
        "timestamp": "2022-02-07T22:07:17.2522733+01:00"
    },
    {
        "id": "139cccd6-0f03-4060-a5e5-c8dbde4465fa",
        "message": "Hello",
        "from": "Keen",
        "to": "Anton",
        "timestamp": "2022-02-07T20:26:27.8259725+01:00"
    },
    {
        "id": "8a5785c3-bec8-4e50-a4d2-342ae6e1868f",
        "message": "Hello",
        "from": "Keen",
        "to": "Anton",
        "timestamp": "2022-02-07T20:26:33.9628641+01:00"
    },
    {
        "id": "915bf7ec-0ede-4f2a-a3c6-c69b01d808c5",
        "message": "Hello",
        "from": "Keen",
        "to": "Anton",
        "timestamp": "2022-02-07T14:28:40.8793644+01:00"
    },
    {
        "id": "95f273b5-0f37-469e-8db3-5533c9f0c2ad",
        "message": "Hello",
        "from": "Keen",
        "to": "Anton",
        "timestamp": "2022-02-07T20:31:40.3198288+01:00"
    },
    {
        "id": "98f8d411-7129-4479-987c-bd1c9d03d6e8",
        "message": "Hello",
        "from": "Keen",
        "to": "Anton",
        "timestamp": "2022-02-07T20:40:03.2934016+01:00"
    },
    {
        "id": "b8acf391-d156-4a1a-bffa-3ab304c30126",
        "message": "Hello",
        "from": "Keen",
        "to": "Anton",
        "timestamp": "2022-02-07T20:28:26.7022716+01:00"
    },
    {
        "id": "bac56539-f357-498d-ba99-8607ddcb5b45",
        "message": "Hello",
        "from": "Keen",
        "to": "Anton",
        "timestamp": "2022-02-07T20:33:09.1578832+01:00"
    },
    {
        "id": "d6ba7caf-72c5-4bbe-9ba5-9bf5149ef701",
        "message": "Hello",
        "from": "Keen",
        "to": "Anton",
        "timestamp": "2022-02-07T20:27:00.6939797+01:00"
    },
    {
        "id": "e8d56a06-0370-4c33-be3d-82b6bc52773d",
        "message": "Hello",
        "from": "Keen",
        "to": "Anton",
        "timestamp": "2022-02-07T20:32:10.6148483+01:00"
    },
    {
        "id": "48835f44-f41c-4388-a7c5-8c4827f42432",
        "message": "Hello",
        "from": "Keen",
        "to": "Kajsa",
        "timestamp": "2022-02-07T20:46:17.5511643+01:00"
    },
    {
        "id": "281152d3-5e13-47fc-abb1-910287efe15b",
        "message": "Hello",
        "from": "Mimmi",
        "to": "Kajsa",
        "timestamp": "2022-02-07T20:45:41.7317211+01:00"
    }
]
```