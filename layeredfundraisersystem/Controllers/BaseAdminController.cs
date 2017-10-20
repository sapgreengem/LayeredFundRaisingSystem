using layeredFundRaiserSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace layeredFundRaiserSystem.Controllers
{
    public class BaseAdminController : Controller
    {
        // GET: BaseAdmin
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            // Enable the following code to activate session check

            if (Session["AdminLogin"] == null)
            {
                Response.Redirect("/Login/AdminLogin");
            }

            CountPendings count = new CountPendings();
            ViewBag.Posts = count.CountPendingPost();
            ViewBag.Withdraws = count.CountPendingWithdraws();
            ViewBag.CountTimeExtendReq = count.CountTimeExtendReq();

            ShowUserName name = new ShowUserName();
            ViewBag.adminName = name.AdminName(Convert.ToInt32(Session["AdminLogin"]));
        }
    }
}