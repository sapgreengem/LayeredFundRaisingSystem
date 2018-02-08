using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//
using FundRaiserSystemEntity;
using FundRaiserSystemService;
using layeredFundRaiserSystem.Models;
using System.Globalization;

namespace layeredFundRaiserSystem.Controllers
{
    public class MyProfileController : BaseUserController
    {
        // GET: MyProfile
        public ActionResult Index()
        {
            if (Session["Login"] != null && Session["UserInformationId"] == null)
            {
                Response.Redirect("/FillAllInfo/Index");
            }
            IUserLoginService loginService = ServiceFactory.GetUserLoginService();
            ViewBag.UserLoginTableData = loginService.GetAll().Where(a => a.UserId == Convert.ToInt32(Session["Login"])).FirstOrDefault();

            IUserInformationService UserInfoService = ServiceFactory.GetUserInformationService();
            ViewBag.UserInformationTableData = UserInfoService.GetAll().Where(a => a.UserId == Convert.ToInt32(Session["Login"])).FirstOrDefault();

            ViewBag.UserBankAccountDetails = this.userBankAccount(Convert.ToInt32(Session["UserInformationId"]));
            return View();
        }

        [HttpGet]
        public ActionResult Edit()
        {
            if (Session["Login"] != null && Session["UserInformationId"] == null)
            {
                Response.Redirect("/FillAllInfo/Index");
            }
            IUserInformationService UserInfoService = ServiceFactory.GetUserInformationService();
            ViewBag.UserInfo = UserInfoService.GetAll().Where(a => a.UserId == Convert.ToInt32(Session["Login"]));

            ViewBag.EditBankDetails = this.userBankAccount(Convert.ToInt32(Session["UserInformationId"]));

            IBankInformationService bankService = ServiceFactory.GetBankInformationService();

            ViewBag.BankList = bankService.GetAll();
            ViewBag.CountryList = this.LoadCountry();

            return View();
        }
        [HttpPost]
        public ActionResult Edit(FormCollection coll)
        {
            if (!String.IsNullOrWhiteSpace(coll["firstName"].ToString()) && !String.IsNullOrWhiteSpace(coll["lastName"].ToString()) &&
                !String.IsNullOrWhiteSpace(coll["presentAddress"].ToString()) && !String.IsNullOrWhiteSpace(coll["permanentAddress"].ToString()) &&
                !String.IsNullOrWhiteSpace(coll["contactNo"].ToString()) && !String.IsNullOrWhiteSpace(coll["nationalID"].ToString()) &&
                !String.IsNullOrWhiteSpace(coll["userAccountNo"].ToString()) && coll["country"].ToString() != "SelectOne" &&
                Convert.ToInt32(coll["BankList"]) > 0)
            {

                IUserInformationService infoService = ServiceFactory.GetUserInformationService();
                UserInformation userInfo = infoService.Get(Convert.ToInt32(Session["UserInformationId"]));
                userInfo.FirstName = coll["firstName"].ToString();
                userInfo.LastName = coll["lastName"].ToString();
                userInfo.PresentAddress = coll["presentAddress"].ToString();
                userInfo.PermanentAddress = coll["permanentAddress"].ToString();
                userInfo.ContactNo = coll["contactNo"].ToString();
                userInfo.NationalId = coll["nationalID"].ToString();
                userInfo.Country = coll["country"].ToString();
                infoService.Update(userInfo);

                IUserBankAccountService bankService = ServiceFactory.GetUserBankAccountService();
                IEnumerable<UserBankAccount> allAccounts = bankService.GetAll().Where(a => a.UserInformationId == Convert.ToInt32(Session["UserInformationId"]));
                foreach (var item in allAccounts)
                {
                    UserBankAccount bankInfo = bankService.Get(Convert.ToInt32(item.UserBankAccountId)); //new UserBankAccount();
                    bankInfo.BankId = Convert.ToInt32(coll["BankList"]);
                    bankInfo.UserAccountNo = coll["userAccountNo"].ToString();
                    bankService.Update(bankInfo);
                }
                return Redirect("/MyProfile");
            }
            else
            {
                IUserInformationService UserInfoService = ServiceFactory.GetUserInformationService();
                ViewBag.UserInfo = UserInfoService.GetAll().Where(a => a.UserId == Convert.ToInt32(Session["Login"]));

                ViewBag.EditBankDetails = this.userBankAccount(Convert.ToInt32(Session["UserInformationId"]));

                IBankInformationService bankService = ServiceFactory.GetBankInformationService();
                ViewBag.BankList = bankService.GetAll();

                ViewBag.ErrorMessage = "Any fild Cannot Be Empty";
            }
            return View();
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            if (Session["Login"] != null && Session["UserInformationId"] == null)
            {
                Response.Redirect("/FillAllInfo/Index");
            }
            if (Session["Login"] == null && Session["UserInformationId"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                IUserInformationService UserInfoService = ServiceFactory.GetUserInformationService();
                UserInformation userInformation = UserInfoService.Get(Convert.ToInt32(Session["UserInformationId"]));//.Where(a => a.UserId == Convert.ToInt32(Session["Login"]));
                return View(userInformation);
            }

        }

        [HttpPost]
        public ActionResult ChangePassword(FormCollection coll)
        {
            IUserInformationService UserInfoService = ServiceFactory.GetUserInformationService();
            UserInformation userInformation = UserInfoService.Get(Convert.ToInt32(Session["UserInformationId"]));//.Where(a => a.UserId == Convert.ToInt32(Session["Login"]));

            if (!String.IsNullOrWhiteSpace(coll["CurrentPassword"].ToString())
                && !String.IsNullOrWhiteSpace(coll["NewPassword"].ToString())
                && !String.IsNullOrWhiteSpace(coll["confirmPassword"].ToString()))
            {
                if (Session["Login"] != null && Session["UserInformationId"] != null)
                {
                    IUserLoginService loginService = ServiceFactory.GetUserLoginService();
                    UserLogin userLogin = loginService.Get(Convert.ToInt32(Session["Login"]));//.Where(a => a.UserId == Convert.ToInt32(Session["Login"]));

                    if (userLogin.Password == coll["CurrentPassword"].ToString())
                    {
                        if (coll["NewPassword"].ToString() == coll["confirmPassword"].ToString())
                        {

                            userLogin.Password = coll["confirmPassword"].ToString();
                            loginService.Update(userLogin);
                            return Redirect("/Logout");
                        }
                        else
                        {
                            ViewBag.ErrorMessage = "New Password and Confirm Password do not match";
                        }
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Current Password is not correct";
                    }
                }
                else
                {
                    ViewBag.ErrorMessage = "Cannot Change Password of others";
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Please Fill Data";
            }
            
            return View(userInformation);
        }

        public List<string> LoadCountry()
        {
            List<string> CountryList = new List<string>();

            CultureInfo[] CInfoList = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            foreach (CultureInfo CInfo in CInfoList)
            {
                RegionInfo R = new RegionInfo(CInfo.LCID);
                if (!(CountryList.Contains(R.EnglishName)))
                {
                    CountryList.Add(R.EnglishName);
                }
            }
            CountryList.Sort();
            return CountryList;
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

                    //UserInformationId = item.UserInformation.UserInformationId,
                    //ProfilePicture = item.UserInformation.ProfilePicture,
                    //FirstName = item.UserInformation.FirstName,
                    //LastName = item.UserInformation.LastName,
                    //ContactNo = item.UserInformation.ContactNo,
                    //Country = item.UserInformation.Country,
                    //NationalId = item.UserInformation.NationalId,
                    //PermanentAddress = item.UserInformation.PermanentAddress,
                    //PresentAddress = item.UserInformation.PresentAddress,
                    //UserId = item.UserInformation.UserId
                };
                joinData.Add(load);
            }
            return joinData.ToList();
        }

        [HttpGet]
        public JsonResult GetCurrentPassword(string id)
        {
            IUserLoginService service = ServiceFactory.GetUserLoginService();
            var User = Convert.ToInt32(Session["Login"]);
            var x = 1;
            string msg = "";
            var login = service.Get(User);

            if (id != login.Password)
                msg = "Password Do Not Match";
            else
                msg = "Password Matched";

            return Json(new { message = msg }, JsonRequestBehavior.AllowGet);
        }
    }
}