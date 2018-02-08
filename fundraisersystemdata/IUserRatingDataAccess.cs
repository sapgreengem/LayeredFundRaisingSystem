using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FundRaiserSystemEntity;

namespace FundRaiserSystemData
{
    public interface IUserRatingDataAccess
    {
        IEnumerable<UserRating> GetAll(bool includeFundRequestPosts = false, bool includeUserInformations = false);
        UserRating Get(int id, bool includeFundRequestPosts = false, bool includeUserInformations = false);
        int Insert(UserRating userRating);
        int Update(UserRating userRating);
        int Delete(int id);
    }
}
