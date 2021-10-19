using System;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Context.Models;

namespace Context
{
    public class RestApiContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }

        public RestApiContext(DbContextOptions<RestApiContext> options):base(options){ }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
