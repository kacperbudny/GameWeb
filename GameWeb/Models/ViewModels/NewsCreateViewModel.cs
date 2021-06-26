using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GameWeb.Models.ViewModels
{
    public class NewsCreateViewModel : NewsViewModel
    {
        [Required(ErrorMessage = "Proszę umieścić zdjęcie.")]
        [DisplayName("Zdjęcie")]
        public override IFormFile ImageFile { get; set; }
    }
}
