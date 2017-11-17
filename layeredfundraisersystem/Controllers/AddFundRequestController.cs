﻿using FundRaiserSystemData;
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
            //IPostingCategoryService postingService = ServiceFactory.GetPostingCategoryService();

            //List<SelectListItem> categoryList = new List<SelectListItem>();
            //foreach (PostingCategory category in postingService.GetAll())
            //{
            //    SelectListItem item = new SelectListItem()
            //    {
            //        Text = category.CategoryName,
            //        Value = category.CategoryId.ToString()
            //    };
            //    categoryList.Add(item);
            //}
            //ViewBag.Categories = categoryList;

            ViewBag.Categories = this.Catagories();
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection form, HttpPostedFileBase file, HttpPostedFileBase proof)
        {
            
            if (!String.IsNullOrWhiteSpace(form["PostTitle"]) && !String.IsNullOrWhiteSpace(form["PostDetails"])
                && !String.IsNullOrWhiteSpace(form["RequiredAmount"]) && !String.IsNullOrWhiteSpace(form["EndDateInDays"])
                && Convert.ToInt32(form["Category"]) != 0)
            {
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
                    service.Insert(post);
                }
                else
                {
                    ViewBag.ErrorMessage = "Please give an Image of your post";
                }
                //----------------------------------------------------------------------------

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
                    proofService.Insert(postProofs);
                }
                else
                {
                    ViewBag.ErrorMessage = "Please give an Image for proof";
                }
                ViewBag.PostConfirmation = "Post is pending for Approval";
            }
            else
            {
                ViewBag.ErrorMessage = "Fields cannot be empty. Please fill all informations";
            }

            ViewBag.Categories = this.Catagories();

            return View();
        }
        public IEnumerable<SelectListItem> Catagories()
        {
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
            return categoryList;
        }
    }
}