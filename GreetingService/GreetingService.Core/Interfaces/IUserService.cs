using GreetingService.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreetingService.Core.Interfaces
{
    public interface IUserService
    {
        public bool IsValidUser(string username, string password);
        public User GetUser(string email);
        public IEnumerable<User> GetUsers();
        public void CreateUser(User user);
        public void UpdateUser(User user);
        public void DeleteUser(string email);
    }
}
