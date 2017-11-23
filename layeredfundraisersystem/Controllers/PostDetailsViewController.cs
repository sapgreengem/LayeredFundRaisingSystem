using FundRaiserSystemData;
using FundRaiserSystemEntity;
using FundRaiserSystemService;
using layeredFundRaiserSystem.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace layeredFundRaiserSystem.Controllers
{
    public class PostDetailsViewController : Controller
    {
        IFundRequestPostService service = ServiceFactory.GetFundRequestPostService();
        IDonationOnPostService donateService = ServiceFactory.GetDonationOnPostService();
        ShowUserName name = new ShowUserName();
        // GET: PostDetailsView
        public ActionResult Index(int id=0)
        {
            if(id == 0)
            {
                Response.Redirect("/Error");
            }
            if (Session["Login"] != null)
            {
                ViewBag.LoginName = name.UserName(Convert.ToInt32(Session["UserInformationId"]));
            }
            FundRequestPost post = service.Get(id, true, true, false);//.Where(a=> a.PostStatus == "Active");
            post.ClickCounter += 1;
            service.Update(post);

            ViewBag.UserName = this.loadFundPostUserName(id);
            ViewBag.PercentageFunded = (post.CollectedAmount / post.RequiredAmount) * 100;

            ViewBag.NumberOfPeopleDonated = donateService.GetAll().Where(q => q.PostId == id).Count();

            ViewBag.DonorList = this.loadAllDonor(id);

            ViewBag.UserInformationId = Convert.ToInt32(Session["UserInformationId"]);

            return View(post);
        }
        
        public IEnumerable loadAllDonor(int id) //Load All Donors donated on this post
        {

            IEnumerable<DonationOnPost> donation = donateService.GetAll(true, true, false).Where(a => a.PostId == id).OrderByDescending(s=> s.DonationDate); // Include User & donation
            List<JoinDonationOnPost_UserInfo_FundRequest_Transection> list = new List<JoinDonationOnPost_UserInfo_FundRequest_Transection>();
            foreach (DonationOnPost item in donation)
            {
                JoinDonationOnPost_UserInfo_FundRequest_Transection load = new JoinDonationOnPost_UserInfo_FundRequest_Transection()
                {
                    PostId = item.PostId,
                    FirstName = item.UserInformation.FirstName,
                    ProfilePicture = item.UserInformation.ProfilePicture,
                    DonationAmount = item.DonationAmount
                };
                list.Add(load);
            }
            return list;
        }

        public IEnumerable loadFundPostUserName(int id) //Load Username who started this post
        {
            FundRequestPost post = service.Get(id, true, true, false);
            List<JoinFundRequestPost_Category_UserInfo> userInfo = new List<JoinFundRequestPost_Category_UserInfo>();
            JoinFundRequestPost_Category_UserInfo user = new JoinFundRequestPost_Category_UserInfo()
            {
                FirstName = post.UserInformation.FirstName,
                LastName = post.UserInformation.LastName
            };
            userInfo.Add(user);
            return userInfo;
        }

        [HttpGet]
        public JsonResult Comment(int id, int id1, string id2)
        {
            IUserCommentService user = ServiceFactory.GetUserCommentService();

            if (id1 != 0 && id2 != "null")
            {
                UserComment comment = new UserComment();
                comment.PostId = id;
                comment.UserInformationId = id1;
                comment.Comment = id2;
                user.Insert(comment);
            }
            var LatestComments = user.GetAll()
                .Where(a => a.PostId == id)
                .OrderByDescending(a => a.CommentId);

            List<PostComment> postComment = new List<PostComment>();
            foreach (var x in LatestComments)
            {
                PostComment comment = new PostComment();
                comment.UserName = name.UserName(x.UserInformationId);
                comment.UserImage = name.UserImage(x.UserInformationId);
                comment.Comment = x.Comment;

                postComment.Add(comment);
            }
            return Json(new { list = postComment }, JsonRequestBehavior.AllowGet);
        }
    }
}