using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FundRaiserSystemEntity;
namespace FundRaiserSystemData
{
    class RefundingInformationService : IRefundingInformationService
    {
        private IRefundingInformationDataAccess data;
        public RefundingInformationService(IRefundingInformationDataAccess data)
        {
            this.data = data;
        }
        public IEnumerable<RefundingInformation> GetAll(bool includeDonationOnPosts = false, bool includeAdministrations = false)
        {
            return this.data.GetAll(includeDonationOnPosts,includeAdministrations);
        }
        public RefundingInformation Get(int id, bool includeDonationOnPosts = false, bool includeAdministrations = false)
        {
            return this.data.Get(id, includeDonationOnPosts, includeAdministrations);
        }
        public int Insert(RefundingInformation refundingInformation)
        {
            return this.data.Insert(refundingInformation);
        }
        public int Update(RefundingInformation refundingInformation)
        {
            return this.data.Update(refundingInformation);
        }
        public int Delete(int id)
        {
            return this.data.Delete(id);
        }
    }
}
