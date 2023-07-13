using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameWeb.Models
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime BirthDate { get; set; }
        public string Description { get; set; }
        [NotMapped]
        public string Role { get; set; }
        public List<News> News { get; set; }
        public ICollection<FavouriteGame> FavouriteGames { get; set; }
        public ICollection<WishlistGame> WishlistGames { get; set; }
        public ICollection<GameRating> GameRates { get; set; }
    }
}
