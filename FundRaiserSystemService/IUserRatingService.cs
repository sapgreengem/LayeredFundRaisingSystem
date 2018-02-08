using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FundRaiserSystemEntity;
using FundRaiserSystemData;

namespace FundRaiserSystemService
{
    public interface IUserRatingService
    {
        IEnumerable<UserRating> GetAll(bool includeFundRequestPosts = false, bool includeUserInformations = false);
        UserRating Get(int id, bool includeFundRequestPosts = false, bool includeUserInformations = false);
        int Insert(UserRating userRating);
        int Update(UserRating userRating);
        int Delete(int id);
    }
}
