﻿using FundRaiserSystemEntity;
using FundRaiserSystemService;
using layeredFundRaiserSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace layeredFundRaiserSystem.Controllers
{
    public class HomeController : Controller
    {
        
        // GET: Home
        public ActionResult Index()
        {
            this.ChangePostStatus();
            this.topRated();
            this.trending();
            this.recomended();

            if (Session["AdminLogin"] != null)
            {
                Response.Redirect("/AdminHome");
            }
            if (Session["Login"] != null && Session["UserInformationId"] == null)
            {
                Response.Redirect("/FillAllInfo/Index");
            }
            if (Session["Login"] == null)
            {
                Session["StoreURL"] = "/Home/Index";
            }
            if (Session["Login"] != null)
            {
                ShowUserName name = new ShowUserName();
                ViewBag.LoginName = name.UserName(Convert.ToInt32(Session["UserInformationId"]));
            }

            return View();
        }

        public ActionResult AboutUs()
        {
            if (Session["Login"] != null)
            {
                ShowUserName name = new ShowUserName();
                ViewBag.LoginName = name.UserName(Convert.ToInt32(Session["UserInformationId"]));
            }
            return View();
        }

        public ActionResult FAQ()
        {
            if (Session["Login"] != null)
            {
                ShowUserName name = new ShowUserName();
                ViewBag.LoginName = name.UserName(Convert.ToInt32(Session["UserInformationId"]));
            }
            ISettingService settingService = ServiceFactory.GetSettingService();          
            return View(settingService.Get(1));
        }

        public ActionResult ContactUs()
        {
            if (Session["Login"] != null)
            {
                ShowUserName name = new ShowUserName();
                ViewBag.LoginName = name.UserName(Convert.ToInt32(Session["UserInformationId"]));
            }
            ISettingService setting = ServiceFactory.GetSettingService();
            return View(setting.Get(1));
        }

        public void topRated()
        {
            IFundRequestPostService service = ServiceFactory.GetFundRequestPostService();
            IEnumerable<FundRequestPost> posts = service.GetAll()
                .Where(a => a.PostStatus == "Active")
                .OrderByDescending(a => a.AverageRating);

            List<FundRequestPost> top = new List<FundRequestPost>();
            List<FundRequestPost> nextTop = new List<FundRequestPost>();
            int count = 0;
            foreach (var item in posts)
            {
                if (count < 4)
                {
                    FundRequestPost fundRequestPost = new FundRequestPost();
                    fundRequestPost.CollectedAmount = item.CollectedAmount;
                    fundRequestPost.PostId = item.PostId;
                    fundRequestPost.PostImage = item.PostImage;
                    fundRequestPost.PostTitle = item.PostTitle;
                    fundRequestPost.RemainingAmount = item.RemainingAmount;
                    fundRequestPost.RequiredAmount = item.RequiredAmount;
                    fundRequestPost.AverageRating = item.AverageRating;
                    top.Add(fundRequestPost);
                }
                if (count >= 4 && count <= 7)
                {
                    FundRequestPost fundRequestPost = new FundRequestPost();
                    fundRequestPost.CollectedAmount = item.CollectedAmount;
                    fundRequestPost.PostId = item.PostId;
                    fundRequestPost.PostImage = item.PostImage;
                    fundRequestPost.PostTitle = item.PostTitle;
                    fundRequestPost.RemainingAmount = item.RemainingAmount;
                    fundRequestPost.RequiredAmount = item.RequiredAmount;
                    fundRequestPost.AverageRating = item.AverageRating;
                    nextTop.Add(fundRequestPost);
                }
                count++;
            }
            ViewBag.Top = top.ToList();
            ViewBag.nextTop = nextTop.ToList();
        }

        public void trending()
        {
            IFundRequestPostService service = ServiceFactory.GetFundRequestPostService();
            IEnumerable<FundRequestPost> posts = service.GetAll()
                .Where(a => a.PostStatus == "Active")
                .OrderByDescending(a => a.StartDate)
                .OrderByDescending(a => a.ClickCounter)
                .OrderByDescending(a => a.CollectedAmount);

            List<FundRequestPost> trending = new List<FundRequestPost>();
            List<FundRequestPost> nextTrending = new List<FundRequestPost>();
            int count = 0;
            foreach (var item in posts)
            {
                if (count < 4)
                {
                    FundRequestPost fundRequestPost = new FundRequestPost();
                    fundRequestPost.CollectedAmount = item.CollectedAmount;
                    fundRequestPost.PostId = item.PostId;
                    fundRequestPost.PostImage = item.PostImage;
                    fundRequestPost.PostTitle = item.PostTitle;
                    fundRequestPost.RemainingAmount = item.RemainingAmount;
                    fundRequestPost.RequiredAmount = item.RequiredAmount;
                    fundRequestPost.AverageRating = item.AverageRating;
                    trending.Add(fundRequestPost);
                }
                if (count >= 4 && count <= 7)
                {
                    FundRequestPost fundRequestPost = new FundRequestPost();
                    fundRequestPost.CollectedAmount = item.CollectedAmount;
                    fundRequestPost.PostId = item.PostId;
                    fundRequestPost.PostImage = item.PostImage;
                    fundRequestPost.PostTitle = item.PostTitle;
                    fundRequestPost.RemainingAmount = item.RemainingAmount;
                    fundRequestPost.RequiredAmount = item.RequiredAmount;
                    fundRequestPost.AverageRating = item.AverageRating;
                    nextTrending.Add(fundRequestPost);
                }
                count++;
            }
            ViewBag.trending = trending.ToList();
            ViewBag.nextTrending = nextTrending.ToList();
        }

        public void recomended()
        {
            IFundRequestPostService service = ServiceFactory.GetFundRequestPostService();
            IEnumerable<FundRequestPost> posts = service.GetAll()
                .Where(a => a.PostStatus == "Active")
                .OrderBy(a => (a.CollectedAmount / a.RequiredAmount) * 100);

            List<FundRequestPost> recomended = new List<FundRequestPost>();
            List<FundRequestPost> nextRecomended = new List<FundRequestPost>();
            int count = 0;
            foreach (var item in posts)
            {
                if (count < 4)
                {
                    FundRequestPost fundRequestPost = new FundRequestPost();
                    fundRequestPost.CollectedAmount = item.CollectedAmount;
                    fundRequestPost.PostId = item.PostId;
                    fundRequestPost.PostImage = item.PostImage;
                    fundRequestPost.PostTitle = item.PostTitle;
                    fundRequestPost.RemainingAmount = item.RemainingAmount;
                    fundRequestPost.RequiredAmount = item.RequiredAmount;
                    fundRequestPost.AverageRating = item.AverageRating;
                    recomended.Add(fundRequestPost);
                }
                if (count >= 4 && count <= 7)
                {
                    FundRequestPost fundRequestPost = new FundRequestPost();
                    fundRequestPost.CollectedAmount = item.CollectedAmount;
                    fundRequestPost.PostId = item.PostId;
                    fundRequestPost.PostImage = item.PostImage;
                    fundRequestPost.PostTitle = item.PostTitle;
                    fundRequestPost.RemainingAmount = item.RemainingAmount;
                    fundRequestPost.RequiredAmount = item.RequiredAmount;
                    fundRequestPost.AverageRating = item.AverageRating;
                    nextRecomended.Add(fundRequestPost);
                }
                count++;
            }
            ViewBag.recomended = recomended.ToList();
            ViewBag.nextRecomended = nextRecomended.ToList();
        }

        public void ChangePostStatus()
        {
            IFundRequestPostService service = ServiceFactory.GetFundRequestPostService();
            IEnumerable<FundRequestPost> posts = service.GetAll()
                                                .Where(a => Convert.ToDouble((a.EndDate - DateTime.Now).TotalDays) < 0)
                                                .Where(a => a.PostStatus == "Active");

            if (posts != null)
            {
                foreach (var item in posts)
                {
                    FundRequestPost load = new FundRequestPost();
                    load.PostId = item.PostId;
                    load.PostTitle = item.PostTitle;
                    load.PostDetails = item.PostDetails;
                    load.RequiredAmount = item.RequiredAmount;
                    load.StartDate = item.StartDate;
                    load.EndDate = item.EndDate;
                    load.CollectedAmount = item.CollectedAmount;
                    load.RemainingAmount = item.RemainingAmount;
                    load.PostImage = item.PostImage;
                    load.PostStatus = "Expired";
                    load.UserInformationId = item.UserInformationId;
                    load.CategoryId = item.CategoryId;
                    load.RefundId = item.RefundId;
                    load.ClickCounter = item.ClickCounter;
                    service.Update(load);
                }
            }
        }
    }
}