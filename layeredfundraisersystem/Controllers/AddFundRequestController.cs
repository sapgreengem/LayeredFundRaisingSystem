using FundRaiserSystemData;
using FundRaiserSystemEntity;
using FundRaiserSystemService;
using layeredFundRaiserSystem.Models;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace layeredFundRaiserSystem.Controllers
{
    public class AddFundRequestController : BaseUserController
    {
        // GET: AddFundRequest
        public ActionResult Index()
        {
            
            IPostingCategoryService service = ServiceFactory.GetPostingCategoryService();

            List<SelectListItem> categoryList = new List<SelectListItem>();
            foreach (PostingCategory category in service.GetAll())
            {
                SelectListItem item = new SelectListItem()
                {
                    Text = category.CategoryName,
                    Value = category.CategoryId.ToString()
                };
                categoryList.Add(item);
            }
            ViewBag.Categories = categoryList;

            ShowUserName name = new ShowUserName();
            ViewBag.LoginName = name.UserName(Convert.ToInt32(Session["UserInformationId"]));

            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection form, HttpPostedFileBase file, HttpPostedFileBase proof)
        {
            //int UserInformationId = 0;
            //IUserInformationService userInfoService = ServiceFactory.GetUserInformationService();
            //IEnumerable<UserInformation> informations = userInfoService.GetAll().Where(login => login.UserId == Convert.ToInt32(Session["Login"]));
            //foreach (var item in informations)
            //{
            //    UserInformationId = item.UserInformationId;
            //}


            IFundRequestPostService service = ServiceFactory.GetFundRequestPostService();
            FundRequestPost post = new FundRequestPost();
            // Verify that the user selected a file
            if (file != null && file.ContentLength > 0)
            {
                post.PostTitle = form["PostTitle"].ToString();
                post.PostDetails = form["PostDetails"].ToString();
                post.RequiredAmount = Convert.ToDouble(form["RequiredAmount"]);
                post.StartDate = DateTime.Now;
                post.EndDate = DateTime.Now.AddDays(Convert.ToDouble(form["EndDateInDays"]));
                post.CollectedAmount = 0;
                post.RemainingAmount = 0;
                post.PostStatus = "Pending";
                post.UserInformationId = Convert.ToInt32(Session["UserInformationId"]);
                post.CategoryId = Convert.ToInt32(form["Category"]);
                post.ClickCounter = 1;

                // extract only the filename
                var fileName = Path.GetFileName(file.FileName);
                var NewFileName = DateTime.Now.ToFileTime()+" "+ fileName;
                post.PostImage = NewFileName;
                // store the file inside ~/App_Data/uploads folder
                var path = Path.Combine(Server.MapPath("~/PostImages"), NewFileName);
                file.SaveAs(path);
            }
            service.Insert(post);

            IPostProofService proofService = ServiceFactory.GetPostProofService();
            PostProof postProofs = new PostProof();
            // Verify that the user selected a file
            if (proof != null && proof.ContentLength > 0)
            {
                postProofs.PostId = post.PostId;
                // extract only the filename
                var fileName = Path.GetFileName(proof.FileName);
                var NewFileName = DateTime.Now.ToFileTime() + " " + fileName;
                postProofs.PictureForProof = NewFileName;
                // store the file inside ~/App_Data/uploads folder
                var path = Path.Combine(Server.MapPath("~/PostProofImages"), NewFileName);
                proof.SaveAs(path);
            }
            proofService.Insert(postProofs);
            ViewBag.PostConfirmation = "Post is pending for Approval";

            IPostingCategoryService postingService = ServiceFactory.GetPostingCategoryService();

            List<SelectListItem> categoryList = new List<SelectListItem>();
            foreach (PostingCategory category in postingService.GetAll())
            {
                SelectListItem item = new SelectListItem()
                {
                    Text = category.CategoryName,
                    Value = category.CategoryId.ToString()
                };
                categoryList.Add(item);
            }
            ViewBag.Categories = categoryList;

            return View();
        }
    }
}