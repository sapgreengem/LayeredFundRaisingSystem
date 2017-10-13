using layeredFundRaiserSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace layeredFundRaiserSystem.Controllers
{
    public class AdminReportsController : BaseAdminController
    {
        // GET: Reports
        public ActionResult Index()
        {
            CountPendings count = new CountPendings();
            ViewBag.Posts = count.CountPendingPost();
            ViewBag.Withdraws = count.CountPendingWithdraws();

            ShowUserName name = new ShowUserName();
            ViewBag.adminName = name.AdminName(Convert.ToInt32(Session["AdminLogin"]));

            return View();
        }
    }
}