using GameWeb.Models;
using GameWeb.Utilities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameWeb.Data
{
    public static class SeedData
    {
        public static async Task SeedDb(IServiceProvider serviceProvider)
        {
            await SeedRoles(serviceProvider);
            await SeedUsers(serviceProvider);
        }

        public static async Task SeedUsers(IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            if (!userManager.Users.Any())
            {
                var users = new List<ApplicationUser>
                {
                    new ApplicationUser
                    {
                        UserName = "Admin",
                        Email = "admin@gameweb.com",
                    },
                    new ApplicationUser
                    {
                        UserName = "User",
                        Email = "user@gameweb.com",
                    },
                    new ApplicationUser
                    {
                        UserName = "Editor",
                        Email = "editor@gameweb.com",
                    },
                    new ApplicationUser
                    {
                        UserName = "Publisher",
                        Email = "publisher@gameweb.com",
                    },
                };

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "Pa$$w0rd");

                    string role = null;

                    switch (user.UserName)
                    {
                        case "Admin":
                            role = RoleNames.AdminRole;
                            break;
                        case "Editor":
                            role = RoleNames.NewsCreatorRole;
                            break;
                        case "Publisher":
                            role = RoleNames.GamePublisherRole;
                            break;
                    }

                    if (role != null) await userManager.AddToRoleAsync(user, role);
                }

                await context.SaveChangesAsync();
            }
        }

        public static async Task SeedRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();
            var roleNames = typeof(RoleNames).GetFields();

            if (roleManager == null)
            {
                throw new Exception("roleManager null");
            }

            foreach (var r in roleNames)
            {
                var role = r.GetValue(null).ToString();

                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}
