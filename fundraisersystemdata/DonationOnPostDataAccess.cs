using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FundRaiserSystemEntity;

namespace FundRaiserSystemData
{
    class DonationOnPostDataAccess: IDonationOnPostDataAccess
    {
        private FundRaiserDBContext context;
        public DonationOnPostDataAccess(FundRaiserDBContext context)
        {
            this.context = context;
        }

        public IEnumerable<DonationOnPost> GetAll(bool includeUserInformations = false, bool includePosts = false, bool includeTransections = false)
        {
            if (includeUserInformations || includePosts || includeTransections)
            {
                return this.context.DonationOnPosts.Include("OnlineTransection").Include("FundRequestPost").Include("UserInformation").ToList();
            }
            else
            {
                return this.context.DonationOnPosts.ToList();
            }
        }
        public DonationOnPost Get(int id, bool includeUserInformations = false, bool includePosts = false, bool includeTransections = false)
        {
            if (includeUserInformations || includePosts || includeTransections)
            {
                return this.context.DonationOnPosts.Include("OnlineTransection").Include("FundRequestPost").Include("UserInformation").SingleOrDefault(x => x.DonationId == id);
            }
            else
            {
                return this.context.DonationOnPosts.SingleOrDefault(x => x.DonationId == id);
            }
        }

        public int Insert(DonationOnPost donationOnPost)
        {
            this.context.DonationOnPosts.Add(donationOnPost);
            return this.context.SaveChanges();
        }

        public int Update(DonationOnPost donationOnPost)
        {
            DonationOnPost donation = this.context.DonationOnPosts.SingleOrDefault(x => x.DonationId == donationOnPost.DonationId);
            donation.DonationAmount = donationOnPost.DonationAmount;
            donation.DonationDate = donationOnPost.DonationDate;
            donation.PostId = donationOnPost.PostId;
            donation.UserInformationId = donationOnPost.UserInformationId;
            donation.TransectionId = donationOnPost.TransectionId;
            donation.ShowDonationInfo = donationOnPost.ShowDonationInfo;

            return this.context.SaveChanges();
        }

        public int Delete(int id)
        {
            DonationOnPost donationOnPost = this.context.DonationOnPosts.SingleOrDefault(x => x.DonationId == id);
            this.context.DonationOnPosts.Remove(donationOnPost);

            return this.context.SaveChanges();
        }
    }
}
