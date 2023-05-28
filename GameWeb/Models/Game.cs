using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameWeb.Models
{
    public class Game
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Proszę wprowadzić nazwę.")]
        [DisplayName("Nazwa")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Proszę wprowadzić datę.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Data wydania")]
        public DateTime ReleaseDate { get; set; }
        [Required(ErrorMessage = "Proszę wprowadzić platformy.")]
        [DisplayName("Platformy")]
        public string Platform { get; set; }
        [Required(ErrorMessage = "Proszę wprowadzić wydawcę.")]
        [DisplayName("Wydawca")]
        public string Publisher { get; set; }
        [Required(ErrorMessage = "Proszę wprowadzić dewelopera.")]
        [DisplayName("Deweloper")]
        public string Developer { get; set; }
        [Required(ErrorMessage = "Proszę wprowadzić gatunek.")]
        [DisplayName("Gatunek")]
        public string Genre { get; set; }
        [Required(ErrorMessage = "Proszę wprowadzić opis.")]
        [DisplayName("Opis")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Proszę umieścić zdjęcie.")]
        [DisplayName("Zdjęcie")]
        public string Image { get; set; }
        public int? MinimalRequirementsId { get; set; }
        [ForeignKey("MinimalRequirementsId")]
        public Requirement? MinimalRequirements { get; set; }
        public int? RecommendedRequirementsId { get; set; }
        [ForeignKey("RecommendedRequirementsId")]
        public Requirement? RecommendedRequirements { get; set; }
        public List<GameCommentThread>? CommentThreads { get; set; }
        public ICollection<FavouriteGame>? FavouriteGames { get; set; }
        public ICollection<WishlistGame>? WishlistGames { get; set; }
        public ICollection<GameRating>? GameRates { get; set; }
        [NotMapped]
        public bool IsCurrentUsersFavourite { get; set; }
        [NotMapped]
        public bool IsInCurrentUsersWishlist { get; set; }
        [NotMapped]
        public int UserRating { get; set; }
        [NotMapped]
        public double AverageRating { get; set; }
    }
}