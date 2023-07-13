using GameWeb.ViewModels;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameWeb.Models.ViewModels
{
    public class GameCreateViewModel
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
        public int? MinimalRequirementsId { get; set; }
        [ForeignKey("MinimalRequirementsId")]
        public Requirement? MinimalRequirements { get; set; }
        public int? RecommendedRequirementsId { get; set; }
        [ForeignKey("RecommendedRequirementsId")]
        public Requirement? RecommendedRequirements { get; set; }
        [Required(ErrorMessage = "Proszę umieścić zdjęcie.")]
        [DisplayName("Zdjęcie")]
        public IFormFile ImageFile { get; set; }
    }
}
