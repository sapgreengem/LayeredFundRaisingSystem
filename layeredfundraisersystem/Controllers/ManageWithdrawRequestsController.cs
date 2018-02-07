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

            if (fundWithdraw != null)
            {
                IFundRequestPostService postService = ServiceFactory.GetFundRequestPostService();
                FundRequestPost fundRequestPost = postService.Get(fundWithdraw.PostId);

                ISettingService settingsService = ServiceFactory.GetSettingService();
                Setting settings = settingsService.Get(1);

                if (fundRequestPost.RemainingAmount >= fundWithdraw.WithdrawAmount)
                {

                    fundRequestPost.RemainingAmount -= fundWithdraw.WithdrawAmount;
                    postService.Update(fundRequestPost);

                    fundWithdraw.RequestStatus = "Transfered";
                    withdrawService.Update(fundWithdraw);

                    settings.TotalIncome += fundWithdraw.WithdrawAmount * (settings.ServiceCharge / 100);
                    settingsService.Update(settings);

                }
                else
                {
                    ViewBag.ErrorMessage = "Fund Cannot Be Transferred, Invalid Requested Amount";
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Invalid Withdraw Request";
            }

            ViewBag.WithdrawRequests = this.loadRequest();
            return RedirectToAction("Index", "ManageWithdrawRequests");
        }

        [HttpGet]
        public ActionResult UserProfile(int id, int id1)
        {
            IUserLoginService loginService = ServiceFactory.GetUserLoginService();
            ViewBag.LoginData = loginService.GetAll().Where(a => a.UserId == id);

            IUserInformationService UserInfoService = ServiceFactory.GetUserInformationService();
            ViewBag.UserInfo = UserInfoService.GetAll().Where(a => a.UserId == id);


            ViewBag.UserBankAccountDetails = this.userBankAccount(id1);

            foreach (var item in UserInfoService.GetAll().Where(a => a.UserId == id))
            {
                ViewBag.ProfilePic = item.ProfilePicture;
            }
            return View();
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
                    UserId = item.UserInformation.UserId,
                    UserInformationId = item.UserInformation.UserInformationId,
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
        
        public IEnumerable<JoinBankInformations_UserBankAccounts_UserInfo> userBankAccount(int UserInfoId)
        {
            IUserBankAccountService accService = ServiceFactory.GetUserBankAccountService();
            IEnumerable<UserBankAccount> getAccData = accService.GetAll(true, true).Where(a => a.UserInformationId == UserInfoId);
            List<JoinBankInformations_UserBankAccounts_UserInfo> joinData = new List<JoinBankInformations_UserBankAccounts_UserInfo>();
            foreach (UserBankAccount item in getAccData)
            {
                JoinBankInformations_UserBankAccounts_UserInfo load = new JoinBankInformations_UserBankAccounts_UserInfo()
                {
                    BankId = item.BankId,
                    BankName = item.BankInformation.BankName,
                    BranchName = item.BankInformation.BranchName,

                    UserAccountNo = item.UserAccountNo,
                    UserBankAccountId = item.UserBankAccountId
                };
                joinData.Add(load);
            }
            return joinData.ToList();
        }

        [HttpGet]
        public JsonResult GetUser(int id, int id1)
        {
            IUserLoginService loginService = ServiceFactory.GetUserLoginService();
            var UserLoginTableData = loginService.GetAll().Where(a => a.UserId == id).FirstOrDefault();

            IUserInformationService UserInfoService = ServiceFactory.GetUserInformationService();
            var UserInformationTableData = UserInfoService.GetAll().Where(a => a.UserId == id).FirstOrDefault();

            UserInfo userInfo = new UserInfo();
            userInfo.NationalId = UserInformationTableData.NationalId;
            userInfo.FirstName = UserInformationTableData.FirstName;
            userInfo.LastName = UserInformationTableData.LastName;
            userInfo.PermanentAddress = UserInformationTableData.PermanentAddress;
            userInfo.PresentAddress = UserInformationTableData.PresentAddress;
            userInfo.ProfilePicture = UserInformationTableData.ProfilePicture;
            userInfo.ContactNo = UserInformationTableData.ContactNo;
            userInfo.Country = UserInformationTableData.Country;

            userInfo.Email = UserLoginTableData.Email;
            userInfo.UserCreationDate = UserLoginTableData.UserCreationDate;
            userInfo.Status = UserLoginTableData.Status;

            return Json(new { list = userInfo }, JsonRequestBehavior.AllowGet);
        }

    }

    public class UserInfo
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string AutoGeneratedLink { get; set; }
        public System.DateTime UserCreationDate { get; set; }
        public string Status { get; set; }

        public int UserInformationId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PresentAddress { get; set; }
        public string PermanentAddress { get; set; }
        public string ContactNo { get; set; }
        public string NationalId { get; set; }
        public string Country { get; set; }
        public string ProfilePicture { get; set; }
    }
}