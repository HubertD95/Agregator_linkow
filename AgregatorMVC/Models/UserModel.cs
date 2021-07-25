using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AgregatorMVC.Models
{
    [Table("Users")]
    public class UserModel
    {        
        [Key]
        public long ID { get; set; }
        [Display(Name = "Adres email")]
        [Required(ErrorMessage ="To pole jest wymagane")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Login { get; set; }
        [Display(Name = "Hasło")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "To pole jest wymagane")]
        public string Password { get; set; }
    }
}
