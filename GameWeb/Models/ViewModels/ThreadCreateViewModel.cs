using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GameWeb.Models.ViewModels
{
    public class ThreadCreateViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Proszę wprowadzić nazwę wątku.")]
        [DisplayName("Nazwa wątku")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Proszę wprowadzić treść wiadomości.")]
        [DisplayName("Treść wiadomości")]
        public string Content { get; set; }
        public int GameId { get; set; }
        public string GameName { get; set; }
    }
}
