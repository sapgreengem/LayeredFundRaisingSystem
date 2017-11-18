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

                int countRow = service.GetAll().Where(email => email.Email == collection["email"].ToString())
                                        .Where(pass => pass.Password == collection["password"].ToString())
                                        .Where(status=> status.Status != "Pending")
                                        .Count();
                if (countRow == 1)
                {
                    IEnumerable<UserLogin> user = service.GetAll().Where(email => email.Email == collection["email"].ToString())
                                                                  .Where(pass => pass.Password == collection["password"].ToString())
                                                                  .Where(status => status.Status != "Pending");
                    foreach (var item in user)
                    {
                        Session["Login"] = item.UserId;
                    }
                    if (this.getUserInfoId() > 0)
                    {
                        Session["UserInformationId"] = this.getUserInfoId();
                        countRow = 0;
                    }
                    else
                    {
                        countRow = 0;
                        return RedirectToAction("Index", "FillAllInfo");
                    }
                    countRow = 0;

                    if (Session["DonateNowPostID"] != null)
                        return RedirectToAction("Index","DonateOnPost",Convert.ToInt32(Session["DonateNowPostID"]));

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

        public int getUserInfoId()
        {
            int userInfoId = 0;
            IUserInformationService userInfoService = ServiceFactory.GetUserInformationService();
            IEnumerable<UserInformation> user = userInfoService.GetAll().Where(a => a.UserId == Convert.ToInt32(Session["Login"]));
            foreach (var item in user)
            {
                userInfoId = item.UserInformationId;
            }
            return userInfoId;
        }
    }
}