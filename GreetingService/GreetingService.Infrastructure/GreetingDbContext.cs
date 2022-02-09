using GreetingService.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreetingService.Infrastructure
{
    public class GreetingDbContext : DbContext
    {
        /// <summary>
        /// This constructor is needed for EF Core tools to be able to work with Migrations
        /// </summary>
        public GreetingDbContext()
        {
        }

        /// <summary>
        /// This constructor is used in dependency injection
        /// </summary>
        /// <param name="options"></param>
        public GreetingDbContext(DbContextOptions options) : base(options)
        {
        }

        /// <summary>
        /// This override is used by EF Core tools to be able to work with Migrations
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Can't get a handle on IConfiguration here, we'll get connection string from Environment Variables as a work around
            optionsBuilder.UseSqlServer(Environment.GetEnvironmentVariable("GreetingDbConnectionString"));
        }

        /// <summary>
        /// Greetings table
        /// </summary>
        public DbSet<Greeting> Greetings { get; set; }
    }
}
