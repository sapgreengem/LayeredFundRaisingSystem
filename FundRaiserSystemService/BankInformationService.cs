using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FundRaiserSystemEntity;
using FundRaiserSystemData;

namespace FundRaiserSystemService
{
    class BankInformationService: IBankInformationService
    {
        private IBankInformationDataAccess data;
        public BankInformationService(IBankInformationDataAccess data)
        {
            this.data = data;
        }
        public IEnumerable<BankInformation> GetAll()
        {
            return this.data.GetAll();
        }
        public BankInformation Get(int id)
        {
            return this.data.Get(id);

        }
        public int Insert(BankInformation bankInfo)
        {
            return this.data.Insert(bankInfo);

        }
        public int Update(BankInformation bankInfo)
        {
            return this.data.Update(bankInfo);

        }
        public int Delete(int id)
        {
            return this.data.Delete(id);

        }
    }
}
