using GameWeb.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GameWeb.ViewModels
{
    public class GameViewModel
    {
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
        public virtual IFormFile ImageFile { get; set; }
        public Requirement MinimalRequirements { get; set; }
        public Requirement RecommendedRequirements { get; set; }
    }
}
