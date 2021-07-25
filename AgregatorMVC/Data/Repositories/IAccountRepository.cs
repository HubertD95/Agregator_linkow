using AgregatorMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgregatorMVC.Data.Repositories
{
    public interface IAccountRepository
    {        
        UserModel Get(string Login, string Password);
        UserModel GetByLogin(string login);
        void Add(UserModel UserModel);
        IQueryable<UserModel> GetAll();
    }
}
