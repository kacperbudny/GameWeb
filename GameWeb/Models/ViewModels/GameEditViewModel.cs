using GameWeb.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameWeb.Models.ViewModels
{
    public class GameEditViewModel : GameViewModel
    {
        public int Id { get; set; }
        public override IFormFile ImageFile { get; set; }
        public string Image { get; set; }
    }
}
