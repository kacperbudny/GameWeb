using System.ComponentModel.DataAnnotations;

namespace GameWeb.ViewModels
{
    public class RegistrationViewModel
    {
        [Required]
        [Display(Name = "Nazwa użytkownika")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź hasło")]
        [Compare("Password",
            ErrorMessage = "Hasła się nie zgadzają.")]
        public string ConfirmPassword { get; set; }
    }
}
