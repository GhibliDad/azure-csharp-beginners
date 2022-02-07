using Azure.Storage.Blobs;
using GreetingService.Core.Entities;
using GreetingService.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace GreetingService.Infrastructure.GreetingRepository
{
    public class BlobGreetingRepository : IGreetingRepository
    {
        private const string _blobContainerName = "greetings";              //we'll use a hardcoded name for the container for our blobs
        private readonly BlobContainerClient _blobContainerClient;          //we will reuse the same client in each instance of this class
        private readonly JsonSerializerOptions _jsonSerializerOptions = new() { WriteIndented = true };

        public BlobGreetingRepository(IConfiguration configuration)                 //ask for an IConfiguration here and dependency injection will provide it for us
        {
            var connectionString = configuration["LoggingStorageAccount"];          //get connection string from our app configuration
            _blobContainerClient = new BlobContainerClient(connectionString, _blobContainerName);
            _blobContainerClient.CreateIfNotExists();                               //create the container if it does not already exist
        }

        public async Task CreateAsync(Greeting greeting)
        {
            var path = $"{greeting.From}/{greeting.To}/{greeting.Id}";
            var blob = _blobContainerClient.GetBlobClient(path);              //get a reference to the blob using Greeting.ID as blob name
            if (await blob.ExistsAsync())
                throw new Exception($"Greeting with id: {greeting.Id} already exists");

            var greetingBinary = new BinaryData(greeting, _jsonSerializerOptions);
            await blob.UploadAsync(greetingBinary);
        }

        //More info about IAsyncEnumerable here: https://docs.microsoft.com/en-us/archive/msdn-magazine/2019/november/csharp-iterating-with-async-enumerables-in-csharp-8
        public async Task<Greeting> GetAsync(Guid id)
        {
            var blobs = _blobContainerClient.GetBlobsAsync();
            var blob = await blobs.FirstOrDefaultAsync(x => x.Name.EndsWith(id.ToString()));            //Special method FirstOrDefaultAsync requires System.Linq.Async nuget package. Use these methods to query IAsyncEnumerable

            if (blob == null)
                throw new Exception($"Greeting with id: {id} not found");

            var blobClient = _blobContainerClient.GetBlobClient(blob.Name);
            var blobContent = await blobClient.DownloadContentAsync();
            var greeting = blobContent.Value.Content.ToObjectFromJson<Greeting>();
            return greeting;
        }

        public async Task<IEnumerable<Greeting>> GetAsync()
        {
            var greetings = new List<Greeting>();
            var blobs = _blobContainerClient.GetBlobsAsync();                           //GetBlobsAsync return an AsyncPageable which is an IAsyncEnumerable<T>. This type is a bit special, check out the await foreach on the next line
            await foreach (var blob in blobs)                                           //this is how we can asynchronously iterate and process data in an IAsyncEnumerable<T>
            {
                var blobClient = _blobContainerClient.GetBlobClient(blob.Name);
                var blobContent = await blobClient.DownloadContentAsync();              //downloading lots of blobs like this will be slow, a more common scenario would be to list metadata for each blob and then download one or more blobs on demand instead of by default downloading all blobs. But we'll roll with this solution in this exercise
                var greeting = blobContent.Value.Content.ToObjectFromJson<Greeting>();
                greetings.Add(greeting);
            }

            return greetings;
        }

        public async Task UpdateAsync(Greeting greeting)
        {
            //since we're adding To and From to the blob name (path), an updated greeting could potentially change the blob name, we need to first remove to old greeting and then create the new Greeting when the new path

            var previousGreeting = await GetAsync(greeting.Id);

            var previousGreetingPath = $"{previousGreeting.From}/{previousGreeting.To}/{previousGreeting.Id}";
            var previousGreetingBlobClient = _blobContainerClient.GetBlobClient(previousGreetingPath);
            await previousGreetingBlobClient.DeleteAsync();

            var newGreetingPath = $"{greeting.From}/{greeting.To}/{greeting.Id}";
            var newGreetingBinary = new BinaryData(greeting, _jsonSerializerOptions);
            var newGreetingBlobClient = _blobContainerClient.GetBlobClient(newGreetingPath);
            await previousGreetingBlobClient.UploadAsync(newGreetingBinary);
        }
    }
}
