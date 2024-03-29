﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FundRaiserSystemEntity;
using FundRaiserSystemService;
using layeredFundRaiserSystem.Models;

namespace layeredFundRaiserSystem.Controllers
{
    public class FundWithdrawController : BaseUserController
    {
        // GET: FundWithdraw
        [HttpGet]
        public ActionResult Index(int id)
        {
            IFundRequestPostService postService = ServiceFactory.GetFundRequestPostService();
            FundRequestPost selectPost = postService.Get(id);

            ISettingService settingsService = ServiceFactory.GetSettingService();
            Setting settings = settingsService.Get(1);
            ViewBag.ServiceCharge = settings.ServiceCharge.ToString();

            return View(selectPost);
        }
        [HttpPost]
        public ActionResult Index(int id, FormCollection coll)
        {
            IFundRequestPostService postService = ServiceFactory.GetFundRequestPostService();
            FundRequestPost selectPost = postService.Get(id);

            ISettingService settingsService = ServiceFactory.GetSettingService();
            Setting settings = settingsService.Get(1);

            if (!String.IsNullOrWhiteSpace(coll["Amount"].ToString()))
            {
                int check = postService.GetAll().Where(a => a.UserInformationId == Convert.ToInt32(Session["UserInformationId"])).Where(a => a.PostId == id).Count();
                if (check > 0)
                {
                    if (Convert.ToDouble(selectPost.RemainingAmount) >= Convert.ToDouble(coll["Amount"]) && Convert.ToDouble(coll["Amount"]) > 0)
                    {
                        IFundWithdrawService withdrawService = ServiceFactory.GetFundWithdrawService();
                        var count = withdrawService.GetAll().Where(a => a.PostId == selectPost.PostId).Where(a => a.RequestStatus == "Pending").Count();
                        if (count > 0)
                        {
                            ViewBag.ErrorMessage = "Already Requested";
                        }
                        else
                        {
                            FundWithdraw withdraw = new FundWithdraw();
                            withdraw.PostId = id;
                            withdraw.WithdrawAmount = Convert.ToDouble(coll["Amount"]);
                            withdraw.WithdrawWithCharge = (Convert.ToDouble(coll["Amount"]) - Convert.ToDouble(coll["Amount"]) * (settings.ServiceCharge / 100));
                            withdraw.WithdrawDate = DateTime.Now;
                            withdraw.RequestStatus = "Pending";
                            withdraw.UserInformationId = Convert.ToInt32(Session["UserInformationId"]);
                            withdrawService.Insert(withdraw);

                            ViewBag.ErrorMessage = "Request sent. After " + settings.ServiceCharge + " % service charge you will get " + (Convert.ToDouble(coll["Amount"]) - Convert.ToDouble(coll["Amount"]) * (settings.ServiceCharge / 100)) + " /=";

                            ViewBag.ServiceCharge = settings.ServiceCharge.ToString();
                            return View(selectPost);
                        }
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Invalid Amount";
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "You Can Only Request For Withdraw from your own post";
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Invalid Amount";
            }

            ViewBag.ServiceCharge = settings.ServiceCharge.ToString();
            return View(selectPost);
        }

        [HttpGet]
        public ActionResult MyWithdraws()
        {
            IFundWithdrawService withdrawService = ServiceFactory.GetFundWithdrawService();
            IEnumerable<FundWithdraw> withdrawList = withdrawService.GetAll(true, true).Where(a => a.UserInformationId == Convert.ToInt32(Session["UserInformationId"]));
            List<JoinFundUithdraws_FundRequestPost_UserInfo> joinData = new List<JoinFundUithdraws_FundRequestPost_UserInfo>();
            foreach (FundWithdraw item in withdrawList)
            {
                JoinFundUithdraws_FundRequestPost_UserInfo load = new JoinFundUithdraws_FundRequestPost_UserInfo()
                {
                    PostId = item.PostId,
                    PostTitle = item.FundRequestPost.PostTitle,
                    RequiredAmount = item.FundRequestPost.RequiredAmount,
                    RemainingAmount = item.FundRequestPost.RemainingAmount,
                    FirstName = item.UserInformation.FirstName,
                    LastName = item.UserInformation.LastName,

                    WithdrawId = item.WithdrawId,
                    WithdrawAmount = item.WithdrawAmount,
                    WithdrawWithCharge = item.WithdrawWithCharge,
                    WithdrawDate = item.WithdrawDate,
                    RequestStatus = item.RequestStatus
                };
                joinData.Add(load);
            }

            if (withdrawList.Count() <=0)
            {
                ViewBag.ErrorMessage = "No Request available";
            }
            else
            {
                ViewBag.AllRequests = joinData.ToList();
            }
            return View();
        }
    }
}