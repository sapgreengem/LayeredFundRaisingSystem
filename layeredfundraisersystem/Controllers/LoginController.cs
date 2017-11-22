using FundRaiserSystemEntity;
using FundRaiserSystemData;
using FundRaiserSystemService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace layeredFundRaiserSystem.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            if (Session["Login"] == null && Session["AdminLogin"] == null)
            {
                return View();
            }
            else if (Session["Login"] == null && Session["AdminLogin"] != null)
            {
                return Redirect("~/AdminHome");
            }
            else
            {
                return Redirect("~/Home");
            }
        }
        [HttpPost]
        public ActionResult Index(FormCollection collection)
        {
            if (!String.IsNullOrWhiteSpace(collection["email"].ToString()) && !String.IsNullOrWhiteSpace(collection["password"].ToString()))
            {
                IUserLoginService service = ServiceFactory.GetUserLoginService();
                UserLogin user = service.GetUser(collection["email"].ToString(), collection["password"].ToString(), "Active");

                if (user != null)
                {
                    Session["Login"] = user.UserId;
 
                    if (this.getUserInfoId(user.UserId) > 0)
                    {
                        Session["UserInformationId"] = this.getUserInfoId(user.UserId);
                        if (Session["RedirectToDonateOnPost"] != null)
                        {
                            Response.Redirect(Session["RedirectToDonateOnPost"].ToString());
                        }
                        else if (Session["RedirectToAddFundRequest"] != null)
                        {
                            Response.Redirect(Session["RedirectToAddFundRequest"].ToString());
                        }
                    }
                    else
                    {
                        return RedirectToAction("Index", "FillAllInfo");
                    }
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.ErrorMessage = "Email and Password Do not match";
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Please Provide Email and Password";
            }
            return View();
        }

        [HttpGet]
        public ActionResult AdminLogin()
        {
            if (Session["Login"] == null && Session["AdminLogin"] == null)
            {
                return View();
            }
            else if (Session["Login"] != null && Session["AdminLogin"] == null)
            {
                return Redirect("~/Home");
            }
            else
            {
                return Redirect("~/AdminHome");
            }
        }
        [HttpPost]
        public ActionResult AdminLogin(FormCollection collection)
        {
            if (!String.IsNullOrWhiteSpace(collection["email"].ToString()) && !String.IsNullOrWhiteSpace(collection["password"].ToString()))
            {
                IAdministrationService service = ServiceFactory.GetAdministrationService();

                int countRow = service.GetAll().Where(email => email.Email == collection["email"].ToString())
                                        .Where(pass => pass.Password == collection["password"].ToString())
                                        .Count();
                if (countRow == 1)
                {
                    IEnumerable<Administration> admin = service.GetAll().Where(email => email.Email == collection["email"].ToString())
                                                                        .Where(pass => pass.Password == collection["password"].ToString());
                    foreach (var item in admin)
                    {
                        Session["AdminLogin"] = item.AdminId;
                    }
                    countRow = 0;
                    return RedirectToAction("Index", "AdminHome");
                }
                else
                {
                    ViewBag.ErrorMessage = "Email and Password Do not match";
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Please Provide Email and Password";
            }

            return View();
        }

        public int getUserInfoId(int id)
        {
            int userInfoId = 0;
            IUserInformationService userInfoService = ServiceFactory.GetUserInformationService();
            UserInformation user = userInfoService.GetAll().Where(a => a.UserId == Convert.ToInt32(id)).FirstOrDefault();
            userInfoId = user.UserInformationId;
            return userInfoId;
        }

        public JsonResult CheckLogin()
        {
            string msg = "";
            if (Session["UserInformationId"] == null)
                msg = "Please Login";
            else
                msg = "";

            return Json(new { message = msg }, JsonRequestBehavior.AllowGet);
        }
    }
}