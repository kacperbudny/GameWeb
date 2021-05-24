using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GameWeb.Models
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime BirthDate { get; set; }
        public string Description { get; set; }
        [NotMapped]
        public string Role { get; set; }
        public ICollection<FavouriteGame> FavouriteGames { get; set; }
    }
}
