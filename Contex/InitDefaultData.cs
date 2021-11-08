using Context.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Context
{
    internal static class InitDefaultData
    {
        private const string Stamp = "v3261te0-g279-8c3q-b8ii-ss9s44m894v207";
        private const string AdminEmail = "admin@master.com";
        private const string AdminPassword = "masterPass";

        /// <summary>
        /// Initializes the default data.
        /// </summary>
        /// <param name="modelBuilder">The model builder.</param>
        public static void Init(ModelBuilder modelBuilder)
        {
            var normalizer = new UpperInvariantLookupNormalizer();
            var hasher = new PasswordHasher<IdentityUser>();

            modelBuilder.Entity<ApplicationUser>().HasData(
               new ApplicationUser
               {
                   Id = "masterId",
                   UserName = AdminEmail,
                   NormalizedUserName = normalizer.NormalizeName(AdminEmail),
                   PasswordHash = hasher.HashPassword(null, AdminPassword),
                   EmailConfirmed = true,
                   Email = AdminEmail,
                   NormalizedEmail = normalizer.NormalizeEmail(AdminEmail),
                   ConcurrencyStamp = Stamp,
                   SecurityStamp = Stamp,
               });
        }
    }
}
