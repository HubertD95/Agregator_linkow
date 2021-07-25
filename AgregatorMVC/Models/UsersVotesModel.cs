using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AgregatorMVC.Models
{
    [Table("UsersVotes")]
    public class UsersVotesModel
    {        
        [Key]
        public long ID { get; set; }
        public long UserID { get; set; }
        public long LinkID { get; set; }
        public bool? UpVote { get; set; }
        public bool? DownVote { get; set; }
    }
}
