using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FundRaiserSystemData;
using FundRaiserSystemEntity;
using FundRaiserSystemService;

namespace layeredFundRaiserSystem.Controllers
{
    public class ManageTimeExtendController : BaseAdminController
    {
        // GET: ManageTimeExtend
        [HttpGet]
        public ActionResult Index()
        {
            IFundRequestPostService postService = ServiceFactory.GetFundRequestPostService();
            IEnumerable<FundRequestPost> post = postService.GetAll().Where(a => a.PostStatus == "ExtendRequest");

            ViewBag.ExtendRequests = post;
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