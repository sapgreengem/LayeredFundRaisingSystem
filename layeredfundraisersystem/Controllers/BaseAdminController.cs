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
        }
    }
}