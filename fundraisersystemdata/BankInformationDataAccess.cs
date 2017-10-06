using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FundRaiserSystemEntity;
namespace FundRaiserSystemData
{
    class BankInformationDataAccess : IBankInformationDataAccess
    {
        private FundRaiserDBContext context;
        public BankInformationDataAccess(FundRaiserDBContext context)
        {
            this.context = context;
        }

        public IEnumerable<BankInformation> GetAll()
        {
            return this.context.BankInformations.ToList();

        }

        public BankInformation Get(int id)
        {
            return this.context.BankInformations.SingleOrDefault(x => x.BankId == id);
        }

        public int Insert(BankInformation bankInfo)
        {
            this.context.BankInformations.Add(bankInfo);
            return this.context.SaveChanges();
        }

        public int Update(BankInformation bankInfo)
        {
            BankInformation bankInformation = this.context.BankInformations.SingleOrDefault(x => x.BankId == bankInfo.BankId);
            bankInformation.BankName = bankInfo.BankName;
            bankInformation.BranchName = bankInfo.BranchName;

            return this.context.SaveChanges();
        }

        public int Delete(int id)
        {
            BankInformation bankInfo = this.context.BankInformations.SingleOrDefault(x => x.BankId == id);
            this.context.BankInformations.Remove(bankInfo);

            return this.context.SaveChanges();
        }
    }
}
