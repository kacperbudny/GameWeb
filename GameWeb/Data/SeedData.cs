using GameWeb.Models;
using GameWeb.Utilities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
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
            await SeedNews(serviceProvider);
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

        public static async Task SeedNews(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var hostEnvironment = serviceProvider.GetRequiredService<IWebHostEnvironment>();

            if (!context.News.Any())
            {
                var editor = context.Users.First(u => u.UserName == "Publisher");

                var news = new List<News>
                {
                    new News
                    {
                        AuthorID = editor.Id,
                        Tags = "gry, nowości, technologia",
                        Title = "Lorem Ipsum 1",
                        Content = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque diam lorem, bibendum sit amet dignissim vitae, dictum eu ante. Mauris nibh augue, aliquam non blandit sed, ultrices sit amet eros. Ut in est non ipsum ullamcorper sagittis. Vestibulum malesuada velit sit amet metus ultricies, in fringilla risus pellentesque. Aenean nec feugiat ipsum, eget sagittis justo. Sed eu tincidunt felis. Proin ut urna urna. Maecenas malesuada quam massa, non rutrum metus ultrices quis. Nulla viverra elit sed diam convallis mattis. Fusce luctus rutrum hendrerit.\r\n\r\nSed et dui dapibus, vestibulum nibh nec, viverra nulla. Nulla eu sodales felis, luctus euismod libero. Praesent cursus erat nisl. Phasellus aliquet magna velit, non rhoncus dui scelerisque eu. Praesent imperdiet orci dolor. Quisque convallis mauris et leo varius, in semper leo maximus. Sed blandit nec sem non hendrerit.\r\n\r\nFusce commodo, eros nec pellentesque tincidunt, metus ante rutrum tellus, nec porttitor leo lectus sed quam. Nullam risus ipsum, lacinia et erat vel, scelerisque faucibus erat. Nullam eget odio lorem. Suspendisse ac eros luctus, luctus dui ac, luctus purus. Praesent hendrerit ut diam eget convallis. Duis mattis, odio sed aliquam aliquet, magna ex ornare nisl, euismod efficitur lacus tortor ac lorem. Nunc sagittis mi odio, a pulvinar sem blandit et. Morbi at ullamcorper ante. Mauris blandit purus est, at ultrices urna vestibulum a.\r\n\r\nVivamus eget quam eget elit malesuada rhoncus ut eu nibh. Nulla a suscipit dolor. Sed eget magna scelerisque, lobortis est iaculis, rutrum lacus. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Nullam ut vehicula turpis. Aliquam erat volutpat. Curabitur vel magna lacus. Nunc gravida, ante eget convallis egestas, dui odio iaculis ligula, sed scelerisque urna est a nisi. Duis ac laoreet dui. Nunc ut tellus feugiat, convallis dolor non, elementum ante. Sed ultrices quam sed fermentum feugiat. Fusce varius dictum scelerisque. Quisque at dui suscipit sem convallis ornare sed at arcu. Etiam tristique eu odio a pretium. Suspendisse volutpat sed turpis a finibus.\r\n\r\nMaecenas eleifend arcu ligula, quis blandit erat congue a. Sed ultrices mauris vel eleifend molestie. Ut pharetra, turpis quis mattis elementum, ipsum sem maximus enim, et aliquet ante velit eget massa. Nunc in elementum diam, vitae gravida nibh. Morbi vehicula vulputate volutpat. Maecenas vitae tincidunt nunc. Morbi eget suscipit sapien, vitae placerat eros. Mauris non nunc mattis, vulputate felis vel, volutpat nisl. In vitae urna orci. Fusce maximus, justo vitae lacinia lobortis, elit mauris pellentesque nisl, vel sodales velit lorem ac quam. Aenean at sapien commodo, sollicitudin ligula sed, rhoncus nibh. Morbi ultrices massa interdum nulla rutrum, eu rutrum ante commodo. Mauris blandit eros euismod lectus dignissim, et bibendum metus facilisis. Lorem ipsum dolor sit amet, consectetur adipiscing elit.\r\n\r\n",
                        ImagePath = Path.Combine(hostEnvironment.WebRootPath, "images", "NewsImages", "1.jpg"),
                        PublicationDate = DateTime.Parse("01/01/2020"),
                    },
                };

                await context.SaveChangesAsync();
            }
        }
    }
}
