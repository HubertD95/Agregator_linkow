using AgregatorMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgregatorMVC.Data.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AgregatorMVCContext _context;
        public AccountRepository(AgregatorMVCContext context)
        {
            _context = context;
        }
        public UserModel Get(string login, string password)
            => _context.Users.SingleOrDefault(x => x.Login == login && x.Password == password);

        public UserModel GetByLogin(string login)
            => _context.Users.SingleOrDefault(x => x.Login == login);

        public void Add(UserModel UserModel)
        {
            _context.Users.Add(UserModel);
            _ = _context.SaveChanges();
        }
        public IQueryable<UserModel> GetAll()
            => _context.Users;
    }
}
