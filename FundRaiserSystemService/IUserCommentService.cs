using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FundRaiserSystemEntity;

namespace FundRaiserSystemData
{
    public interface IUserCommentService
    {
        IEnumerable<UserComment> GetAll(bool includeFundRequestPosts = false, bool includeUserInformations = false);
        UserComment Get(int id, bool includeFundRequestPosts = false, bool includeUserInformations = false);
        int Insert(UserComment userComment);
        int Update(UserComment userComment);
        int Delete(int id);
    }
}
