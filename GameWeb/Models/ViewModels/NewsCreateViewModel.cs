using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameWeb.Models.ViewModels
{
    public class NewsCreateViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Proszę wprowadzić tytuł.")]
        [DisplayName("Tytuł")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Proszę wprowadzić zawartość.")]
        [DisplayName("Zawartość newsa")]
        public string Content { get; set; }
        public DateTime PublicationDate { get; set; }
        public string AuthorID { get; set; }
        public ApplicationUser Author { get; set; }
        [DisplayName("Tagi (rodziel przecinkiem)")]
        public string Tags { get; set; }
        [Required(ErrorMessage = "Proszę umieścić zdjęcie.")]
        [DisplayName("Zdjęcie")]
        public virtual IFormFile ImageFile { get; set; }
    }
}
