using GameWeb.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameWeb.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Has³o")]
        public string Password { get; set; }

        [Display(Name = "Zapamiêtaj mnie")]
        public bool RememberMe { get; set; }
    }
}
