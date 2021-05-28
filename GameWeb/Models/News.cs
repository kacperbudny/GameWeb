using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameWeb.Models
{
    public class News
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Proszę wprowadzić tytuł.")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Proszę wprowadzić zawartość.")]
        public string Content { get; set; }
        [Required]
        public DateTime PublicationDate { get; set; }
        [Required]
        public string AuthorID { get; set; }
        public ApplicationUser Author { get; set; }
        public string Tags { get; set; }
        [Required(ErrorMessage = "Proszę umieścić zdjęcie.")]
        public string ImagePath { get; set; }
    }
}
