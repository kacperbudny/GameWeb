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
        }
    }
}