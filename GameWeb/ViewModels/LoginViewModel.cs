using System.ComponentModel.DataAnnotations;

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
