using GameWeb.ViewModels;
using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GameWeb.Models.ViewModels
{
    public class GameCreateViewModel : GameViewModel
    {
        [Required(ErrorMessage = "Proszę umieścić zdjęcie.")]
        [DisplayName("Zdjęcie")]
        public override IFormFile ImageFile { get; set; }
    }
}
