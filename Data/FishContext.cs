using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using FishyGame.Models;

namespace FishyGame.Data
{
    public class FishContext : DbContext
    {
        // an empty constructor
        public FishContext() { }

        // base(options) calls the base class's constructor,
        // in this case, our base class is DbContext
        public FishContext(DbContextOptions<FishContext> options) : base(options) { }

        // Use DbSet<Student> to query or read and 
        // write information about A Student
        public DbSet<Fish> Fish { get; set; }
        public static System.Collections.Specialized.NameValueCollection AppSettings { get; }

        // configure the database to be used by this context
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
           .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
           .AddJsonFile("appsettings.json")
           .Build();

            // schoolSIMSConnection is the name of the key that
            // contains the has the connection string as the value
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("FishyGameConnection"));
        }
    }
}
