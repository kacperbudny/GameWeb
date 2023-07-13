using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GameWeb.Models.ViewModels
{
    public class NewsViewModel
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
        public virtual IFormFile ImageFile { get; set; }
    }
}
