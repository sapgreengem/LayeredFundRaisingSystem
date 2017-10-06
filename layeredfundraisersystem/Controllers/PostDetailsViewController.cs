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

        // GET: PostDetailsView
        public ActionResult Index(int id)
        {
            if (Session["Login"] != null)
            {
                ShowUserName name = new ShowUserName();
                ViewBag.LoginName = name.UserName(Convert.ToInt32(Session["UserInformationId"]));
            }
            FundRequestPost post = service.Get(id, true, true, false);//.Where(a=> a.PostStatus == "Active");
            post.ClickCounter += 1;
            service.Update(post);

            ViewBag.UserName = this.loadFundPostUserName(id);
            ViewBag.PercentageFunded = (post.CollectedAmount / post.RequiredAmount) * 100;

            ViewBag.NumberOfPeopleDonated = donateService.GetAll().Where(q => q.PostId == id).Count();

            ViewBag.DonorList = this.loadAllDonor(id);

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
    }
}