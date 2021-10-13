using System;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Contex.Models;

namespace Contex
{
    public class ApplicationContex : DbContext
    {
        public DbSet<User> Users { get; set; }

        public ApplicationContex(){ }
        public ApplicationContex(DbContextOptions<ApplicationContex> options):base(options){ }
    }
}
