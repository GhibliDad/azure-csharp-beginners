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

namespace GreetingService.Infrastructure
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
            var blob = _blobContainerClient.GetBlobClient(greeting.Id.ToString());              //get a reference to the blob using Greeting.ID as blob name
            if (await blob.ExistsAsync())
                throw new Exception($"Greeting with id: {greeting.Id} already exists");

            var greetingBinary = new BinaryData(greeting, _jsonSerializerOptions);
            await blob.UploadAsync(greetingBinary);
        }

        public async Task<Greeting> GetAsync(Guid id)
        {
            var blobClient = _blobContainerClient.GetBlobClient(id.ToString());
            if (!await blobClient.ExistsAsync())
                throw new Exception($"Greeting with id: {id} not found");

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
            var blobClient = _blobContainerClient.GetBlobClient(greeting.Id.ToString());
            await blobClient.DeleteIfExistsAsync();
            var greetingBinary = new BinaryData(greeting, _jsonSerializerOptions);
            await blobClient.UploadAsync(greetingBinary);
        }
    }
}
