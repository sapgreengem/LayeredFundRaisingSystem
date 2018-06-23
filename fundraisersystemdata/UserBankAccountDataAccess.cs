using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FundRaiserSystemEntity;
namespace FundRaiserSystemData
{
    class UserBankAccountDataAccess : IUserBankAccountDataAccess
    {
        private FundRaiserDBContext context;
        public UserBankAccountDataAccess(FundRaiserDBContext context)
        {
            this.context = context;
        }
        public IEnumerable<UserBankAccount> GetAll(bool includeUserInformations = false, bool includeBankInformations = false)
        {
            if (includeUserInformations || includeBankInformations)
            {
                return this.context.UserBankAccounts.Include("UserInformation").Include("BankInformation").ToList();
            }
            else
            {
                return this.context.UserBankAccounts.ToList();
            }
        }
        public UserBankAccount Get(int id, bool includeUserInformations = false, bool includeBankInformations = false)
        {
            if (includeUserInformations || includeBankInformations)
            {
                return this.context.UserBankAccounts.Include("UserInformation").Include("BankInformation").SingleOrDefault(x => x.UserBankAccountId == id);
            }
            else
            {
                return this.context.UserBankAccounts.SingleOrDefault(x => x.UserBankAccountId == id);
            }
        }
        public int Insert(UserBankAccount userBankAccount)
        {
            this.context.UserBankAccounts.Add(userBankAccount);
            return this.context.SaveChanges();
        }
        public int Update(UserBankAccount userBankAccount)
        {
            UserBankAccount account = this.context.UserBankAccounts.SingleOrDefault(x => x.UserBankAccountId == userBankAccount.UserBankAccountId);
            account.UserAccountNo = userBankAccount.UserAccountNo;
            account.UserInformationId = userBankAccount.UserInformationId;
            account.BankId = userBankAccount.BankId;
            return this.context.SaveChanges();
        }
        public int Delete(int id)
        {
            UserBankAccount account = this.context.UserBankAccounts.SingleOrDefault(x => x.UserBankAccountId == id);
            this.context.UserBankAccounts.Remove(account);
            return this.context.SaveChanges();
        }
    }
}
