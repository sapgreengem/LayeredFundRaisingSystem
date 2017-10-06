using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FundRaiserSystemEntity;
namespace FundRaiserSystemData
{
    class UserCommentService : IUserCommentService
    {
        private IUserCommentDataAccess data;
        public UserCommentService(IUserCommentDataAccess data)
        {
            this.data = data;
        }
        public IEnumerable<UserComment> GetAll(bool includeFundRequestPosts = false, bool includeUserInformations = false)
        {
            return this.data.GetAll(includeFundRequestPosts, includeUserInformations);
        }
        public UserComment Get(int id, bool includeFundRequestPosts = false, bool includeUserInformations = false)
        {
            return this.data.Get(id, includeFundRequestPosts, includeUserInformations);
        }
        public int Insert(UserComment userComment)
        {
            return this.data.Insert(userComment);
        }
        public int Update(UserComment userComment)
        {
            return this.data.Update(userComment);
        }
        public int Delete(int id)
        {
            return this.data.Delete(id);
        }
    }
}
