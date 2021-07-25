using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgregatorMVC.Models
{
    public class AgregatorMVCContext : DbContext
    {
        public AgregatorMVCContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<LinkModel> Links { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<UsersVotesModel> UsersVotes { get; set; }

    }
}
