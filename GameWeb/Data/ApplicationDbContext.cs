using Microsoft.EntityFrameworkCore;
using GameWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace GameWeb.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Game> Game { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Game>().HasData(
                new Game
                {
                    Id = 1,
                    Name = "Minecraft",
                    ReleaseDate = new DateTime(2011, 11, 18),
                    Platform = "PC, Xbox, PlayStation, Android",
                    Publisher = "Mojang",
                    Description = "Lorem ipsum",
                    Image = "1.jpg",
                    Genre = "Sandbox"
                },
                new Game
                {
                    Id = 2,
                    Name = "Rayman 3: Hoodlum Havoc",
                    ReleaseDate = new DateTime(2003, 02, 15),
                    Platform = "PC, Xbox, PlayStation",
                    Publisher = "Ubisoft",
                    Description = "Lorem ipsum",
                    Image = "2.jpg",
                    Genre = "Platformówki"
                },
                new Game
                {
                    Id = 3,
                    Name = "FlatOut 2",
                    ReleaseDate = new DateTime(2006, 06, 30),
                    Platform = "PC, Xbox, PlayStation",
                    Publisher = "Empire Interactive",
                    Description = "Lorem ipsum",
                    Image = "3.jpg",
                    Genre = "Wyœcigi"
                }
                );
        }
    }
}