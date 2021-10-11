using System;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace Contex
{
    public class ApplicationContex:DbContext
    {
        public DbSet<User> Users { get; set; }
        public ApplicationContex()
        {
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConfigurationManager.ConnectionStrings["DB"].ConnectionString);
        }
    }
}
