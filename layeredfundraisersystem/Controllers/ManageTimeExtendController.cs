using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FundRaiserSystemEntity;
using FundRaiserSystemService;
using layeredFundRaiserSystem.Models;

namespace layeredFundRaiserSystem.Controllers
{
    public class ManageTimeExtendController : BaseAdminController
    {
        // GET: ManageTimeExtend
        [HttpGet]
        public ActionResult Index()
        {
            //JoinFundRequestPost_UserInformation
            IFundRequestPostService postService = ServiceFactory.GetFundRequestPostService();
            IEnumerable<FundRequestPost> post = postService.GetAll(true).Where(a => a.PostStatus == "ExtendRequest");

            List<JoinFundRequestPost_UserInformation> joinData = new List<JoinFundRequestPost_UserInformation>();
            foreach (var item in post)
            {
                JoinFundRequestPost_UserInformation addData = new JoinFundRequestPost_UserInformation()
                {
                    PostId = item.PostId,
                    PostTitle = item.PostTitle,
                    StartDate = item.StartDate,
                    EndDate = item.EndDate,
                    RequiredAmount = item.RequiredAmount,
                    CollectedAmount = item.CollectedAmount,
                    PostStatus = item.PostStatus,
                    UserInformationId = item.UserInformationId,
                    FirstName = item.UserInformation.FirstName
                };
                joinData.Add(addData);
            }
            ViewBag.ExtendRequests = joinData.ToList();
            return View();
        }

        [HttpGet]
        public ActionResult ApproveReq(int id)
        {
            IFundRequestPostService postService = ServiceFactory.GetFundRequestPostService();
            FundRequestPost fundRequestPost = postService.Get(id);
            fundRequestPost.PostStatus = "Active";
            postService.Update(fundRequestPost);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult RejectReq(int id)
        {
            IFundRequestPostService postService = ServiceFactory.GetFundRequestPostService();
            FundRequestPost fundRequestPost = postService.Get(id);
            fundRequestPost.PostStatus = "Expired";
            postService.Update(fundRequestPost);
            return RedirectToAction("Index");
        }
    }
}