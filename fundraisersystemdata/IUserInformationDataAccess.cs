using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FundRaiserSystemEntity;

namespace FundRaiserSystemData
{
    public interface IUserInformationDataAccess
    {
        IEnumerable<UserInformation> GetAll(bool includeUserLogins = false);
        UserInformation Get(int id, bool includeUserLogins = false);
        int Insert(UserInformation userInformation);
        int Update(UserInformation userInformation);
        int Delete(int id);
    }
}
