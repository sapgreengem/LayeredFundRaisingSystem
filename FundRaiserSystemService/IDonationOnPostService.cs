using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FundRaiserSystemEntity;

namespace FundRaiserSystemData
{
    public interface IDonationOnPostService
    {
        IEnumerable<DonationOnPost> GetAll(bool includeUserInformations = false, bool includePosts = false, bool includeTransections = false);
        DonationOnPost Get(int id, bool includeUserInformations = false, bool includePosts = false, bool includeTransections = false);
        int Insert(DonationOnPost donationOnPost);
        int Update(DonationOnPost donationOnPost);
        int Delete(int id);
    }
}
