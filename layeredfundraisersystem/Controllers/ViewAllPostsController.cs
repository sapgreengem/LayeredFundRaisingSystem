using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FundRaiserSystemService;
using FundRaiserSystemEntity;
using FundRaiserSystemData;
using layeredFundRaiserSystem.Models;

namespace layeredFundRaiserSystem.Controllers
{
    public class ViewAllPostsController : Controller
    {
        // GET: ViewAllPosts
        public ActionResult Index()
        {
            ViewBag.Categories = this.getCategory();

            IFundRequestPostService service = ServiceFactory.GetFundRequestPostService();
            ViewBag.Posts = service.GetAll().Where(a=> a.PostStatus == "Active");
            if (Session["Login"] != null)
            {
                ShowUserName name = new ShowUserName();
                ViewBag.LoginName = name.UserName(Convert.ToInt32(Session["UserInformationId"]));
            }
            return View();
        }
        [HttpPost]
        public ActionResult Index(FormCollection cat, FormCollection search)
        {
            ViewBag.Categories = this.getCategory();
            if (search["searchName"] == null && cat["Category"] != null)
            {
                ViewBag.Posts = this.GetPostsById(Convert.ToInt32(cat["Category"]));
            }
            else
            {
                ViewBag.Posts = this.GetPostsByTitle(search["searchName"]);
            }
            return View();
        }
        public IEnumerable<FundRequestPost> GetPostsById(int Category)
        {
            IFundRequestPostService service = ServiceFactory.GetFundRequestPostService();
            IEnumerable<FundRequestPost> post = service.GetAll().Where(b => b.CategoryId == Category).Where(a => a.PostStatus == "Active");
            return post;
        }
        public IEnumerable<FundRequestPost> GetPostsByTitle(string title)
        {
            IFundRequestPostService service = ServiceFactory.GetFundRequestPostService();
            IEnumerable<FundRequestPost> post = service.GetAll().Where(b => b.PostTitle.Contains(title)).Where(a => a.PostStatus == "Active");
            return post;
        }
        public IEnumerable<PostingCategory> getCategory()
        {
            IPostingCategoryService catService = ServiceFactory.GetPostingCategoryService();
            IEnumerable<PostingCategory> loadCategories = catService.GetAll();
            return loadCategories;
        }
    }
}