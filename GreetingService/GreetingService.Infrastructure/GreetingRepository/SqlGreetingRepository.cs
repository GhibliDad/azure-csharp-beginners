using GreetingService.Core.Entities;
using GreetingService.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreetingService.Infrastructure.GreetingRepository
{
    public class SqlGreetingRepository : IGreetingRepository
    {
        private readonly GreetingDbContext _greetingDbContext;                  //This is the primary object to interact with the database with

        public SqlGreetingRepository(GreetingDbContext greetingDbContext)
        {
            _greetingDbContext = greetingDbContext;
        }

        public async Task CreateAsync(Greeting greeting)
        {
            await _greetingDbContext.Greetings.AddAsync(greeting);
            await _greetingDbContext.SaveChangesAsync();                        //SaveChanges() persists the changes to the database
        }

        public async Task<Greeting> GetAsync(Guid id)
        {
            var greeting = await _greetingDbContext.Greetings.FirstOrDefaultAsync(x => x.Id == id);             //Can use LINQ to query the db. EF Core will translate this to T-SQL before sending to the db
            if (greeting == null)
                throw new Exception("Not found");
            
            return greeting;
        }

        public async Task<IEnumerable<Greeting>> GetAsync()
        {
            return await _greetingDbContext.Greetings.ToListAsync();                //Use async methods if possible
        }

        public async Task<IEnumerable<Greeting>> GetAsync(string from, string to)
        {
            //from & to are not null
            if (!string.IsNullOrWhiteSpace(from) && !string.IsNullOrWhiteSpace(to))
            {
                var greetings = _greetingDbContext.Greetings.Where(x => x.From.Equals(from) && x.To.Equals(to));
                return await greetings.ToListAsync();
            }
            //from is not null & to is null
            else if (!string.IsNullOrWhiteSpace(from) && string.IsNullOrWhiteSpace(to))
            {
                var greetings = _greetingDbContext.Greetings.Where(x => x.From.Equals(from));
                return await greetings.ToListAsync();
            }
            //from is null & to is not null
            else if (string.IsNullOrWhiteSpace(from) && !string.IsNullOrWhiteSpace(to))
            {
                var greetings = _greetingDbContext.Greetings.Where(x => x.To.Equals(to));
                return await greetings.ToListAsync();
            }

            //from & to are null, return all greetings
            return await _greetingDbContext.Greetings.ToListAsync();
        }

        public async Task UpdateAsync(Greeting greeting)
        {
            var existingGreeting = await _greetingDbContext.Greetings.FirstOrDefaultAsync(x => x.Id == greeting.Id);            //get a handle on the greeting in the db
            if (existingGreeting == null)
                throw new Exception("Not found");

            existingGreeting.Message = greeting.Message;                                                                        //update the properties
            existingGreeting.To = greeting.To;
            existingGreeting.From = greeting.From;

            await _greetingDbContext.SaveChangesAsync();                                                                        //persist changes to db
        }
    }
}
