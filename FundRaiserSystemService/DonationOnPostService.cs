using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FundRaiserSystemEntity;
using FundRaiserSystemData;

namespace FundRaiserSystemService
{
    class DonationOnPostService : IDonationOnPostService
    {
        private IDonationOnPostDataAccess data;
        public DonationOnPostService(IDonationOnPostDataAccess data)
        {
            this.data = data;
        }
        public IEnumerable<DonationOnPost> GetAll(bool includeUserInformations = false, bool includePosts = false, bool includeTransections = false)
        {
            return this.data.GetAll(includeUserInformations, includePosts, includeTransections);
        }
        public DonationOnPost Get(int id, bool includeUserInformations = false, bool includePosts = false, bool includeTransections = false)
        {
            return this.data.Get(id, includeUserInformations, includePosts, includeTransections);
        }
        public int Insert(DonationOnPost donationOnPost)
        {
            return this.data.Insert(donationOnPost);
        }
        public int Update(DonationOnPost donationOnPost)
        {
            return this.data.Update(donationOnPost);
        }
        public int Delete(int id)
        {
            return this.data.Delete(id);
        }
    }
}
