using System.Net.Http.Json;
using System.Text.Json;
using System.Xml.Serialization;
using System.Xml;
using System.Text;
using System.Diagnostics;
using System.Threading.Tasks;

namespace GreetingService.API.Client;

public class GreetingServiceClient
{
    private static HttpClient _httpClient = new();                          //always reuse http client to avoid exhausting network resources. A static field is shared by all instances of a class

    private const string _getGreetingsCommand = "get greetings";
    private const string _getGreetingCommand = "get greeting ";
    private const string _writeGreetingCommand = "write greeting ";
    private const string _updateGreetingCommand = "update greeting ";
    private const string _exportGreetingsCommand = "export greetings";
    private const string _repeatingCallsCommand = "repeat calls ";
    private static string _from = "Batman";
    private static string _to = "Superman";

    public static async Task Main(string[] args)
    {
        var authParam = Convert.ToBase64String(Encoding.UTF8.GetBytes("keen:summer2025"));
        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", authParam);        //Always send this header for all requests from this HttpClient
        //_httpClient.BaseAddress = new Uri("http://localhost:5020/");
        //_httpClient.BaseAddress = new Uri("https://keen-appservice-dev.azurewebsites.net/");                                                //Always use this part of the uri in all requests sent from this HttpClient
        _httpClient.BaseAddress = new Uri("https://keen-function-dev.azurewebsites.net/");                                                //Always use this part of the uri in all requests sent from this HttpClient

        Console.WriteLine("Welcome to command line Greeting client");
        Console.WriteLine("Enter name of greeting sender:");
        var from = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(from))
            _from = from;

        Console.WriteLine("Enter name of greeting recipient:");
        var to = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(to))
            _to = to;

        while (true)
        {
            Console.WriteLine("\nAvailable commands:");
            Console.WriteLine(_getGreetingsCommand);
            Console.WriteLine($"{_getGreetingCommand} [id]");
            Console.WriteLine($"{_writeGreetingCommand} [message]");
            Console.WriteLine($"{_updateGreetingCommand} [id] [message]");
            Console.WriteLine($"{_updateGreetingCommand} [id] [message]");
            Console.WriteLine(_exportGreetingsCommand);
            Console.WriteLine($"{_repeatingCallsCommand} [count]");

            Console.WriteLine("\nWrite command and press [enter] to execute");

            var command = Console.ReadLine();

            if (string.IsNullOrEmpty(command))
            {
                Console.WriteLine("Command cannot be empty\n");
                continue;
            }

            if (command.Equals(_getGreetingsCommand, StringComparison.OrdinalIgnoreCase))
            {
                await GetGreetingsAsync();
            }
            else if (command.StartsWith(_getGreetingCommand, StringComparison.OrdinalIgnoreCase))
            {
                var idPart = command.Replace(_getGreetingCommand, "");
                if (Guid.TryParse(idPart, out var id))
                {
                    await GetGreetingAsync(id);
                }
                else
                {
                    Console.WriteLine($"{idPart} is not a valid GUID\n");
                }
            }
            else if (command.StartsWith(_writeGreetingCommand, StringComparison.OrdinalIgnoreCase))
            {
                var message = command.Replace(_writeGreetingCommand, "");
                await WriteGreetingAsync(message);
            }
            else if (command.StartsWith(_updateGreetingCommand, StringComparison.OrdinalIgnoreCase))
            {
                var idAndMessagePart = command.Replace(_updateGreetingCommand, "") ?? "";
                var idPart = idAndMessagePart.Split(" ").First();
                var messagePart = idAndMessagePart.Replace(idPart, "").Trim();

                if (Guid.TryParse(idPart, out var id))
                {
                    await UpdateGreetingAsync(id, messagePart);
                }
                else
                {
                    Console.WriteLine($"{idPart} is not a valid GUID");
                }
            }
            else if (command.Equals(_exportGreetingsCommand, StringComparison.OrdinalIgnoreCase))
            {
                await ExportGreetingsAsync();
            }
            else if (command.StartsWith(_repeatingCallsCommand))
            {
                var countPart = command.Replace(_repeatingCallsCommand, "");

                if (int.TryParse(countPart, out var count))
                {
                    await RepeatCallsAsync(count);
                }
                else
                {
                    Console.WriteLine($"Could not parse {countPart} as int");
                }
            }
            else
            {
                Console.WriteLine("Command not recognized\n");
            }
        }
    }

    /// <summary>
    /// async key word means this method is awaitable. Return type Task is equivalent to void in a sync (normal) method
    /// IO operations (network, disk, db, etc) are slow. Using await async allows the CPU to work on other operations while waiting for the async operation to return.
    /// In general use async methods if possible and follow the principle of "async all the way"
    /// </summary>
    /// <returns></returns>
    private static async Task<IEnumerable<Greeting>> GetGreetingsAsync()                                               //common naming convention is to add Async suffix to async method names
    {
        try
        {
            var response = await _httpClient.GetAsync("api/greeting");                          //since we have a set base address in HttpClient we only need to specify the last part here of the uri here.
            response.EnsureSuccessStatusCode();                                                 //throws exception if HTTP response status is not a success status
            var responseBody = await response.Content.ReadAsStringAsync();

            //Do something with response
            var greetings = JsonSerializer.Deserialize<IEnumerable<Greeting>>(responseBody);

            foreach (var greeting in greetings)
            {
                Console.WriteLine($"[{greeting.id}] [{greeting.timestamp}] ({greeting.from} -> {greeting.to}) - {greeting.message}");
            }

            Console.WriteLine();
            return greetings;
        }
        catch (Exception e)
        {
            Console.WriteLine($"Get greetings failed: {e.Message}\n");
        }

        return Enumerable.Empty<Greeting>();
    }

    private static async Task GetGreetingAsync(Guid id)
    {
        try
        {
            var response = await _httpClient.GetAsync($"api/greeting/{id}");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();

            //Do something with response;
            var greeting = JsonSerializer.Deserialize<Greeting>(responseBody);
            Console.WriteLine($"[{greeting.id}] [{greeting.timestamp}] ({greeting.from} -> {greeting.to}) - {greeting.message}\n");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Get greeting failed: {e.Message}\n");
        }
    }

    private static async Task WriteGreetingAsync(string message)
    {
        try
        {
            var greeting = new Greeting
            {
                from = _from,
                to = _to,
                message = message,
            };
            var response = await _httpClient.PostAsJsonAsync("api/greeting", greeting);
            Console.WriteLine($"Wrote greeting. Service responded with: {response.StatusCode}");            //all HTTP responses always contain a status code
            Console.WriteLine();
        }
        catch (Exception e)
        {
            Console.WriteLine($"Write greeting failed: {e.Message}\n");
        }
    }

    private static async Task UpdateGreetingAsync(Guid id, string message)
    {
        try
        {
            var greeting = new Greeting
            {
                id = id,
                from = _from,
                to = _to,
                message = message,
            };
            var response = await _httpClient.PutAsJsonAsync("api/greeting", greeting);
            Console.WriteLine($"Updated greeting. Service responded with: {response.StatusCode}");
        }
        catch (Exception e)
        {
            Console.WriteLine($"Update greeting failed: {e.Message}\n");
        }
    }

    private static async Task ExportGreetingsAsync()
    {
        var response = await _httpClient.GetAsync("api/greeting");
        response.EnsureSuccessStatusCode();                                                 //throws exception if HTTP response status is not a success status
        var responseBody = await response.Content.ReadAsStringAsync();
        var greetings = JsonSerializer.Deserialize<List<Greeting>>(responseBody);

        var filename = "greetingExport.xml";
        var xmlWriterSettings = new XmlWriterSettings
        {
            Indent = true,
        };
        using var xmlWriter = XmlWriter.Create(filename, xmlWriterSettings);
        var serializer = new XmlSerializer(typeof(List<Greeting>));                             //this xml serializer does not support serializing interfaces, need to convert to a concrete class
        serializer.Serialize(xmlWriter, greetings);                                   //convert our greetings of type IEnumerable (interface) to List (concrete class)

        Console.WriteLine($"Exported {greetings.Count()} greetings to {filename}\n");
    }

    private static async Task RepeatCallsAsync(int count)
    {
        var greetings = await GetGreetingsAsync();
        var greeting = greetings.First();

        //init a jobs list
        var jobs = new List<int>();
        for (int i = 0; i < count; i++)
        {
            jobs.Add(i);
        }

        var stopwatch = Stopwatch.StartNew();           //use stopwatch to measure elapsed time just like a real world stopwatch

        //I cheat by running multiple calls in parallel for maximum throughput - we will be limited by our cpu, wifi, internet speeds
        //This is a bit advanced and the syntax is new with lamdas - don't worry if you don't understand all of it.
        //I always copy this from the internet and adapt to my needs
        //Running this in Visual Studio debugger is slow, try running .exe file directly from File Explorer or command line prompt
        await Parallel.ForEachAsync(jobs, new ParallelOptions { MaxDegreeOfParallelism = 50 }, async (job, token) =>
        {
            var start = stopwatch.ElapsedMilliseconds;
            var response = await _httpClient.GetAsync($"api/greeting/{greeting.id}");
            var end = stopwatch.ElapsedMilliseconds;

            Console.WriteLine($"Response: {response.StatusCode} - Call: {job} - latency: {end - start} ms - rate/s: {job / stopwatch.Elapsed.TotalSeconds}");
        });
    }
}