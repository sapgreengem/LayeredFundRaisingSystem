using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FundRaiserSystemData;
using FundRaiserSystemEntity;
using FundRaiserSystemService;
using layeredFundRaiserSystem.Models;

namespace layeredFundRaiserSystem.Controllers
{
    public class ManageBankInformationController : BaseAdminController
    {
        IBankInformationService service = ServiceFactory.GetBankInformationService();

        [HttpGet]
        // GET: ManageBankInformation
        public ActionResult Index()
        {
            ViewBag.BankList = service.GetAll();
            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection coll)
        {
            if (!String.IsNullOrWhiteSpace(coll["BankName"]) && !String.IsNullOrWhiteSpace(coll["BranchName"]))
            {
                int existingBankInfoCounter = 0;
                existingBankInfoCounter = service.GetAll().Where(a => a.BankName == coll["BankName"].ToString()).Where(a => a.BranchName == coll["BranchName"].ToString()).Count();
                if (existingBankInfoCounter == 0)
                {
                    BankInformation info = new BankInformation();
                    info.BankName = coll["BankName"].ToString();
                    info.BranchName = coll["BranchName"].ToString();
                    service.Insert(info);
                }
                else
                {
                    ViewBag.ErrorMessage = "Bank already exists";
                }

            }
            else
            {
                ViewBag.ErrorMessage = "Give all informations";
            }
            
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id=0)
        {
            if (id == 0) return View("Error");
            else
            {
                BankInformation info = service.Get(id);
                return View(info);
            }
        }
        [HttpPost]
        public ActionResult Edit(FormCollection form)
        {
            if (!String.IsNullOrWhiteSpace(form["BankName"]) && !String.IsNullOrWhiteSpace(form["BranchName"]))
            {
                BankInformation info = service.Get(Convert.ToInt32(form["id"]));
                info.BankName = form["BankName"].ToString();
                info.BranchName = form["BranchName"].ToString();
                service.Update(info);
            }
            else
            {
                ViewBag.ErrorMessage = "Give all informations";
            }
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            service.Delete(id);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public JsonResult CheckExistingBank(string id, string id1)
        {
            var existingBankInfoCounter = service.GetAll().Where(a => a.BankName == id).Where(a => a.BranchName == id1).Count();
            var msg = "";

            if (existingBankInfoCounter != 0) msg = "Bank Already Exists";
            else msg = "";

            return Json(new { message = msg }, JsonRequestBehavior.AllowGet);
        }
    }
}