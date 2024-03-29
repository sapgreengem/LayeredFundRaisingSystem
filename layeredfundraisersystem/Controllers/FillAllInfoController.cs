﻿using FundRaiserSystemEntity;
using FundRaiserSystemService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;

namespace layeredFundRaiserSystem.Controllers
{
    public class FillAllInfoController : BaseUserController
    {
        public ActionResult Index()
        {
            if (Session["UserInformationId"] != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                IUserLoginService service = ServiceFactory.GetUserLoginService();
                UserLogin user = service.Get(Convert.ToInt32(Session["Login"]));

                IBankInformationService bankService = ServiceFactory.GetBankInformationService();
                ViewBag.BankList = bankService.GetAll();

                ViewBag.CountryList = this.LoadCountry();

                return View(user);
            }
            
        }

        [HttpPost]
        public ActionResult Index(FormCollection collection, HttpPostedFileBase file)
        {
            if (Session["UserInformationId"] == null && Session["Login"] != null)
            {
                IUserLoginService userService = ServiceFactory.GetUserLoginService();
                UserLogin user = userService.Get(Convert.ToInt32(Session["Login"]));
                IBankInformationService bankService = ServiceFactory.GetBankInformationService();
                ViewBag.BankList = bankService.GetAll();
                
                if (file != null && file.ContentLength > 0
                && !String.IsNullOrWhiteSpace(collection["firstName"].ToString())
                && !String.IsNullOrWhiteSpace(collection["lastName"].ToString())
                && !String.IsNullOrWhiteSpace(collection["presentAddress"].ToString())
                && !String.IsNullOrWhiteSpace(collection["permanentAddress"].ToString())
                && !String.IsNullOrWhiteSpace(collection["nationalID"].ToString())
                && !String.IsNullOrWhiteSpace(collection["contactNo"].ToString())
                && collection["country"].ToString() != "SelectOne"
                && !String.IsNullOrWhiteSpace(collection["userAccountNo"].ToString())
                && Convert.ToInt32(collection["BankList"]) > 0)
                {
                    IUserInformationService service = ServiceFactory.GetUserInformationService();
                    UserInformation uInfo = new UserInformation();

                    uInfo.FirstName = collection["firstName"].ToString();
                    uInfo.LastName = collection["lastName"].ToString();
                    uInfo.PresentAddress = collection["presentAddress"].ToString();
                    uInfo.PermanentAddress = collection["permanentAddress"].ToString();
                    uInfo.NationalId = collection["nationalID"].ToString();
                    uInfo.ContactNo = collection["contactNo"].ToString();
                    uInfo.Country = collection["country"].ToString();
                    uInfo.UserId = Convert.ToInt32(Session["Login"]);

                    var fileName = Path.GetFileName(file.FileName);
                    var NewFileName = DateTime.Now.ToFileTime() + " " + fileName;
                    var fileExt = Path.GetExtension(fileName).ToString();   

                    if (file.ContentLength > 5242880)
                    {
                        ViewBag.ErrorMessage = "File size must be less than 5MB";
                        ViewBag.BankList = bankService.GetAll();
                        ViewBag.CountryList = this.LoadCountry();
                        ViewBag.FirstName = uInfo.FirstName;
                        ViewBag.LastName = uInfo.LastName;
                        ViewBag.PresentAddress = uInfo.PresentAddress;
                        ViewBag.PermanentAddress = uInfo.PermanentAddress;
                        ViewBag.ContactNo = uInfo.ContactNo;
                        ViewBag.NID = uInfo.NationalId;
                        return View(user);
                    }
                    if (fileExt != ".jpeg" && fileExt != ".JPEG" &&
                        fileExt != ".jpg" && fileExt != ".JPG" &&
                        fileExt != ".png" && fileExt != ".PNG")
                    {
                        ViewBag.ErrorMessage = "Invalid Image File Type";
                        ViewBag.BankList = bankService.GetAll();
                        ViewBag.CountryList = this.LoadCountry();
                        ViewBag.FirstName = uInfo.FirstName;
                        ViewBag.LastName = uInfo.LastName;
                        ViewBag.PresentAddress = uInfo.PresentAddress;
                        ViewBag.PermanentAddress = uInfo.PermanentAddress;
                        ViewBag.ContactNo = uInfo.ContactNo;
                        ViewBag.NID = uInfo.NationalId;
                        return View(user);
                    }

                    uInfo.ProfilePicture = NewFileName;
                    var path = Path.Combine(Server.MapPath("~/ProfilePictures"), NewFileName);
                    file.SaveAs(path);
                    service.Insert(uInfo);

                    Session["UserInformationId"] = this.getUserInfoId();

                    IUserBankAccountService UserAccService = ServiceFactory.GetUserBankAccountService();
                    UserBankAccount uAcc = new UserBankAccount();

                    uAcc.BankId = Convert.ToInt32(collection["BankList"]);
                    uAcc.UserAccountNo = collection["userAccountNo"].ToString();
                    uAcc.UserInformationId = Convert.ToInt32(Session["UserInformationId"]);
                    UserAccService.Insert(uAcc);

                    return RedirectToAction("Index", "home");
                }
                else
                {
                    ViewBag.ErrorMessage = "Fill All Info";
                    ViewBag.BankList = bankService.GetAll();
                    ViewBag.CountryList = this.LoadCountry();
                    ViewBag.FirstName = collection["firstName"].ToString();
                    ViewBag.LastName = collection["lastName"].ToString();
                    ViewBag.PresentAddress = collection["presentAddress"].ToString();
                    ViewBag.PermanentAddress = collection["permanentAddress"].ToString();
                    ViewBag.ContactNo = collection["contactNo"].ToString();
                    ViewBag.NID = collection["nationalID"].ToString();
                    return View(user);
                }  
            }
            else
            {
                return RedirectToAction("Index", "home");
            }
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
        public int getUserInfoId()
        {
            int userInfoId = 0;
            IUserInformationService userInfoService = ServiceFactory.GetUserInformationService();
            IEnumerable<UserInformation> user = userInfoService.GetAll().Where(a => a.UserId == Convert.ToInt32(Session["Login"]));
            foreach (var item in user)
            {
                userInfoId = item.UserInformationId;
            }
            return userInfoId;
        }
    }
}