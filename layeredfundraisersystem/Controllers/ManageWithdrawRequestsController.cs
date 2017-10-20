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
    public class ManageWithdrawRequestsController : BaseAdminController
    {
        // GET: ManageWithdrawRequests
        public ActionResult Index()
        {
            ViewBag.WithdrawRequests = this.loadRequest();
            return View();
        }
        [HttpGet]
        public ActionResult AcceptRequest(int id)
        {
            IFundWithdrawService withdrawService = ServiceFactory.GetFundWithdrawService();
            FundWithdraw fundWithdraw = withdrawService.Get(id);
            
            IFundRequestPostService postService = ServiceFactory.GetFundRequestPostService();
            FundRequestPost fundRequestPost = postService.Get(fundWithdraw.PostId);

            if (fundRequestPost.RemainingAmount >= fundWithdraw.WithdrawAmount)
            {

                fundRequestPost.RemainingAmount -= fundWithdraw.WithdrawAmount;
                postService.Update(fundRequestPost);

                fundWithdraw.RequestStatus = "Transfered";
                withdrawService.Update(fundWithdraw);

            }
            else
            {
                ViewBag.ErrorMessage = "Fund Cannot Be Transferred <br> Invalid Requested Amount";
            }
            ViewBag.WithdrawRequests = this.loadRequest();
            return RedirectToAction("Index");
        }

        public IEnumerable<JoinFundUithdraws_FundRequestPost_UserInfo> loadRequest()
        {
            IFundWithdrawService withdrawService = ServiceFactory.GetFundWithdrawService();
            IEnumerable<FundWithdraw> withdrawList = withdrawService.GetAll(true, true).Where(a => a.RequestStatus == "Pending");
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
                    WithdrawDate = item.WithdrawDate,
                    RequestStatus = item.RequestStatus
                };
                joinData.Add(load);
            }
            return joinData.ToList();
        }
    }
}