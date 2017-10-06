using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FundRaiserSystemEntity;

namespace FundRaiserSystemData
{
    public interface IUserBankAccountService
    {
        IEnumerable<UserBankAccount> GetAll(bool includeUserInformations = false, bool includeBankInformations = false);
        UserBankAccount Get(int id, bool includeUserInformations = false, bool includeBankInformations = false);
        int Insert(UserBankAccount userBankAccount);
        int Update(UserBankAccount userBankAccount);
        int Delete(int id);
    }
}
