using GreetingService.Core.Entities;
using GreetingService.Core.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreetingService.Infrastructure.UserService
{
    public class AppSettingsUserService : IUserService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<AppSettingsUserService> _logger;

        public AppSettingsUserService(IConfiguration configuration, ILogger<AppSettingsUserService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public Task ApproveUserAsync(string approvalCode)
        {
            throw new NotImplementedException();
        }

        public Task BeginUserApprovalAsync(User user)
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
            var entries = _configuration.AsEnumerable().ToDictionary(x => x.Key, x => x.Value);
            if (entries.TryGetValue(username, out var storedPassword))
            {
                if (storedPassword == password)
                {
                    _logger.LogInformation("Valid credentials for {username}", username);
                    return true;
                }
            }

            _logger.LogWarning("Invalid credentials for {username}", username);
            return false;
        }

        public async Task<bool> IsValidUserAsync(string username, string password)
        {
            return IsValidUser(username, password);
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
