using AgregatorMVC.Data.CustomValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AgregatorMVC.Models
{
    public class RegistrationModel
    {
        [Required(ErrorMessage = "To pole jest wymagane")]
        [EmailAddress(ErrorMessage = "Nieprawidłowy adres email")]
        [Display(Name = "Adres email")]
        public string Login { get; set; }
        [Display(Name = "Hasło")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "To pole jest wymagane")]
        [StringLength(50, ErrorMessage = "Hasło musi mieć minimum {2} znaków.", MinimumLength = 6)]
        public string Password { get; set; }
        [Display(Name = "Powtórz hasło")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "To pole jest wymagane")]
        [PasswordValidation]
        public string ConfirmPassword { get; set; }
    }
}
