using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FundRaiserSystemEntity;
namespace FundRaiserSystemData
{
    class FundRequestPostDataAccess: IFundRequestPostDataAccess
    {
        private FundRaiserDBContext context;
        public FundRequestPostDataAccess(FundRaiserDBContext context)
        {
            this.context = context;
        }

        public IEnumerable<FundRequestPost> GetAll(bool includeUserInformations = false, bool includeCategories = false, bool includeRefunds = false)
        {
            if (includeUserInformations || includeCategories || includeRefunds)
            {
                return this.context.FundRequestPosts.Include("UserInformation").Include("PostingCategory").Include("RefundingInformation").ToList();
            }
            else
            {
                return this.context.FundRequestPosts.ToList();
            }

        }
        public FundRequestPost Get(int id, bool includeUserInformations = false, bool includeCategories = false, bool includeRefunds = false)
        {
            if (includeUserInformations || includeCategories || includeRefunds)
            {
                return this.context.FundRequestPosts.Include("UserInformation").Include("PostingCategory").Include("RefundingInformation").SingleOrDefault(x => x.PostId == id);
            }
            else
            {
                return this.context.FundRequestPosts.SingleOrDefault(x => x.PostId == id);
            }
            
        }
        public int Insert(FundRequestPost fundRequestPost)
        {
            this.context.FundRequestPosts.Add(fundRequestPost);
            return this.context.SaveChanges();

        }
        public int Update(FundRequestPost fundRequestPost)
        {
            FundRequestPost post = this.context.FundRequestPosts.SingleOrDefault(x => x.PostId == fundRequestPost.PostId);
            post.PostTitle = fundRequestPost.PostTitle;
            post.PostDetails = fundRequestPost.PostDetails;
            post.RequiredAmount = fundRequestPost.RequiredAmount;
            post.StartDate = fundRequestPost.StartDate;
            post.EndDate = fundRequestPost.EndDate;
            post.CollectedAmount = fundRequestPost.CollectedAmount;
            post.RemainingAmount = fundRequestPost.RemainingAmount;
            post.PostImage = fundRequestPost.PostImage;
            post.PostStatus = fundRequestPost.PostStatus;
            post.UserInformationId = fundRequestPost.UserInformationId;
            post.CategoryId = fundRequestPost.CategoryId;
            post.RefundId = fundRequestPost.RefundId;
            post.ClickCounter = fundRequestPost.ClickCounter;

            return this.context.SaveChanges();
        }
        public int Delete(int id)
        {
            FundRequestPost post = this.context.FundRequestPosts.SingleOrDefault(x => x.PostId == id);
            this.context.FundRequestPosts.Remove(post);

            return this.context.SaveChanges();
        }

        public IEnumerable<FundRequestPost> GetActivePost()
        {
            var posts = (from FundRequestPost in this.context.FundRequestPosts
                         where FundRequestPost.PostStatus.Contains("Active")
                         select FundRequestPost).ToList();
            return posts;
        }
    }
}
