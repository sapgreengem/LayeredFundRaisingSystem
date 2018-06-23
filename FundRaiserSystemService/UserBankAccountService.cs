using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FundRaiserSystemEntity;
using FundRaiserSystemData;

namespace FundRaiserSystemService
{
    class UserBankAccountService : IUserBankAccountService
    {
        private IUserBankAccountDataAccess data;
        public UserBankAccountService(IUserBankAccountDataAccess data)
        {
            this.data = data;
        }
        public IEnumerable<UserBankAccount> GetAll(bool includeUserInformations = false, bool includeBankInformations = false)
        {
            return this.data.GetAll(includeUserInformations, includeBankInformations);
        }
        public UserBankAccount Get(int id , bool includeUserInformations = false, bool includeBankInformations = false)
        {
            return this.data.Get(id, includeUserInformations, includeBankInformations);
        }
        public int Insert(UserBankAccount userBankAccount)
        {
            return this.data.Insert(userBankAccount);
        }
        public int Update(UserBankAccount userBankAccount)
        {
            return this.data.Update(userBankAccount);
        }
        public int Delete(int id)
        {
            return this.data.Delete(id);
        }
    }
}
