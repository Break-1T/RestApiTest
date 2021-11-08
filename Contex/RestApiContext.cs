using System;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Context.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


namespace Context
{
    public class RestApiContext : IdentityDbContext<ApplicationUser>
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Operation> Operations { get; set; }
        public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public RestApiContext(DbContextOptions<RestApiContext> options):base(options){ }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.HasKey(user => user.Id);

                entity.Property(user => user.Id).ValueGeneratedOnAdd();
                entity.Property(user => user.Surname).IsRequired();
                entity.Property(user => user.Age);
                entity.Property(user => user.CurrentTime);
                //Сделать не каскадное удаление
                entity.HasMany(user => user.Operations)
                    .WithOne(operation => operation.User).OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Operation>(entity =>
            {
               entity.ToTable("Operations").HasKey(operation => operation.Id);

               entity.Property(operation => operation.Id).ValueGeneratedOnAdd();
               entity.Property(operation => operation.Name).IsRequired();
               entity.Property(operation => operation.DateTime).IsRequired();

               entity.HasOne(operation => operation.User).WithMany(user => user.Operations);
            });
            modelBuilder.Entity<ApplicationUser>(entry =>
            {
                entry.ToTable("ApplicationUsers").HasKey(user => user.Id);

                entry.Property(prop => prop.FirstName);
                entry.Property(prop => prop.LastName);
                entry.Property(prop => prop.Login);
                entry.Property(prop => prop.Password);

            });

            InitDefaultData.Init(modelBuilder);
        }
    }
}
