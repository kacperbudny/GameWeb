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
        public DbSet<GameComment> GameComment { get; set; }
        public DbSet<GameCommentThread> GameCommentThread { get; set; }
        public DbSet<FavouriteGame> FavouriteGame { get; set; }
        public DbSet<WishlistGame> WishlistGame { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<GameRating> GameRating { get; set; }

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

            builder.Entity<FavouriteGame>()
                .HasKey(fg => new { fg.GameId, fg.UserId });

            builder.Entity<FavouriteGame>()
                .HasOne(fg => fg.Game)
                .WithMany(g => g.FavouriteGames)
                .HasForeignKey(fg => fg.GameId);

            builder.Entity<FavouriteGame>()
                .HasOne(fg => fg.User)
                .WithMany(u => u.FavouriteGames)
                .HasForeignKey(fg => fg.UserId);

            builder.Entity<WishlistGame>()
                .HasKey(wg => new { wg.GameId, wg.UserId });

            builder.Entity<WishlistGame>()
                .HasOne(wg => wg.Game)
                .WithMany(g => g.WishlistGames)
                .HasForeignKey(wg => wg.GameId);

            builder.Entity<WishlistGame>()
                .HasOne(wg => wg.User)
                .WithMany(u => u.WishlistGames)
                .HasForeignKey(wg => wg.UserId);

            builder.Entity<GameRating>()
                .HasKey(gr => new { gr.GameId, gr.UserId });

            builder.Entity<GameRating>()
                .HasOne(gr => gr.Game)
                .WithMany(g => g.GameRates)
                .HasForeignKey(gr => gr.GameId);

            builder.Entity<GameRating>()
                .HasOne(gr => gr.User)
                .WithMany(u => u.GameRates)
                .HasForeignKey(gr => gr.UserId);

            builder.Entity<News>()
                .HasOne(n => n.Author)
                .WithMany(u => u.News);

            //builder.Entity<Requirement>().HasData(
            //    new Requirement
            //    {
            //        Id = 1,
            //        CPU = "Intel Core i3 3210 / AMD A8 7600 APU",
            //        DriveSize = 180,
            //        GPU = "NVIDIA GeForce 400 Series / AMD Radeon HD 7000 series",
            //        OS = "64-bit Windows 7",
            //        RAM = 4,
            //    },
            //    new Requirement
            //    {
            //        Id = 2,
            //        CPU = "Intel Core i5 4690 / AMD A10 7800",
            //        DriveSize = 4000,
            //        GPU = "NVIDIA GeForce 700 Series / AMD Radeon Rx 200 Series",
            //        OS = "64-bit Windows 10",
            //        RAM = 8,
            //    },
            //    new Requirement
            //    {
            //        Id = 3,
            //        CPU = "Pentium III 600 MHz / AMD Athlon 600 MHz",
            //        DriveSize = 800,
            //        GPU = "Kompatybilna z DirectX 8.1",
            //        OS = "Windows XP",
            //        RAM = 0.256,
            //    },
            //    new Requirement
            //    {
            //        Id = 4,
            //        CPU = "Pentium III 1 GHz / AMD Athlon 1 Ghz",
            //        DriveSize = 800,
            //        GPU = "Kompatybilna z DirectX 9",
            //        OS = "Windows XP",
            //        RAM = 0.512,
            //    },
            //    new Requirement
            //    {
            //        Id = 5,
            //        CPU = "Intel Pentium 4 2.0GHz / AMD Athlon XP 2000+",
            //        DriveSize = 3500,
            //        GPU = "AMD Radeon HD 4350 / NVIDIA GeForce 6600 GT",
            //        OS = "32-bit Windows XP",
            //        RAM = 0.256,
            //    },
            //    new Requirement
            //    {
            //        Id = 6,
            //        CPU = "Intel Pentium 4 2.0GHz / AMD Athlon XP 2500+",
            //        DriveSize = 4000,
            //        GPU = "AMD Radeon 9600 Series / NVIDIA GeForce 6600",
            //        OS = "32-bit Windows XP",
            //        RAM = 0.512,
            //    }
            //    );

            //builder.Entity<Game>().HasData(
            //    new Game
            //    {
            //        Id = 1,
            //        Name = "Minecraft",
            //        ReleaseDate = new DateTime(2011, 11, 18),
            //        Platform = "PC, Xbox, PlayStation, Android",
            //        Publisher = "Mojang",
            //        Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer ut mattis dolor. " +
            //        "Curabitur accumsan sapien quam, vel volutpat lacus euismod eget. Lorem ipsum dolor sit amet, consectetur adipiscing elit. " +
            //        "Duis eget ornare quam, nec lobortis nunc. Ut dui ex, faucibus ut finibus dictum, pretium in elit. Aliquam ac molestie sem. " +
            //        "Morbi dictum nisl in justo venenatis, id ultricies elit porta.",
            //        Image = "1.jpg",
            //        Genre = "Sandbox",
            //        Developer = "Mojang",
            //        MinimalRequirementsId = 1,
            //        RecommendedRequirementsId = 2
            //    },
            //    new Game
            //    {
            //        Id = 2,
            //        Name = "Rayman 3: Hoodlum Havoc",
            //        ReleaseDate = new DateTime(2003, 02, 15),
            //        Platform = "PC, Xbox, PlayStation",
            //        Publisher = "Ubisoft",
            //        Description = "Aliquam molestie mollis sagittis. Curabitur a nibh eu tortor dapibus sodales. " +
            //        "Proin a aliquet turpis. Proin augue massa, posuere ac interdum at, aliquam sed sem. Donec felis turpis, viverra eget elit ut, facilisis auctor risus. " +
            //        "Donec aliquam augue urna, et accumsan nisi lobortis in. Ut sagittis nunc feugiat nibh venenatis tempor. Sed rutrum ullamcorper sapien et eleifend. \n\n" +
            //        "Integer et odio tincidunt, luctus felis non, eleifend libero. Etiam a vulputate enim. " +
            //        "Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. " +
            //        "In ac ante iaculis, scelerisque orci varius, ornare ligula. Donec nec faucibus eros. " +
            //        "Phasellus lacinia elit arcu, condimentum sodales libero pretium sed. In ut commodo orci. Sed sagittis viverra ante, id dignissim metus mollis ut.",
            //        Image = "2.jpg",
            //        Genre = "Platformówki",
            //        Developer = "Ubisoft",
            //        MinimalRequirementsId = 3,
            //        RecommendedRequirementsId = 4
            //    },
            //    new Game
            //    {
            //        Id = 3,
            //        Name = "FlatOut 2",
            //        ReleaseDate = new DateTime(2006, 06, 30),
            //        Platform = "PC, Xbox, PlayStation",
            //        Publisher = "Empire Interactive",
            //        Description = "Donec vehicula ornare elit, nec tempor ante ornare a. In hac habitasse platea dictumst. " +
            //        "Etiam rhoncus ornare vestibulum. Quisque id odio pellentesque, maximus massa in, aliquam quam. " +
            //        "Donec lacus sem, lobortis a dolor vel, condimentum pellentesque orci. " +
            //        "Aenean nunc ipsum, cursus a blandit at, pellentesque id purus. Quisque tincidunt urna vel sem maximus, a efficitur urna suscipit.",
            //        Image = "3.jpg",
            //        Genre = "Wyścigi",
            //        Developer = "Bugbear Entertainment",
            //        MinimalRequirementsId = 5,
            //        RecommendedRequirementsId = 6
            //    }
            //);
        }
    }
}