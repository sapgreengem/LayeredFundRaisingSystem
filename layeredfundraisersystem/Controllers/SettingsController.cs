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
            settings.ServiceCharge = Convert.ToDouble(coll["ServiceCharge"]);
            settings.RefundCharge = Convert.ToDouble(coll["RefundCharge"]);
            service.Update(settings);
            return Redirect("~/Settings");
        }
    }
}