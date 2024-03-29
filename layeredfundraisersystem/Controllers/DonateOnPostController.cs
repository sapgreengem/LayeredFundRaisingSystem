﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FundRaiserSystemService;
using FundRaiserSystemEntity;
using layeredFundRaiserSystem.Models;

namespace layeredFundRaiserSystem.Controllers
{
    public class DonateOnPostController : Controller
    {
        // GET: DonateOnPost/Index/4
        public ActionResult Index(int id)
        {
            if (Session["Login"] == null && id != 0)
            {
                Session["StoreURL"] = "/DonateOnPost/Index/" + id;
                Response.Redirect("/Login", false);
            }

        #region HandleError
            var userID = Convert.ToInt32(Session["UserInformationId"]);

            IFundRequestPostService fundRequestPostService = ServiceFactory.GetFundRequestPostService();
            FundRequestPost fundRequestPost = fundRequestPostService.Get(id);

            if (fundRequestPost.UserInformationId == userID)
                //Response.Redirect("/Error");
                return RedirectToAction("Index", "Error", new { id = "You Cannot Donate On Your Post" });
            #endregion HandleError

            this.loginUserName();

            ITransectionMethodNameService service = ServiceFactory.GetTransectionMethodNameService();
            ViewBag.TransectionMethodName = service.GetAll();
            return View();
        }

        [HttpPost]
        public ActionResult Index(int id, FormCollection coll)
        {
            ITransectionMethodNameService service = ServiceFactory.GetTransectionMethodNameService();
            ViewBag.TransectionMethodName = service.GetAll();

            if (coll["method"] != null && !String.IsNullOrWhiteSpace(coll["Ammount"]))
            {
                IFundRequestPostService postService = ServiceFactory.GetFundRequestPostService();
                FundRequestPost item = postService.Get(id);
                if (item.UserInformationId != Convert.ToInt32(Session["UserInformationId"]))
                {
                    if (Convert.ToDouble(coll["Ammount"]) >= 50)
                    {
                        IOnlineTransectionService transectionService = ServiceFactory.GetOnlineTransectionService();
                        OnlineTransection transection = new OnlineTransection();
                        transection.GetwayId = id + " " + DateTime.Now + " " + Convert.ToDouble(coll["Ammount"]);
                        transection.MethodId = Convert.ToInt32(coll["method"]);
                        transection.DonationId = 0;
                        transectionService.Insert(transection);

                        IDonationOnPostService donateService = ServiceFactory.GetDonationOnPostService();
                        DonationOnPost donation = new DonationOnPost();
                        donation.DonationAmount = Convert.ToDouble(coll["Ammount"]);
                        donation.DonationDate = DateTime.Now;
                        donation.PostId = id;
                        donation.UserInformationId = Convert.ToInt32(Session["UserInformationId"]);
                        donation.TransectionId = transection.TransectionId;
                        if (coll["ShowInfo"] != null)
                        {
                            donation.ShowDonationInfo = "NO";
                        }
                        else
                            donation.ShowDonationInfo = "YES";

                        donateService.Insert(donation);

                        this.updateCollectedMoney(Convert.ToDouble(coll["Ammount"]), id);

                        return Redirect("/PostDetailsView/Index/" + id); 
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Invalid Donation Amount. You cannot donate less than 50/=";
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "You Cannot Donate on your own post";
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Select payment method and Fill amount first";
            }

            this.loginUserName();
            return View();
        }

        public void updateCollectedMoney(double amount, int id)
        {
            
            IFundRequestPostService fundService = ServiceFactory.GetFundRequestPostService();
            FundRequestPost postAmount = fundService.Get(id);
            postAmount.CollectedAmount += amount;
            postAmount.RemainingAmount += amount;
            fundService.Update(postAmount);
            
            if ((postAmount.RequiredAmount - postAmount.CollectedAmount) <= 0)
            {
                postAmount.PostStatus = "Completed";
                fundService.Update(postAmount);
            }

            ISettingService settingsService = ServiceFactory.GetSettingService();
            Setting settings = settingsService.Get(1);
            settings.CollectedAmount += amount;
            settingsService.Update(settings);
        }
        public void loginUserName()
        {
            if (Session["UserInformationId"] != null)
            {
                ShowUserName name = new ShowUserName();
                ViewBag.LoginName = name.UserName(Convert.ToInt32(Session["UserInformationId"]));
            }
            else
            {
                Response.Redirect("/Login");
            }
        }
    }
}