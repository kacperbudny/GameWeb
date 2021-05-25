using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameWeb.Models.ViewModels
{
    public class ThreadCreateViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Proszę wprowadzić nazwę wątku.")]
        [DisplayName("Nazwa wątku")]
        public string Name { get; set; }
        public int GameId { get; set; }
        public Game Game { get; set; }
        public GameComment Comment { get; set; }
    }
}
