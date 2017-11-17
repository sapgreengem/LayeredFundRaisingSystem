using layeredFundRaiserSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace layeredFundRaiserSystem.Controllers
{
    public class BaseUserController : Controller
    {
        // GET: Base
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            
            base.OnActionExecuting(filterContext);
            // Enable the following code to activate session check
            if(Session["Login"] == null)
            {
                Response.Redirect("/Login");
            }

            if (Session["UserInformationId"] != null)
            {
                ShowUserName name = new ShowUserName();
                ViewBag.LoginName = name.UserName(Convert.ToInt32(Session["UserInformationId"]));
            }
            
        }
    }
}