using FundRaiserSystemEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundRaiserSystemData
{
    public interface IUserLoginService
    {
        IEnumerable<UserLogin> GetAll();
        UserLogin GetUser(string email, string password, string status);
        UserLogin Get(int id);
        int Insert(UserLogin userLogin);
        int Update(UserLogin userLogin);
        int Delete(int id);
    }
}
