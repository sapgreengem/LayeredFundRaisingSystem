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

            CountPendings count = new CountPendings();
            ViewBag.Posts = count.CountPendingPost();
            ViewBag.Withdraws = count.CountPendingWithdraws();

            ShowUserName name = new ShowUserName();
            ViewBag.adminName = name.AdminName(Convert.ToInt32(Session["AdminLogin"]));

            return View(adminService.Get(Convert.ToInt32(Session["AdminLogin"])));
        }

        [HttpGet]
        public ActionResult Edit()
        {

            CountPendings count = new CountPendings();
            ViewBag.Posts = count.CountPendingPost();
            ViewBag.Withdraws = count.CountPendingWithdraws();

            ShowUserName name = new ShowUserName();
            ViewBag.adminName = name.AdminName(Convert.ToInt32(Session["AdminLogin"]));

            IAdministrationService adminService = ServiceFactory.GetAdministrationService();
            return View(adminService.Get(Convert.ToInt32(Session["AdminLogin"])));
        }

        [HttpPost]
        public ActionResult Edit(FormCollection collection)
        {
            CountPendings count = new CountPendings();
            ViewBag.Posts = count.CountPendingPost();
            ViewBag.Withdraws = count.CountPendingWithdraws();

            ShowUserName name = new ShowUserName();
            ViewBag.adminName = name.AdminName(Convert.ToInt32(Session["AdminLogin"]));

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
    }
}