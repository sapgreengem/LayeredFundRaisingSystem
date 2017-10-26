using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FundRaiserSystemService;
using FundRaiserSystemData;
using FundRaiserSystemEntity;
using layeredFundRaiserSystem.Models;

namespace layeredFundRaiserSystem.Controllers
{
    public class AdminProfileController : BaseAdminController
    {
        // GET: AdminProfile
        public ActionResult Index()
        {
            IAdministrationService adminService = ServiceFactory.GetAdministrationService();
            //adminService.Get(Convert.ToInt32(Session["AdminLogin"]));

            return View(adminService.Get(Convert.ToInt32(Session["AdminLogin"])));
        }

        [HttpGet]
        public ActionResult Edit()
        {
            IAdministrationService adminService = ServiceFactory.GetAdministrationService();
            return View(adminService.Get(Convert.ToInt32(Session["AdminLogin"])));
        }

        [HttpPost]
        public ActionResult Edit(FormCollection collection)
        {
            if ( !String.IsNullOrWhiteSpace(collection["FirstName"].ToString()) 
                && !String.IsNullOrWhiteSpace(collection["LastName"].ToString()) 
                && !String.IsNullOrWhiteSpace(collection["Email"].ToString()) 
                && !String.IsNullOrWhiteSpace(collection["Address"].ToString())
                )
            {
                IAdministrationService adminService = ServiceFactory.GetAdministrationService();
                Administration admin = adminService.Get(Convert.ToInt32(Session["AdminLogin"]));

                admin.FirstName = collection["FirstName"].ToString();
                admin.LastName = collection["LastName"].ToString();
                admin.Email = collection["Email"].ToString();
                admin.Address = collection["Address"].ToString();

                adminService.Update(admin);
                return RedirectToAction("Index", "AdminProfile");
            }
            else
            {
                ViewBag.ErrorMessage = "Fields Cannot be empty";
                IAdministrationService adminService = ServiceFactory.GetAdministrationService();
                return View(adminService.Get(Convert.ToInt32(Session["AdminLogin"])));
            }
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            IAdministrationService adminService = ServiceFactory.GetAdministrationService();
            
            return View(adminService.Get(Convert.ToInt32(Session["AdminLogin"])));
        }

        [HttpPost]
        public ActionResult ChangePassword(FormCollection collection)
        {
            IAdministrationService adminService = ServiceFactory.GetAdministrationService();
            Administration admin = adminService.Get(Convert.ToInt32(Session["AdminLogin"]));
            if (!String.IsNullOrWhiteSpace(collection["CurrentPassword"].ToString()) && !String.IsNullOrWhiteSpace(collection["NewPassword"].ToString()) && !String.IsNullOrWhiteSpace(collection["confirmPassword"].ToString()))
            {
                if (admin.Password == collection["CurrentPassword"].ToString())
                {
                    if (collection["NewPassword"].ToString() == collection["confirmPassword"].ToString())
                    {
                        admin.Password = collection["confirmPassword"].ToString();
                        adminService.Update(admin);
                        return Redirect("/Logout");
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "New Password and Confirm Password do not match";
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "Current Password is not correct";
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Please Fill Data";
            }

            return View(adminService.Get(Convert.ToInt32(Session["AdminLogin"])));
        }
    }
}