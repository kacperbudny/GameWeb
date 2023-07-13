using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GameWeb.Models.ViewModels
{
    public class UserEditViewModel
    {
        public string Id { get; set; }
        [Required]
        [Display(Name = "Nazwa użytkownika")]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string? Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź hasło")]
        [Compare("Password",
            ErrorMessage = "Hasła się nie zgadzają.")]
        public string? ConfirmPassword { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Data urodzenia")]
        public DateTime BirthDate { get; set; }

        [DisplayName("Opis")]
        public string? Description { get; set; }

        public string SelectedRole { get; set; }

        public IEnumerable<SelectListItem>? RoleList { get; set; }
    }
}
