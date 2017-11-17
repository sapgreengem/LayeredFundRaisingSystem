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
            List<PostingCategory> categories = this.getCategory();
            List<PostingCategory> categories1 = new List<PostingCategory>();

            if (search["searchName"] == null && cat["Category"] != null)
            {
                if (Convert.ToInt32(cat["Category"]) != 0)
                {
                    int categoryID = Convert.ToInt32(cat["Category"]);
                    PostingCategory category = this.getCategory().Where(a => a.CategoryId == categoryID).SingleOrDefault();
                    string categoryName = category.CategoryName.ToString();

                    foreach (var item in categories)
                    {
                        if (item.CategoryName != categoryName)
                        {
                            PostingCategory ostingCategory = new PostingCategory();
                            ostingCategory.CategoryId = item.CategoryId;
                            ostingCategory.CategoryName = item.CategoryName;

                            categories1.Add(ostingCategory);
                        }
                    }

                    PostingCategory postingCategory = new PostingCategory();
                    postingCategory.CategoryId = categoryID;
                    postingCategory.CategoryName = categoryName;

                    categories1.Insert(0, postingCategory);

                    ViewBag.Categories = categories1;
                    ViewBag.Posts = this.GetPostsById(Convert.ToInt32(cat["Category"]));
                }
                else
                {
                    ViewBag.Categories = categories;
                    ViewBag.Posts = this.GetPostsById(Convert.ToInt32(cat["Category"]));
                }
                
            }
            else
            {
                ViewBag.Posts = this.GetPostsByTitle(search["searchName"]);
            }

            if (Session["Login"] != null)
            {
                ShowUserName name = new ShowUserName();
                ViewBag.LoginName = name.UserName(Convert.ToInt32(Session["UserInformationId"]));
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
        public List<PostingCategory> getCategory()
        {
            IPostingCategoryService catService = ServiceFactory.GetPostingCategoryService();
            List<PostingCategory> loadCategories = catService.GetAll().ToList();
            return loadCategories;
        }
    }
}