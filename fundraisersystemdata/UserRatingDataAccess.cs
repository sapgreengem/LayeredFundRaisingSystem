using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FundRaiserSystemEntity;

namespace FundRaiserSystemData
{
    class UserRatingDataAccess: IUserRatingDataAccess
    {
        private FundRaiserDBContext context;
        public UserRatingDataAccess(FundRaiserDBContext context)
        {
            this.context = context;
        }
        public IEnumerable<UserRating> GetAll(bool includeFundRequestPosts = false, bool includeUserInformations = false)
        {
            if (includeFundRequestPosts || includeUserInformations)
            {
                return this.context.UserRatings.Include("FundRequestPost").Include("UserInformation").ToList();
            }
            else
            {
                return this.context.UserRatings.ToList();
            }
        }
        public UserRating Get(int id, bool includeFundRequestPosts = false, bool includeUserInformations = false)
        {
            if (includeFundRequestPosts || includeUserInformations)
            {
                return this.context.UserRatings.Include("FundRequestPost").Include("UserInformation").SingleOrDefault(x => x.RatingId == id);
            }
            else
            {
                return this.context.UserRatings.SingleOrDefault(x => x.RatingId == id);
            }
        }
        public int Insert(UserRating userRating)
        {
            this.context.UserRatings.Add(userRating);
            return this.context.SaveChanges();
        }
        public int Update(UserRating userRating)
        {
            UserRating rating = this.context.UserRatings.SingleOrDefault(x => x.RatingId == userRating.RatingId);
            rating.Rating = userRating.Rating;
            rating.PostId = userRating.PostId;
            rating.UserInformationId = userRating.UserInformationId;
            return this.context.SaveChanges();
        }
        public int Delete(int id)
        {
            UserRating rating = this.context.UserRatings.SingleOrDefault(x => x.RatingId == id);
            this.context.UserRatings.Remove(rating);
            return this.context.SaveChanges();
        }
    }
}
