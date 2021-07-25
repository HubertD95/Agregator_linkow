using AgregatorMVC.Data.CustomValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AgregatorMVC.Models
{
    [Table("Links")]
    public class LinkModel
    {
        [Key]
        public long ID { get; set; }
        [Required(ErrorMessage = "Pole jest wymagane")]
        [Display(Name = "Tytuł")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Pole jest wymagane")]
        [Url(ErrorMessage = "Nieprawidłowy link")]
        [LinkValidation]
        public string Link { get; set; }
        [Display(Name = "Punkty")]
        public int Points { get; set; }
        public long UserID { get; set; }
        public DateTime DateTime { get; set; } 
        public bool? UpVote { get; set; }
        public bool? DownVote { get; set; }
    }
}
