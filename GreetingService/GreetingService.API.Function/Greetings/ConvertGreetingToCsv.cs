using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using GreetingService.Core.Entities;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace GreetingService.API.Function.Greetings
{
    public class ConvertGreetingToCsv
    {
        //'greetings/{name}' is the path to the blobs we want to trigger on, greetings is the container name and {name} is the blob name inside that container
        //'Connection = "LoggingStorageAccount"' tells Azure Functions where it can find the connection string to the storage account, "LoggingStorageAccount" is an app setting from earlier exercises where we stored our connection string, since we'll be using the same storage account we can use the same app setting key 
        //'Stream greetingJsonBlob' will contain the blob that triggered this function (it should contain a Greeting in json format)
        //'[Blob("greetings-csv/{name}", FileAccess.Write)] Stream greetingCsvBlob' is an output binding in Azure Functions, writing to this stream will create a blob in the container: 'greetings-csv' with the same name as our original greeting
        //'string name' will contain the blob name 
        [FunctionName("ConvertGreetingToCsv")]
        public async Task Run([BlobTrigger("greetings/{name}", Connection = "LoggingStorageAccount")] Stream greetingJsonBlob, string name, [Blob("greetings-csv/{name}", FileAccess.Write, Connection = "LoggingStorageAccount")] Stream greetingCsvBlob, ILogger log)
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Size: {greetingJsonBlob.Length} Bytes");

            var greeting = JsonSerializer.Deserialize<Greeting>(greetingJsonBlob);    //Deserialize the incoming Stream containing Greeting json

            var streamWriter = new StreamWriter(greetingCsvBlob);       //use a StreamWriter to write to a Stream       
            streamWriter.WriteLine("id;from;to;message;timestamp");     //write header row in csv
            streamWriter.WriteLine($"{greeting.Id};{greeting.From};{greeting.To};{greeting.Message};{greeting.Timestamp}");     //write header row in csv
            await streamWriter.FlushAsync();                            //ensure that all data is written before closing the stream
        }
    }
}
