using GreetingService.Core.Entities;
using GreetingService.Core.Interfaces;
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

        public void CreateUser(User user)
        {
            user.Created = DateTime.Now;
            user.Modified = DateTime.Now;
            _greetingDbContext.Users.Add(user);
            _greetingDbContext.SaveChanges();
        }

        public void DeleteUser(string email)
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

        public User GetUser(string email)
        {
            var user = _greetingDbContext.Users.FirstOrDefault(x => x.Email.Equals(email));
            return user;
        }

        public IEnumerable<User> GetUsers()
        {
            return _greetingDbContext.Users.ToList();
        }

        public void UpdateUser(User user)
        {
            var existingUser = _greetingDbContext.Users.FirstOrDefault(x => x.Email.Equals(user.Email));
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
            _greetingDbContext.SaveChanges();
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
