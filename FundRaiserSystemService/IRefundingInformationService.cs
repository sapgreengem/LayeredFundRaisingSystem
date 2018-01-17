using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FundRaiserSystemEntity;
using FundRaiserSystemData;

namespace FundRaiserSystemService
{
    public interface IRefundingInformationService
    {
        IEnumerable<RefundingInformation> GetAll(bool includeDonationOnPosts = false, bool includeAdministrations = false);
        RefundingInformation Get(int id, bool includeDonationOnPosts = false, bool includeAdministrations = false);
        int Insert(RefundingInformation refundingInformation);
        int Update(RefundingInformation refundingInformation);
        int Delete(int id);
    }
}
