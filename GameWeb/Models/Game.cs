using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace GameWeb.Models
{
    public class Game
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Prosz� wprowadzi� nazw�.")]
        [DisplayName("Nazwa")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Prosz� wprowadzi� dat�.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Data wydania")]
        public DateTime ReleaseDate { get; set; }
        [Required(ErrorMessage = "Prosz� wprowadzi� platformy.")]
        [DisplayName("Platformy")]
        public string Platform { get; set; }
        [Required(ErrorMessage = "Prosz� wprowadzi� wydawc�.")]
        [DisplayName("Wydawca")]
        public string Publisher { get; set; }
        [Required(ErrorMessage = "Prosz� wprowadzi� dewelopera.")]
        [DisplayName("Deweloper")]
        public string Developer { get; set; }
        [Required(ErrorMessage = "Prosz� wprowadzi� gatunek.")]
        [DisplayName("Gatunek")]
        public string Genre { get; set; }
        [Required(ErrorMessage = "Prosz� wprowadzi� opis.")]
        [DisplayName("Opis")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Prosz� umie�ci� zdj�cie.")]
        [DisplayName("Zdj�cie")]
        public string Image { get; set; }
        public int MinimalRequirementsId { get; set; }
        [ForeignKey("MinimalRequirementsId")]
        public Requirement MinimalRequirements { get; set; }
        public int RecommendedRequirementsId { get; set; }
        [ForeignKey("RecommendedRequirementsId")]
        public Requirement RecommendedRequirements { get; set; }
        public List<GameCommentThread> CommentThreads { get; set; }
        public ICollection<FavouriteGame> FavouriteGames { get; set; }
        public ICollection<WishlistGame> WishlistGames { get; set; }
        [NotMapped]
        public bool IsCurrentUsersFavourite { get; set; }
        [NotMapped]
        public bool IsInCurrentUsersWishlist { get; set; }
    }
}