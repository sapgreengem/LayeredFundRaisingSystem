using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FundRaiserSystemEntity;
using FundRaiserSystemService;
using FundRaiserSystemData;

namespace layeredFundRaiserSystem.Controllers
{
    public class SettingsController : BaseAdminController
    {
        // GET: Settings
        public ActionResult Index()
        {
            ISettingService service = ServiceFactory.GetSettingService();
            return View(service.Get(1));
        }

        [HttpPost]
        public ActionResult Index(FormCollection coll)
        {
            ISettingService service = ServiceFactory.GetSettingService();
            Setting settings = service.Get(1);

            if (!String.IsNullOrWhiteSpace(coll["ServiceCharge"]) && !String.IsNullOrWhiteSpace(coll["RefundCharge"])
                && !String.IsNullOrWhiteSpace(coll["ContactNo"]) && !String.IsNullOrWhiteSpace(coll["Address"])
                && !String.IsNullOrWhiteSpace(coll["AccNo"]))
            {
                settings.ServiceCharge = Convert.ToDouble(coll["ServiceCharge"]);
                settings.RefundCharge = Convert.ToDouble(coll["RefundCharge"]);
                settings.SystemContactNo = coll["ContactNo"].ToString();
                settings.SystemAddress = coll["Address"].ToString();
                settings.SystemBankAccount = coll["AccNo"].ToString();
                service.Update(settings);
                return Redirect("~/Settings");
            }
            else
            {
                ViewBag.ErrorMessage = "No field can be empty";
                return View(service.Get(1));
            }
            

        }
    }
}