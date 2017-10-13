using FundRaiserSystemData;
using FundRaiserSystemEntity;
using FundRaiserSystemService;
using layeredFundRaiserSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace layeredFundRaiserSystem.Controllers
{
    public class ManageCategoryController : BaseAdminController
    {
        IPostingCategoryService service = ServiceFactory.GetPostingCategoryService();

        // GET: ManageCategory
        [HttpGet]
        public ActionResult Index()
        {
            CountPendings count = new CountPendings();
            ViewBag.Posts = count.CountPendingPost();
            ViewBag.Withdraws = count.CountPendingWithdraws();

            ShowUserName name = new ShowUserName();
            ViewBag.adminName = name.AdminName(Convert.ToInt32(Session["AdminLogin"]));

            return View(service.GetAll());
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            CountPendings count = new CountPendings();
            ViewBag.Posts = count.CountPendingPost();
            ViewBag.Withdraws = count.CountPendingWithdraws();

            ShowUserName name = new ShowUserName();
            ViewBag.adminName = name.AdminName(Convert.ToInt32(Session["AdminLogin"]));

            PostingCategory category = new PostingCategory();
            category.CategoryName = form["CategoryName"].ToString();
            service.Insert(category);
            
            return View(service.GetAll());
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            CountPendings count = new CountPendings();
            ViewBag.Posts = count.CountPendingPost();
            ViewBag.Withdraws = count.CountPendingWithdraws();

            ShowUserName name = new ShowUserName();
            ViewBag.adminName = name.AdminName(Convert.ToInt32(Session["AdminLogin"]));

            PostingCategory category = service.Get(id);
            return View(category);
        }
        [HttpPost]
        public ActionResult Edit(FormCollection form)
        {
            CountPendings count = new CountPendings();
            ViewBag.Posts = count.CountPendingPost();
            ViewBag.Withdraws = count.CountPendingWithdraws();

            ShowUserName name = new ShowUserName();
            ViewBag.adminName = name.AdminName(Convert.ToInt32(Session["AdminLogin"]));

            PostingCategory category = service.Get(Convert.ToInt32(form["id"]));
            category.CategoryName = form["CategoryName"].ToString();
            service.Update(category);
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