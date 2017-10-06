using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FundRaiserSystemEntity;
namespace FundRaiserSystemData
{
    class FundWithdrawService : IFundWithdrawService
    {
        private IFundWithdrawDataAccess data;
        public FundWithdrawService(IFundWithdrawDataAccess data)
        {
            this.data = data;
        }
        public IEnumerable<FundWithdraw> GetAll(bool includeUserInformations = false, bool includeFundRequestPosts = false)
        {
            return this.data.GetAll(includeUserInformations, includeFundRequestPosts);
        }
        public FundWithdraw Get(int id, bool includeUserInformations = false, bool includeFundRequestPosts = false)
        {
            return this.data.Get(id, includeUserInformations, includeFundRequestPosts);
        }
        public int Insert(FundWithdraw fundWithdraw)
        {
            return this.data.Insert(fundWithdraw);
        }
        public int Update(FundWithdraw fundWithdraw)
        {
            return this.data.Update(fundWithdraw);
        }
        public int Delete(int id)
        {
            return this.data.Delete(id);
        }
    }
}
