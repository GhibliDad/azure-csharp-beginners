using GreetingService.Core.Entities;
using GreetingService.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreetingService.Infrastructure.UserService
{
    public class SqlUserService : IUserService
    {
        private readonly GreetingDbContext _greetingDbContext;
        private readonly ILogger<SqlUserService> _logger;

        public SqlUserService(GreetingDbContext greetingDbContext, ILogger<SqlUserService> logger)
        {
            _greetingDbContext = greetingDbContext;
            _logger = logger;
        }

        public async Task CreateUserAsync(User user)
        {
            user.Created = DateTime.Now;
            user.Modified = DateTime.Now;
            await _greetingDbContext.Users.AddAsync(user);
            await _greetingDbContext.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(string email)
        {
            var user = _greetingDbContext.Users.FirstOrDefault(x => x.Email.Equals(email));
            if (user == null)
            {
                _logger.LogWarning("Delete user failed, user with email {email} not found", email);
                throw new Exception();      //Consider throwing a custom not found exception instead
            }

            _greetingDbContext.Users.Remove(user);
            _greetingDbContext.SaveChanges();
        }

        public async Task<User> GetUserAsync(string email)
        {
            var user = await _greetingDbContext.Users.FirstOrDefaultAsync(x => x.Email.Equals(email));
            return user;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _greetingDbContext.Users.ToListAsync();
        }

        public async Task UpdateUserAsync(User user)
        {
            var existingUser = await _greetingDbContext.Users.FirstOrDefaultAsync(x => x.Email.Equals(user.Email));
            if (existingUser == null)
            {
                _logger.LogWarning("Update user failed, user with email {email} not found", user.Email);
                throw new Exception("User not found");      //Consider throwing a custom not found exception instead
            }

            if (!string.IsNullOrWhiteSpace(user.Password))
                existingUser.Password = user.Password;

            if (!string.IsNullOrWhiteSpace(user.LastName))
                existingUser.LastName = user.LastName;

            if (!string.IsNullOrWhiteSpace(user.FirstName))
                existingUser.FirstName = user.FirstName;

            existingUser.Modified = DateTime.Now;          
            await _greetingDbContext.SaveChangesAsync();
        }

        public async Task<bool> IsValidUserAsync(string username, string password)
        {
            var user = await _greetingDbContext.Users.FirstOrDefaultAsync(x => x.Email.Equals(username));
            if (user != null && user.Password.Equals(password))
                return true;

            return false;
        }

        public bool IsValidUser(string username, string password)
        {
            var user = _greetingDbContext.Users.FirstOrDefault(x => x.Email.Equals(username));
            if (user != null && user.Password.Equals(password))
                return true;

            return false;
        }
    }
}
