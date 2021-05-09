using GameWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameWeb.Data
{
    public static class SeedData
    {
        public static async Task SeedUsers(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var users = new List<IdentityUser>
                {
                    new IdentityUser
                    {
                        UserName = "Admin",
                        Email = "admin@gameweb.com",
                    },
                    new IdentityUser
                    {
                        UserName = "User",
                        Email = "user@gameweb.com",
                    },
                    new IdentityUser
                    {
                        UserName = "Editor",
                        Email = "editor@gameweb.com",
                    },
                    new IdentityUser
                    {
                        UserName = "Publisher",
                        Email = "publisher@gameweb.com",
                    },
                };

                foreach (var user in users)
                {
                    await userManager.CreateAsync(user, "Pa$$w0rd");
                }

                await context.SaveChangesAsync();
            }
        }
    }
}
