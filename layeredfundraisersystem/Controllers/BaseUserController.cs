using layeredFundRaiserSystem.Models;
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
            //else if (Session["Login"] != null)
            //{
            //    int check = 0;
            //    IUserInformationService service = ServiceFactory.GetUserInformationService();
            //    check = service.GetAll().Where(a => a.UserId == Convert.ToInt32(Session["Login"])).Count();
            //    if (check <= 0)
            //    {
            //        Response.Redirect("/FillAllInfo/Index");
            //    }
            //}

            if (Session["UserInformationId"] != null)
            {
                ShowUserName name = new ShowUserName();
                ViewBag.LoginName = name.UserName(Convert.ToInt32(Session["UserInformationId"]));
            }


            
        }
    }
}