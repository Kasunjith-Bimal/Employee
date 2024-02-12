using Employee.Domain.Entities;
using Employee.Infrastructure.Persistence.Repository.EmployeeRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employee.Infrastructure.Persistence.EFCore
{
    public class EmployeeDbContext : IdentityDbContext<EmployeeDetail>
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
        {
            // The base constructor handles initializing the DbContext with the provided options.
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var adminRoleId = Guid.NewGuid().ToString();
            // Seed roles
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = adminRoleId, Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = Guid.NewGuid().ToString(), Name = "Employee", NormalizedName = "EMPLOYEE" }
            );

            var adminUserId = Guid.NewGuid().ToString();

            var adminUser = new EmployeeDetail
            {
                Id = adminUserId,
                UserName = "kasunysoft@gmail.com",
                NormalizedUserName = "KASUNYSOFT@GMAIL.COM",
                Email = "kasunysoft@gmail.com",
                NormalizedEmail = "KASUNYSOFT@GMAIL.COM",
                EmailConfirmed = false,
                SecurityStamp = Guid.NewGuid().ToString("D"),
                Address = "No 212,Walekade,Palugasdamana,Polonnaruwa.",
                Telephone = "0716063159",
                JoinDate = new DateTime(),
                FullName ="Mirahampe Patisthana Gedara Kasunjith Bimal Lakshitha",
                Salary =333000,
                PhoneNumber = "0716063159",
                IsFirstLogin = true,
                IsActive = true,
            };

            var hasher = new PasswordHasher<EmployeeDetail>();
            adminUser.PasswordHash = hasher.HashPassword(adminUser, "KasunJith123@");

            builder.Entity<EmployeeDetail>().HasData(adminUser);

            // Assign the Admin user to the Admin role
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = adminRoleId,
                UserId = adminUserId
            });
        }
    }
}
