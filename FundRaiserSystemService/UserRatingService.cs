using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FundRaiserSystemEntity;
using FundRaiserSystemData;

namespace FundRaiserSystemService
{
    class UserRatingService: IUserRatingService
    {
        private IUserRatingDataAccess data;
        public UserRatingService(IUserRatingDataAccess data)
        {
            this.data = data;
        }
        public IEnumerable<UserRating> GetAll(bool includeFundRequestPosts = false, bool includeUserInformations = false)
        {
            return this.data.GetAll(includeFundRequestPosts, includeUserInformations);
        }
        public UserRating Get(int id, bool includeFundRequestPosts = false, bool includeUserInformations = false)
        {
            return this.data.Get(id, includeFundRequestPosts, includeUserInformations);
        }
        public int Insert(UserRating userRating)
        {
            return this.data.Insert(userRating);
        }
        public int Update(UserRating userRating)
        {
            return this.data.Update(userRating);
        }
        public int Delete(int id)
        {
            return this.data.Delete(id);
        }

        public UserRating GetSingle(int postID, int userInfo)
        {
            return this.data.GetSingle(postID, userInfo);
        }
    }
}
