using Azure.Storage.Blobs;
using GreetingService.Core.Entities;
using GreetingService.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreetingService.Infrastructure.UserService
{
    public class BlobUserService : IUserService
    {
        private const string _blobContainerName = "users";
        private const string _blobName = "users.json";
        private readonly BlobContainerClient _blobContainerClient;

        public BlobUserService(IConfiguration configuration)
        {
            var connectionString = configuration["LoggingStorageAccount"];          //get connection string from our app configuration
            _blobContainerClient = new BlobContainerClient(connectionString, _blobContainerName);
            _blobContainerClient.CreateIfNotExists();                               //create the container if it does not already exist
        }

        public Task ApproveUserAsync(string approvalCode)
        {
            throw new NotImplementedException();
        }

        public async Task CreateUserAsync(User user)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteUserAsync(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUserAsync(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            throw new NotImplementedException();
        }

        public bool IsValidUser(string username, string password)
        {
            var blob = _blobContainerClient.GetBlobClient(_blobName);

            if (!blob.Exists())
                return false;

            var blobContent = blob.DownloadContent();
            var usersDictionary = blobContent.Value.Content.ToObjectFromJson<IDictionary<string, string>>();

            if (usersDictionary.TryGetValue(username, out var storedPassword))
            {
                if (storedPassword.Equals(password))
                    return true;
            }

            return false;
        }

        public async Task<bool> IsValidUserAsync(string username, string password)
        {
            var blob = _blobContainerClient.GetBlobClient(_blobName);

            if (!await blob.ExistsAsync())
                return false;

            var blobContent = await blob.DownloadContentAsync();
            var usersDictionary = blobContent.Value.Content.ToObjectFromJson<IDictionary<string, string>>();

            if (usersDictionary.TryGetValue(username, out var storedPassword))
            {
                if (storedPassword.Equals(password))
                    return true;
            }

            return false;
        }

        public Task RejectUserAsync(string approvalCode)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateUserAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
