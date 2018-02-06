using FundRaiserSystemData;
using FundRaiserSystemEntity;
using FundRaiserSystemService;
using layeredFundRaiserSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace layeredFundRaiserSystem.Controllers
{
    public class AdminHomeController : BaseAdminController
    {

        public ActionResult Index()
        {
            IFundRequestPostService service = ServiceFactory.GetFundRequestPostService();
            ViewBag.TotalActivePosts = service.GetAll().Where(a=> a.PostStatus == "Active").Count();

            IUserLoginService userLogin = ServiceFactory.GetUserLoginService();
            ViewBag.TotalActiveUsers = userLogin.GetAll().Where(a => a.Status == "Active").Count();

            ISettingService settings = ServiceFactory.GetSettingService();
            Setting setting = settings.Get(1);
            ViewBag.TotalIncome = setting.TotalIncome.ToString("0.00");
            ViewBag.TotalCollection = setting.CollectedAmount.ToString("0.00");


            return View();
        }
    }
}