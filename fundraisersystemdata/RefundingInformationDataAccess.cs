using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FundRaiserSystemEntity;
namespace FundRaiserSystemData
{
    class RefundingInformationDataAccess : IRefundingInformationDataAccess
    {
        private FundRaiserDBContext context;
        public RefundingInformationDataAccess(FundRaiserDBContext context)
        {
            this.context = context;
        }

        public IEnumerable<RefundingInformation> GetAll(bool includeDonationOnPosts = false, bool includeAdministrations = false)
        {
            if (includeDonationOnPosts || includeAdministrations)
            {
                return this.context.RefundingInformations.Include("DonationOnPost").Include("Administration").ToList();
            }
            else
            {
                return this.context.RefundingInformations.ToList();
            }
        }
        public RefundingInformation Get(int id, bool includeDonationOnPosts = false, bool includeAdministrations = false)
        {
            if (includeDonationOnPosts || includeAdministrations)
            {
                return this.context.RefundingInformations.Include("DonationOnPost").Include("Administration").SingleOrDefault(x => x.RefundId == id);
            }
            else
            {
                return this.context.RefundingInformations.SingleOrDefault(x => x.RefundId == id);
            }
        }
        public int Insert(RefundingInformation refundingInformation)
        {
            this.context.RefundingInformations.Add(refundingInformation);
            return this.context.SaveChanges();

        }
        public int Update(RefundingInformation refundingInformation)
        {
            RefundingInformation refund = this.context.RefundingInformations.SingleOrDefault(x => x.RefundId== refundingInformation.RefundId);
            refund.RefundAmount = refundingInformation.RefundAmount;
            refund.PostId = refundingInformation.PostId;
            refund.AdminId = refundingInformation.AdminId;
            return this.context.SaveChanges();
        }
        public int Delete(int id)
        {
            RefundingInformation refund = this.context.RefundingInformations.SingleOrDefault(x => x.RefundId == id);
            this.context.RefundingInformations.Remove(refund);

            return this.context.SaveChanges();
        }
    }
}
