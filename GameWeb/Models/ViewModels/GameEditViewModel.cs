using GameWeb.ViewModels;
using Microsoft.AspNetCore.Http;

namespace GameWeb.Models.ViewModels
{
    public class GameEditViewModel : GameViewModel
    {
        public int Id { get; set; }
        public override IFormFile ImageFile { get; set; }
        public string Image { get; set; }
    }
}
