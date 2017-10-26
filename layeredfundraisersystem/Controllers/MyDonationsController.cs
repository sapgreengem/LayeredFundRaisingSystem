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
    public class MyDonationsController : BaseUserController
    {
        // GET: MyDonations
        public ActionResult Index()
        {
            IDonationOnPostService donationService = ServiceFactory.GetDonationOnPostService();

            IEnumerable<DonationOnPost> listData = donationService.GetAll(true,true,true)
                .Where(a => a.UserInformationId == Convert.ToInt32(Session["UserInformationId"]))
                .OrderByDescending(a=> a.DonationDate);
            List<JoinDonationOnPosts_UserInformations_FundReqPosts> joinData = new List<JoinDonationOnPosts_UserInformations_FundReqPosts>();
            foreach (DonationOnPost item in listData)
            {
                JoinDonationOnPosts_UserInformations_FundReqPosts addData = new JoinDonationOnPosts_UserInformations_FundReqPosts()
                {
                    DonationId = item.DonationId,
                    DonationDate = item.DonationDate,
                    DonationAmount = item.DonationAmount,
                    TransectionId = item.TransectionId,
                    PostId = item.FundRequestPost.PostId,
                    FirstName = item.FundRequestPost.UserInformation.FirstName,
                    LastName = item.FundRequestPost.UserInformation.LastName,
                    PostTitle = item.FundRequestPost.PostTitle,
                    PostStatus = item.FundRequestPost.PostStatus,
                    RequiredAmount = item.FundRequestPost.RequiredAmount,
                    EndDate = item.FundRequestPost.EndDate
                    
                    
                };
                joinData.Add(addData);
            }
            ViewBag.DonationList = joinData.ToList();

            ViewBag.numberOfDonatedPosts = joinData.GroupBy(a => a.PostId).Count();

            ViewBag.SumOfDonatedAmount = joinData.Sum(a => a.DonationAmount);

            return View();
        }
    }
}