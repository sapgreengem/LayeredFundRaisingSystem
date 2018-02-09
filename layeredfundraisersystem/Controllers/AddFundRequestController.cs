using FundRaiserSystemEntity;
using FundRaiserSystemService;
using layeredFundRaiserSystem.Models;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Security.Application;

namespace layeredFundRaiserSystem.Controllers
{
    public class AddFundRequestController : Controller
    {
        // GET: AddFundRequest
        public ActionResult Index()
        {
            if (Session["Login"] == null)
            {
                Session["StoreURL"] = "/AddFundRequest/Index";
                Response.Redirect("/Login", false);
            }
            if (Session["Login"] != null && Session["UserInformationId"] == null)
            {
                Response.Redirect("/FillAllInfo/Index");
            }
            if (Session["Login"] != null && Session["UserInformationId"] != null)
            {
                ShowUserName name = new ShowUserName();

                string LoginUserName= name.UserName(Convert.ToInt32(Session["UserInformationId"]));
                ViewBag.LoginName = name.UserName(Convert.ToInt32(Session["UserInformationId"]));

                IUserInformationService service = ServiceFactory.GetUserInformationService();
                UserInformation userInformation = service.Get(Convert.ToInt32(Session["UserInformationId"]));
                if (userInformation.Country != "Bangladesh")
                {
                    ViewBag.Message = "Sorry " + LoginUserName + " You are not allowed to Request for fund";
                }
            }

            ViewBag.Categories = this.Catagories();
            

            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Index(FormCollection form, HttpPostedFileBase file, HttpPostedFileBase proof)
        {
            var PostTitle = Sanitizer.GetSafeHtmlFragment((form["PostTitle"]).ToString());
            var PostDetails = Sanitizer.GetSafeHtmlFragment((form["PostDetails"]).ToString());

            if (!String.IsNullOrWhiteSpace(PostTitle) && !String.IsNullOrWhiteSpace(PostDetails)
                && !String.IsNullOrWhiteSpace(form["RequiredAmount"]) && !String.IsNullOrWhiteSpace(form["EndDateInDays"])
                && Convert.ToInt32(form["Category"]) != 0 && file != null && proof != null)
            {
                IFundRequestPostService service = ServiceFactory.GetFundRequestPostService();
                FundRequestPost post = new FundRequestPost();

                IPostProofService proofService = ServiceFactory.GetPostProofService();
                PostProof postProofs = new PostProof();

                post.PostTitle = PostTitle;
                post.PostDetails = PostDetails;
                post.RequiredAmount = Convert.ToDouble(form["RequiredAmount"]);
                post.StartDate = DateTime.Now;
                post.EndDate = DateTime.Now.AddDays(Convert.ToDouble(form["EndDateInDays"]));
                post.CollectedAmount = 0;
                post.RemainingAmount = 0;
                post.PostStatus = "Pending";
                post.UserInformationId = Convert.ToInt32(Session["UserInformationId"]);
                post.CategoryId = Convert.ToInt32(form["Category"]);
                post.ClickCounter = 0;
                post.AverageRating = 0;
            
                var fileName = Path.GetFileName(file.FileName);
                var fileExt = Path.GetExtension(fileName);

                var ProofFileName = Path.GetFileName(proof.FileName);
                var fileExt1 = Path.GetExtension(fileName);

                if (file.ContentLength > 5242880 || proof.ContentLength > 5242880)
                {
                    ViewBag.ErrorMessage = "File size must be less than 5MB";
                    ViewBag.Categories = this.Catagories();
                    ViewBag.PostTitle = PostTitle;
                    ViewBag.Details = PostDetails;
                    return View();
                }
                else
                {
                    if (fileExt != ".jpeg" || fileExt != ".JPEG" ||
                    fileExt != ".jpg" || fileExt != ".JPG" ||
                    fileExt != ".png" || fileExt != ".PNG")
                    {
                        ViewBag.ErrorMessage = "Invalid File Type";
                        ViewBag.Categories = this.Catagories();
                        ViewBag.PostTitle = PostTitle;
                        ViewBag.Details = PostDetails;
                        return View();
                    }
                    else if (fileExt1 != ".jpeg" || fileExt1 != ".JPEG" ||
                    fileExt1 != ".jpg" || fileExt1 != ".JPG" ||
                    fileExt1 != ".png" || fileExt1 != ".PNG" || fileExt1 != ".xsl" || fileExt1 != ".xslx" || fileExt1 != ".docx" || fileExt1 != ".doc" || fileExt1 != ".pdf")
                    {
                        ViewBag.ErrorMessage = "Invalid File Type";
                        ViewBag.Categories = this.Catagories();
                        ViewBag.PostTitle = PostTitle;
                        ViewBag.Details = PostDetails;
                        return View();
                    }
                }
                    
                var NewFileName = DateTime.Now.ToFileTime() + " " + fileName;             
                var path = Path.Combine(Server.MapPath("~/PostImages"), NewFileName);
                post.PostImage = NewFileName;
                file.SaveAs(path);
                service.Insert(post);

                var ProofNewFileName = DateTime.Now.ToFileTime() + " " + ProofFileName;
                var path1 = Path.Combine(Server.MapPath("~/PostProofImages"), ProofFileName);
                postProofs.PictureForProof = ProofNewFileName;               
                postProofs.PostId = post.PostId;
                proof.SaveAs(path1);
                proofService.Insert(postProofs);

                ViewBag.PostConfirmation = "Post is pending for Approval";
            }
            else
            {
                ViewBag.ErrorMessage = "Fields cannot be empty or fill all authentic information";
            }

            if (Session["UserInformationId"] != null)
            {
                ShowUserName name = new ShowUserName();
                ViewBag.LoginName = name.UserName(Convert.ToInt32(Session["UserInformationId"]));
            }

            ViewBag.Categories = this.Catagories();
            ViewBag.PostTitle = PostTitle;
            ViewBag.Details = PostDetails;
            return View();
        }
        public List<SelectListItem> Catagories()
        {
            IPostingCategoryService postingService = ServiceFactory.GetPostingCategoryService();

            List<SelectListItem> categoryList = new List<SelectListItem>();
            foreach (PostingCategory category in postingService.GetAll())
            {
                if(category.CategoryName != "All")
                {
                    SelectListItem item = new SelectListItem()
                    {
                        Text = category.CategoryName,
                        Value = category.CategoryId.ToString()
                    };
                    categoryList.Add(item);
                }      
            }

            return categoryList;
        }
    }
}