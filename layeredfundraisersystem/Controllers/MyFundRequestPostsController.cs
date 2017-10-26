using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FundRaiserSystemData;
using FundRaiserSystemEntity;
using FundRaiserSystemService;
using layeredFundRaiserSystem.Models;

namespace layeredFundRaiserSystem.Controllers
{
    public class MyFundRequestPostsController : BaseUserController
    {
        // GET: MyFundRequestPosts
        public ActionResult Index()
        {
            IFundRequestPostService service = ServiceFactory.GetFundRequestPostService();
            ViewBag.MyRunningPosts = service.GetAll()
                .Where(a => a.UserInformationId == Convert.ToInt32(Session["UserInformationId"]))
                .Where(a => a.PostStatus == "Active");

            ViewBag.MyCompletedPosts = service.GetAll()
                .Where(a => a.UserInformationId == Convert.ToInt32(Session["UserInformationId"]))
                .Where(a => a.PostStatus == "Completed");

            ViewBag.MyExpiredPosts = service.GetAll()
                .Where(a => a.UserInformationId == Convert.ToInt32(Session["UserInformationId"]))
                .Where(a => a.PostStatus == "Expired");

            ViewBag.CountNumberofPosts = service.GetAll()
                .Where(a => a.UserInformationId == Convert.ToInt32(Session["UserInformationId"]))
                .Count();

            ViewBag.CountMyCompletedPosts = service.GetAll()
                .Where(a => a.UserInformationId == Convert.ToInt32(Session["UserInformationId"]))
                .Where(a => a.PostStatus == "Completed")
                .Count();
            ViewBag.SumCollectedAmount = service.GetAll()
                .Where(a => a.UserInformationId == Convert.ToInt32(Session["UserInformationId"]))
                .Sum(a=> a.CollectedAmount);

            ViewBag.SumRemainingAmount = service.GetAll()
                .Where(a => a.UserInformationId == Convert.ToInt32(Session["UserInformationId"]))
                .Sum(a => a.RemainingAmount);


            IFundWithdrawService withdrawService = ServiceFactory.GetFundWithdrawService();
            ViewBag.SumWithdrawanAmount = withdrawService.GetAll()
                .Where(a => a.UserInformationId == Convert.ToInt32(Session["UserInformationId"]))
                .Sum(a=> a.WithdrawAmount);

            return View();
        }
    }
}