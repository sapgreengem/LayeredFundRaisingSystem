﻿using FundRaiserSystemData;
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
    public class AdminHomeController : BaseAdminController
    {

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult TopDonor()
        {
            IDonationOnPostService donationService = ServiceFactory.GetDonationOnPostService();
            IUserInformationService infoService = ServiceFactory.GetUserInformationService();
            IEnumerable<UserInformation> userInformation = infoService.GetAll();

            List<JoinDonationOnPosts_UserInformations_FundReqPosts> joinData = new List<JoinDonationOnPosts_UserInformations_FundReqPosts>();
            foreach (var item in donationService.GetAll(true))
            {
                JoinDonationOnPosts_UserInformations_FundReqPosts addData = new JoinDonationOnPosts_UserInformations_FundReqPosts()
                {
                    FirstName = item.UserInformation.FirstName,
                    DonationAmount = item.DonationAmount,
                    UserInformationId = item.UserInformationId
                };
                joinData.Add(addData);
            }

            //.GroupBy(item => item.GroupKey)
            //.Select(group => group.Sum(item => item.Aggregate));

            ViewBag.TopDonarListNew = joinData.GroupBy(a => a.UserInformationId)
                .Select(a => new { Name = a.FirstOrDefault().FirstName, TotalDonationAmount = a.Sum(b => b.DonationAmount) }).OrderByDescending(a => a.TotalDonationAmount).ToList();

            ViewBag.TopDonarList = userInformation;
            List<double> val = new List<double>();

            foreach (var item in userInformation)
            {
                val.Add(donationService.GetAll().Where(a => a.UserInformationId == item.UserInformationId).Sum(a => a.DonationAmount));
            }
            ViewBag.Sum = val;
            return View();
        }
        [HttpGet]
        public ActionResult TopDonatedPosts()
        {
            ViewBag.TopDonatedPosts = this.LoadPosts().OrderByDescending(a => a.CollectedAmount);
            return View();
        }
        public ActionResult CollectedAmmountOfMonth()
        {
            return View();
        }
        public ActionResult MostViewedPosts()
        {
            ViewBag.MostViewedPosts = this.LoadPosts().OrderByDescending(a => a.ClickCounter);

            return View();
        }

        public IEnumerable<JoinFundRequestPost_Category_UserInfo> LoadPosts()
        {
            IFundRequestPostService postService = ServiceFactory.GetFundRequestPostService();
            IEnumerable<FundRequestPost> post = postService.GetAll(true, true, false); // Include User & Ctegory
            List<JoinFundRequestPost_Category_UserInfo> joinData = new List<JoinFundRequestPost_Category_UserInfo>();
            foreach (FundRequestPost item in post)
            {
                JoinFundRequestPost_Category_UserInfo load = new JoinFundRequestPost_Category_UserInfo()
                {
                    PostId = item.PostId,
                    PostTitle = item.PostTitle,
                    RequiredAmount = item.RequiredAmount,
                    StartDate = item.StartDate,
                    EndDate = item.EndDate,
                    CategoryName = item.PostingCategory.CategoryName,
                    FirstName = item.UserInformation.FirstName,
                    LastName = item.UserInformation.LastName,
                    UserId = item.UserInformation.UserId,
                    UserInformationId = item.UserInformationId,
                    ClickCounter = item.ClickCounter,
                    CollectedAmount = item.CollectedAmount

                };
                joinData.Add(load);
            }
            return joinData.ToList();
        }
    }
}