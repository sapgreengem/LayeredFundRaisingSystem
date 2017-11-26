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
            if (Session["Login"] != null && Session["UserInformationId"] == null)
            {
                Response.Redirect("/FillAllInfo/Index");
            }
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
                .Sum(a=> a.CollectedAmount).ToString("0.00");

            ViewBag.SumRemainingAmount = service.GetAll()
                .Where(a => a.UserInformationId == Convert.ToInt32(Session["UserInformationId"]))
                .Sum(a => a.RemainingAmount).ToString("0.00");


            IFundWithdrawService withdrawService = ServiceFactory.GetFundWithdrawService();
            ViewBag.SumWithdrawanAmount = withdrawService.GetAll()
                .Where(a => a.RequestStatus == "Transfered")
                .Where(a => a.UserInformationId == Convert.ToInt32(Session["UserInformationId"]))
                .Sum(a=> a.WithdrawAmount).ToString("0.00");
            ViewBag.SumWithdrawWithCharge = withdrawService.GetAll()
                .Where(a => a.RequestStatus == "Transfered")
                .Where(a => a.UserInformationId == Convert.ToInt32(Session["UserInformationId"]))
                .Sum(a => a.WithdrawWithCharge).ToString("0.00");
            return View();
        }
    }
}