using GameWeb.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace GameWeb.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        public DbSet<Game> Game { get; set; }
        public DbSet<Requirement> Requirement { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Requirement>()
                .HasOne(e => e.Game)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Game>()
                .HasOne(e => e.MinimalRequirements)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Game>()
                .HasOne(e => e.RecommendedRequirements)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Requirement>().HasData(
                new Requirement
                {
                    Id = 1,
                    CPU = "Intel Core i3 3210 / AMD A8 7600 APU",
                    DriveSize = 180,
                    GPU = "NVIDIA GeForce 400 Series / AMD Radeon HD 7000 series",
                    OS = "64-bit Windows 7",
                    RAM = 4,
                },
                new Requirement
                {
                    Id = 2,
                    CPU = "Intel Core i5 4690 / AMD A10 7800",
                    DriveSize = 4000,
                    GPU = "NVIDIA GeForce 700 Series / AMD Radeon Rx 200 Series",
                    OS = "64-bit Windows 10",
                    RAM = 8,
                },
                new Requirement
                {
                    Id = 3,
                    CPU = "Pentium III 600 MHz / AMD Athlon 600 MHz",
                    DriveSize = 800,
                    GPU = "Kompatybilna z DirectX 8.1",
                    OS = "Windows XP",
                    RAM = 0.256,
                },
                new Requirement
                {
                    Id = 4,
                    CPU = "Pentium III 1 GHz / AMD Athlon 1 Ghz",
                    DriveSize = 800,
                    GPU = "Kompatybilna z DirectX 9",
                    OS = "Windows XP",
                    RAM = 0.512,
                },
                new Requirement
                {
                    Id = 5,
                    CPU = "Intel Pentium 4 2.0GHz / AMD Athlon XP 2000+",
                    DriveSize = 3500,
                    GPU = "AMD Radeon HD 4350 / NVIDIA GeForce 6600 GT",
                    OS = "32-bit Windows XP",
                    RAM = 0.256,
                },
                new Requirement
                {
                    Id = 6,
                    CPU = "Intel Pentium 4 2.0GHz / AMD Athlon XP 2500+",
                    DriveSize = 4000,
                    GPU = "AMD Radeon 9600 Series / NVIDIA GeForce 6600",
                    OS = "32-bit Windows XP",
                    RAM = 0.512,
                }
                );

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
                    Genre = "Sandbox",
                    Developer = "Mojang",
                    MinimalRequirementsId = 1,
                    RecommendedRequirementsId = 2
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
                    Genre = "Platformówki",
                    Developer = "Ubisoft",
                    MinimalRequirementsId = 3,
                    RecommendedRequirementsId = 4
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
                    Genre = "Wyœcigi",
                    Developer = "Bugbear Entertainment",
                    MinimalRequirementsId = 5,
                    RecommendedRequirementsId = 6
                }
            );
        }
    }
}