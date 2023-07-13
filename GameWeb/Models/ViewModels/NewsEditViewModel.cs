using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System;

namespace GameWeb.Models.ViewModels
{
    public class NewsEditViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Proszę wprowadzić tytuł.")]
        [DisplayName("Tytuł")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Proszę wprowadzić zawartość.")]
        [DisplayName("Zawartość newsa")]
        public string Content { get; set; }
        public DateTime PublicationDate { get; set; }
        public string? AuthorID { get; set; }
        [DisplayName("Tagi (rodziel przecinkiem)")]
        public string Tags { get; set; }
        public string Image { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
