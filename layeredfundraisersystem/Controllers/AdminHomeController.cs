using FundRaiserSystemData;
using FundRaiserSystemEntity;
using FundRaiserSystemService;
using layeredFundRaiserSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace layeredFundRaiserSystem.Controllers
{
    public class AdminHomeController : BaseAdminController
    {

        [HttpGet]
        public ActionResult Index()
        {
            CountPendings count = new CountPendings();
            ViewBag.Posts = count.CountPendingPost();
            ViewBag.Withdraws = count.CountPendingWithdraws();

            ShowUserName name = new ShowUserName();
            ViewBag.adminName = name.AdminName(Convert.ToInt32(Session["AdminLogin"]));
            return View();
        }
        [HttpGet]
        public ActionResult TopDonor()
        {
            CountPendings count = new CountPendings();
            ViewBag.Posts = count.CountPendingPost();
            ViewBag.Withdraws = count.CountPendingWithdraws();

            ShowUserName name = new ShowUserName();
            ViewBag.adminName = name.AdminName(Convert.ToInt32(Session["AdminLogin"]));

            IDonationOnPostService donationService = ServiceFactory.GetDonationOnPostService();
            IUserInformationService infoService = ServiceFactory.GetUserInformationService();
            IEnumerable<UserInformation> userInformation = infoService.GetAll();

            ViewBag.TopDonarList = userInformation;
            List<double> val = new List<double>();

            foreach (var item in userInformation)
            {
                val.Add(donationService.GetAll().Where(a => a.UserInformationId == item.UserInformationId).Sum(a => a.DonationAmount));
            }
            ViewBag.Sum = val;
            return View();
        }
        [HttpGet]
        public ActionResult TopDonatedPosts()
        {
            CountPendings count = new CountPendings();
            ViewBag.Posts = count.CountPendingPost();
            ViewBag.Withdraws = count.CountPendingWithdraws();

            ShowUserName name = new ShowUserName();
            ViewBag.adminName = name.AdminName(Convert.ToInt32(Session["AdminLogin"]));

            ViewBag.TopDonatedPosts = this.LoadPosts().OrderByDescending(a => a.CollectedAmount);
            return View();
        }
        public ActionResult CollectedAmmountOfMonth()
        {
            CountPendings count = new CountPendings();
            ViewBag.Posts = count.CountPendingPost();
            ViewBag.Withdraws = count.CountPendingWithdraws();

            ShowUserName name = new ShowUserName();
            ViewBag.adminName = name.AdminName(Convert.ToInt32(Session["AdminLogin"]));

            return View();
        }
        public ActionResult MostViewedPosts()
        {
            CountPendings count = new CountPendings();
            ViewBag.Posts = count.CountPendingPost();
            ViewBag.Withdraws = count.CountPendingWithdraws();

            ShowUserName name = new ShowUserName();
            ViewBag.adminName = name.AdminName(Convert.ToInt32(Session["AdminLogin"]));

            ViewBag.MostViewedPosts = this.LoadPosts().OrderByDescending(a => a.ClickCounter);

            return View();
        }

        public IEnumerable<JoinFundRequestPost_Category_UserInfo> LoadPosts()
        {
            IFundRequestPostService postService = ServiceFactory.GetFundRequestPostService();
            IEnumerable<FundRequestPost> post = postService.GetAll(true, true, false); // Include User & Ctegory
            List<JoinFundRequestPost_Category_UserInfo> joinData = new List<JoinFundRequestPost_Category_UserInfo>();
            foreach (FundRequestPost item in post)
            {
                JoinFundRequestPost_Category_UserInfo load = new JoinFundRequestPost_Category_UserInfo()
                {
                    PostId = item.PostId,
                    PostTitle = item.PostTitle,
                    RequiredAmount = item.RequiredAmount,
                    StartDate = item.StartDate,
                    EndDate = item.EndDate,
                    CategoryName = item.PostingCategory.CategoryName,
                    FirstName = item.UserInformation.FirstName,
                    LastName = item.UserInformation.LastName,
                    UserId = item.UserInformation.UserId,
                    UserInformationId = item.UserInformationId,
                    ClickCounter = item.ClickCounter,
                    CollectedAmount = item.CollectedAmount

                };
                joinData.Add(load);
            }
            return joinData.ToList();
        }
    }
}