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
            return View();
        }
    }
}