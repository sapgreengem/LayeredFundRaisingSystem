using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FundRaiserSystemService;
using FundRaiserSystemEntity;
//
using layeredFundRaiserSystem.Models;

namespace layeredFundRaiserSystem.Controllers
{
    public class ViewAllPostsController : Controller
    {
        // GET: ViewAllPosts
        public ActionResult Index()
        {
            if (Session["Login"] == null)
            {
                Session["StoreURL"] = "/ViewAllPosts/Index"; 
            }

            if (Session["AdminLogin"] != null)
            {
                Response.Redirect("/AdminHome");
            }
            if (Session["Login"] != null && Session["UserInformationId"] == null)
            {
                Response.Redirect("/FillAllInfo/Index");
            }
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
            ViewBag.Categories = this.getCategory();

            #region SearchByCategory 

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
                    IFundRequestPostService service = ServiceFactory.GetFundRequestPostService();
                    IEnumerable<FundRequestPost> post = service.GetAll().Where(a => a.PostStatus == "Active");

                    ViewBag.Categories = categories;
                    ViewBag.Posts = post;
                }
            }
            #endregion SearchByCategory

            #region SearchByText
            else if (search["searchName"] != null && cat["Category"] == null)
            {
                ViewBag.Categories = this.getCategory();
                var res = this.GetPostsByTitle(search["searchName"].ToString()).ToList();

                if (res == null)
                    ViewBag.PostsError = "No Matching Result Found";
                else
                    ViewBag.Posts = res;
            }
            //else if (search["searchName"] != null)
            //{
            //    int categoryID = Convert.ToInt32(cat["Category"]);

            //    var res = this.GetPostsByTitle(search["searchName"].ToString(), categoryID).ToList();

            //    if (res == null)
            //        ViewBag.PostsError = "No Matching Result Found";
            //    else
            //        ViewBag.Posts = res;
            //}
            #endregion SearchByText
            //else
            //{
            //    ViewBag.Categories = this.getCategory();
            //    ViewBag.Posts = this.GetPostsByTitle(search["searchName"]).ToList();
            //}

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
        public IEnumerable<FundRequestPost> GetPostsByTitle(string title/*, int CatID*/)
        {
            IFundRequestPostService service = ServiceFactory.GetFundRequestPostService();
            IEnumerable<FundRequestPost> post = service.GetAll().Where(b => b.PostTitle.Contains(title))
                /*.Where(a=> a.CategoryId == CatID)*/
                .Where(a => a.PostStatus == "Active");
            return post;
        }
        public List<PostingCategory> getCategory()
        {
            IPostingCategoryService catService = ServiceFactory.GetPostingCategoryService();
            List<PostingCategory> loadCategories = catService.GetAll().ToList();
            return loadCategories;
        }

        [HttpGet]
        public JsonResult GetPostBySearchName(string id)
        {

            //IFundRequestPostService service = service.GetAll()
            //IEnumerable<FundRequestPost> FundRequestPost = FundRequestPost;
            var msg = "";
            return Json(new { message = msg }, JsonRequestBehavior.AllowGet);
        }
    }
}