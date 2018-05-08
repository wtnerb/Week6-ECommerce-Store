using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Threading.Tasks;
using Week6Lab_Identity.Data;

namespace Week6Lab_Identity.Models
{
    public class SeedAdmin
    {
        private const string AdminEmail = "admin@admin.com";
        private const string AdminPassword = "!12qwQW";

        private static readonly List<IdentityRole> Roles = new List<IdentityRole>
        {
            new IdentityRole
            {
                Name=Purpose.Admin,
                NormalizedName = Purpose.Admin.ToLower(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            },

            new IdentityRole
            {
                Name=Purpose.User,
                NormalizedName = Purpose.User.ToLower(),
                ConcurrencyStamp = Guid.NewGuid().ToString()
            }
        };
        
        public static void SeedDatabase (IServiceProvider serviceProvider, UserManager<ApplicationUser> userManager)
        {
            using (var dbCtx =
                new DbCtx(serviceProvider.GetRequiredService<DbContextOptions<DbCtx>>()))
            {
                dbCtx.Database.EnsureCreated();
                AddRoles(dbCtx);
                AddUser(dbCtx, userManager);
                AddUserRoles(dbCtx);
            }
        }

        public static void AddRoles (DbCtx ctx)
        {
            if (ctx.Roles.Any()) return;
            foreach (var role in Roles)
            {
                ctx.Roles.Add(role);
                ctx.SaveChanges();
            }
        }

        public static async void AddUser (DbCtx ctx, UserManager<ApplicationUser> userManager)
        {
            if (ctx.Users.Any(x => x.UserName == AdminEmail)) return;
            ApplicationUser admin = new ApplicationUser()
            {
                UserName = AdminEmail,
                Email = AdminEmail,
                EmailConfirmed = true,
                Location = (byte)Enum.Parse<Region>("Bellingham"),
                DateRegistered = DateTime.Now
            };
            await userManager.CreateAsync(admin, AdminPassword);
        }

        public static void AddUserRoles (DbCtx ctx)
        {
            if (ctx.UserRoles.Any()) return;
            var userRole = new IdentityUserRole<string>
            {
                UserId = ctx.Users.Single(r => r.Email == AdminEmail).Id,
                RoleId = ctx.Roles.Single(r => r.Name == Purpose.Admin).Id
            };
            ctx.UserRoles.Add(userRole);
            ctx.SaveChanges();
        }
    }
}
