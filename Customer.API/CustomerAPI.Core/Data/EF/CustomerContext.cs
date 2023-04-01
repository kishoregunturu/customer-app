using CustomerAPI.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CustomerAPI
{
    public class CustomerContext:DbContext
    {
        
        private string? db { get; set; }

        public CustomerContext(IConfiguration configuration)
        {
             db = configuration.GetConnectionString("DB");
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={db}");

        public DbSet<Customer> Customers { get; set; }
    }
}
