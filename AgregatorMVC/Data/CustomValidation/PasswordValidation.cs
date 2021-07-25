using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AgregatorMVC.Models;

namespace AgregatorMVC.Data.CustomValidation
{
    public class PasswordValidation : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var password = (RegistrationModel)validationContext.ObjectInstance;

            if (password.Password == password.ConfirmPassword)
            {
                
                return ValidationResult.Success;               
            }
            else
                return new ValidationResult("Hasła nie są takie same");
        }
    }
}
