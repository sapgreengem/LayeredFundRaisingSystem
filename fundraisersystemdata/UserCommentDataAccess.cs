using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FundRaiserSystemEntity;
namespace FundRaiserSystemData
{
    class UserCommentDataAccess : IUserCommentDataAccess
    {
        private FundRaiserDBContext context;
        public UserCommentDataAccess(FundRaiserDBContext context)
        {
            this.context = context;
        }
        public IEnumerable<UserComment> GetAll(bool includeFundRequestPosts = false, bool includeUserInformations = false)
        {
            //if (includeFundRequestPosts)
            //{
            //    return this.context.UserComments.Include("FundRequestPost").ToList();
            //}
            //else if (includeUserInformations)
            //{
            //    return this.context.UserComments.Include("UserInformation").ToList();
            //}
            if (includeFundRequestPosts || includeUserInformations)
            {
                return this.context.UserComments.Include("FundRequestPost").Include("UserInformation").ToList();
            }
            else
            {
                return this.context.UserComments.ToList();
            }
        }
        public UserComment Get(int id, bool includeFundRequestPosts = false, bool includeUserInformations = false)
        {
            //if (includeFundRequestPosts)
            //{
            //    return this.context.UserComments.Include("FundRequestPost").SingleOrDefault(x=> x.CommentId == id);
            //}
            //else if (includeUserInformations)
            //{
            //    return this.context.UserComments.Include("UserInformation").SingleOrDefault(x => x.CommentId == id);
            //}
            if (includeFundRequestPosts || includeUserInformations)
            {
                return this.context.UserComments.Include("FundRequestPost").Include("UserInformation").SingleOrDefault(x => x.CommentId == id);
            }
            else
            {
                return this.context.UserComments.SingleOrDefault(x => x.CommentId == id);
            }
        }
        public int Insert(UserComment userComment)
        {
            this.context.UserComments.Add(userComment);
            return this.context.SaveChanges();
        }
        public int Update(UserComment userComment)
        {
            UserComment comment = this.context.UserComments.SingleOrDefault(x => x.CommentId == userComment.CommentId);
            comment.Comment = userComment.Comment;
            comment.PostId = userComment.PostId;
            comment.UserInformationId = userComment.UserInformationId;
            return this.context.SaveChanges();
        }
        public int Delete(int id)
        {
            UserComment comment = this.context.UserComments.SingleOrDefault(x => x.CommentId == id);
            this.context.UserComments.Remove(comment);
            return this.context.SaveChanges();
        }
    }
}
