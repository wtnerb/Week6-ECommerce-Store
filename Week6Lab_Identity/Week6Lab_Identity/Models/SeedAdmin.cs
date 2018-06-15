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
        
        public static async Task SeedDatabase (IServiceProvider serviceProvider, UserManager<ApplicationUser> userManager)
        {
            using (var dbCtx =
                new DbCtx(serviceProvider.GetRequiredService<DbContextOptions<DbCtx>>()))
            {
                dbCtx.Database.EnsureCreated();
                await AddRolesAsync(dbCtx);
                await AddUserAsync(dbCtx, userManager);
                await AddUserRolesAsync(dbCtx);
            }
        }

        public static async Task AddRolesAsync (DbCtx ctx)
        {
            if (ctx.Roles.Any()) return;
            foreach (var role in Roles)
            {
                await ctx.Roles.AddAsync(role);
                ctx.SaveChanges();
            }
        }

        public static async Task AddUserAsync (DbCtx ctx, UserManager<ApplicationUser> userManager)
        {
            if (await ctx.Users.AnyAsync(x => x.UserName == AdminEmail)) return;
            ApplicationUser admin = new ApplicationUser()
            {
                UserName = AdminEmail,
                Email = AdminEmail,
                EmailConfirmed = true,
                Location = (byte)Enum.Parse<Region>("Bellingham"),
                DateRegistered = DateTime.Now,
                BasketId = 0
            };
            await userManager.CreateAsync(admin, AdminPassword);
        }

        public static async Task AddUserRolesAsync (DbCtx ctx)
        {
            if (ctx.UserRoles.Any()) return;
            var userRole = new IdentityUserRole<string>
            {
                UserId = (await ctx.Users.SingleAsync(r => r.Email == AdminEmail)).Id,
                RoleId = (await ctx.Roles.SingleAsync(r => r.Name == Purpose.Admin)).Id
            };
            await ctx.UserRoles.AddAsync(userRole);
            ctx.SaveChanges();
        }
    }
}
