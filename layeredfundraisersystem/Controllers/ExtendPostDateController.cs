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
    public class ExtendPostDateController : BaseUserController
    {
        // GET: ExtendPostDate
        [HttpGet]
        public ActionResult Index(int id)
        {
            IFundRequestPostService postService = ServiceFactory.GetFundRequestPostService();
            FundRequestPost selectPost = postService.Get(id);
            return View(selectPost);
        }

        [HttpPost]
        public ActionResult Index(int id, FormCollection coll)
        {
            IFundRequestPostService postService = ServiceFactory.GetFundRequestPostService();
            FundRequestPost selectPost = postService.Get(id);

            if (!String.IsNullOrWhiteSpace(coll["days"].ToString()))
            {
                int check = postService.GetAll().Where(a => a.UserInformationId == Convert.ToInt32(Session["UserInformationId"])).Where(a => a.PostId == id).Count();
                if (check > 0)
                {
                    var count = postService.GetAll().Where(a => a.PostId == selectPost.PostId).Where(a => a.PostStatus == "ExtendRequest").Count();
                    if (count > 0)
                    {
                        ViewBag.ErrorMessage = "Already Requested";
                    }
                    else
                    {
                        FundRequestPost post = postService.Get(id);
                        post.EndDate = DateTime.Now.AddDays(Convert.ToDouble(coll["days"]));
                        post.PostStatus = "ExtendRequest";

                        postService.Update(post);
                        ViewBag.ErrorMessage = "Request Sent";

                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "You Can Only Request to extend days from your own post";
                }
                
            }
            else
            {
                ViewBag.ErrorMessage = "Please give number of extended days";
            }
            return View(selectPost);
        }

        [HttpGet]
        public ActionResult MyTimeExtend()
        {
            IFundRequestPostService service = ServiceFactory.GetFundRequestPostService();
            IEnumerable<FundRequestPost> rows = service.GetAll().Where(a => a.PostStatus == "ExtendRequest").Where(a => a.UserInformationId == Convert.ToInt32(Session["UserInformationId"]));

            if (rows.Count() <= 0)
            {
                ViewBag.ErrorMessage = "No Request available";
            }
            else
            {
                ViewBag.AllRequests = service.GetAll().Where(a => a.PostStatus == "ExtendRequest").Where(a => a.UserInformationId == Convert.ToInt32(Session["UserInformationId"]));
            }

            return View();
        }
    }
}