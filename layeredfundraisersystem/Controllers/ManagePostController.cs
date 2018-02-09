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
    public class ManagePostController : BaseAdminController
    {
        IFundRequestPostService service = ServiceFactory.GetFundRequestPostService();
        // GET: ManagePost
        public ActionResult Index()
        {
            //IEnumerable<FundRequestPost> post = service.GetAll(true, true, false).Where(a => a.PostStatus == "Pending"); // Include User & Ctegory
            ViewBag.viewPost = this.loadRequest("Pending");//.Where(a => a.PostStatus == "Pending");
            return View();
        }
        
        [HttpGet]
        public ActionResult Approve(int id)
        {
            FundRequestPost itemToUpdate = service.Get(id, true, true, false);
            itemToUpdate.PostStatus = "Active";
            service.Update(itemToUpdate);
            
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Reject(int id)
        {
            FundRequestPost itemToUpdate = service.Get(id, true, true, false);
            itemToUpdate.PostStatus = "Reject";
            service.Update(itemToUpdate);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Proof(int id)
        {
            IPostProofService proofService = ServiceFactory.GetPostProofService();
            ViewBag.Proofs = proofService.GetAll().Where(a => a.PostId == id);

            return View();
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
            ViewBag.viewPost = this.loadRequest("Pending");//.Where(a => a.PostStatus == "Pending");
            return View();
        }

        public IEnumerable<JoinFundRequestPost_Category_UserInfo> loadRequest(string status)
        {
            IEnumerable<FundRequestPost> post;
            if (status == "All")
            {
                post = service.GetAll(true, true, false);

            }
            else
            {
                post = service.GetAll(true, true, false).Where(a => a.PostStatus == status); // Include User & Ctegory
            }
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
                    UserId = item.UserInformation.UserId,
                    UserInformationId = item.UserInformationId                 
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
        public ActionResult StatusWisePost()
        {          
            ViewBag.PostStatus = PostStatus();
            ViewBag.viewPost = this.loadRequest("All");
            return View();
        }

        [HttpPost]
        public ActionResult StatusWisePost(FormCollection collection)
        {
            string status = collection["Status"];
            ViewBag.viewPost = this.loadRequest(status);

            List<string> Status = PostStatus();
            List<string> Status1 = new List<string>();
            foreach (var item in Status)
            {
                if(item != status)
                {
                    Status1.Add(item);
                }
            }
            Status1.Insert(0, status);
            ViewBag.PostStatus = Status1;

            return View();
        }

        public List<string> PostStatus()
        {
            List<string> PostStatus = new List<string>();
            PostStatus.Add("All");
            PostStatus.Add("Active");
            PostStatus.Add("Reject");
            PostStatus.Add("Expired");
            PostStatus.Add("Completed");

            return PostStatus;
        }
    }
}