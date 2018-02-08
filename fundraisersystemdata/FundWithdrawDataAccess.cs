using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FundRaiserSystemEntity;

namespace FundRaiserSystemData
{
    class FundWithdrawDataAccess: IFundWithdrawDataAccess
    {
        private FundRaiserDBContext context;
        public FundWithdrawDataAccess(FundRaiserDBContext context)
        {
            this.context = context;
        }

        public IEnumerable<FundWithdraw> GetAll(bool includeUserInformations = false, bool includeFundRequestPosts = false)
        {
            if (includeUserInformations || includeFundRequestPosts )
            {
                return this.context.FundWithdraws.Include("UserInformation").Include("FundRequestPost").ToList();
            }
            else
            {
                return this.context.FundWithdraws.ToList();
            }

        }
        public FundWithdraw Get(int id, bool includeUserInformations = false, bool includeFundRequestPosts = false)
        {
            if (includeUserInformations || includeFundRequestPosts)
            {
                return this.context.FundWithdraws.Include("UserInformation").Include("FundRequestPost").SingleOrDefault(x => x.WithdrawId == id);
            }
            else
            {
                return this.context.FundWithdraws.SingleOrDefault(x => x.WithdrawId == id);
            }
        }
        public int Insert(FundWithdraw fundWithdraw)
        {
            this.context.FundWithdraws.Add(fundWithdraw);
            return this.context.SaveChanges();

        }
        public int Update(FundWithdraw fundWithdraw)
        {
            FundWithdraw withdraw = this.context.FundWithdraws.SingleOrDefault(x => x.WithdrawId == fundWithdraw.WithdrawId);
            withdraw.WithdrawAmount = fundWithdraw.WithdrawAmount;
            withdraw.WithdrawWithCharge = fundWithdraw.WithdrawWithCharge;
            withdraw.WithdrawDate = fundWithdraw.WithdrawDate;
            withdraw.RequestStatus = fundWithdraw.RequestStatus;
            withdraw.UserInformationId = fundWithdraw.UserInformationId;
            withdraw.PostId = fundWithdraw.PostId;

            return this.context.SaveChanges();
        }
        public int Delete(int id)
        {
            FundWithdraw withdraw = this.context.FundWithdraws.SingleOrDefault(x => x.WithdrawId == id);
            this.context.FundWithdraws.Remove(withdraw);

            return this.context.SaveChanges();
        }
    }
}
