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
    public class ManageBankInformationController : BaseAdminController
    {
        IBankInformationService service = ServiceFactory.GetBankInformationService();

        [HttpGet]
        // GET: ManageBankInformation
        public ActionResult Index()
        {
            CountPendings count = new CountPendings();
            ViewBag.Posts = count.CountPendingPost();
            ViewBag.Withdraws = count.CountPendingWithdraws();
            ViewBag.BankList = service.GetAll();
            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection coll)
        {
            BankInformation info = new BankInformation();
            info.BankName = coll["BankName"].ToString();
            info.BranchName = coll["BranchName"].ToString();
            service.Insert(info);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id=0)
        {
            if (id == 0) return View("Error");
            else
            {
                CountPendings count = new CountPendings();
                ViewBag.Posts = count.CountPendingPost();
                ViewBag.Withdraws = count.CountPendingWithdraws();
                BankInformation info = service.Get(id);
                return View(info);
            }
        }
        [HttpPost]
        public ActionResult Edit(FormCollection form)
        {
            BankInformation info = service.Get(Convert.ToInt32( form["id"]));
            info.BankName = form["BankName"];
            info.BranchName = form["BranchName"];
            service.Update(info);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            service.Delete(id);
            return RedirectToAction("Index");
        }
    }
}