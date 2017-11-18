using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace layeredFundRaiserSystem.Controllers
{
    public class LogoutController : Controller
    {
        // GET: Logout
        public ActionResult Index()
        {
            if (Session["Login"]!=null || Session["AdminLogin"]!=null || Session["UserInformationId"]!= null)
            {
                Session["Login"] = null;
                Session["AdminLogin"] = null;
                Session["UserInformationId"] = null;
                Session["RedirectToAddFundRequest"] = null;
                Session["RedirectToDonateOnPost"] = null;
            }
            return Redirect("/Home");
        }
    }
}