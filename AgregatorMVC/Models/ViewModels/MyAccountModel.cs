using AgregatorMVC.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgregatorMVC.Models
{
    public class MyAccountModel
    {
        public LinkModel Link { get; set; }
        public PaginatedList<LinkModel> Links { get; set; }
    }
}
