//
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
    public class ManageUserController : BaseAdminController
    {
        [HttpGet]
        public ActionResult Index()
        {
            IUserInformationService service = ServiceFactory.GetUserInformationService();

            IEnumerable<UserInformation> info = service.GetAll(true);
            List<JoinUserLogin_UserInfo> allInfo = new List<JoinUserLogin_UserInfo>();
            foreach (UserInformation item in info)
            {
                JoinUserLogin_UserInfo load = new JoinUserLogin_UserInfo()
                {
                    UserInformationId = item.UserInformationId,
                    UserId = item.UserLogin.UserId,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Email = item.UserLogin.Email,
                    PresentAddress = item.PresentAddress,
                    PermanentAddress =item.PermanentAddress,
                    ContactNo = item.ContactNo,
                    NationalId = item.NationalId,
                    Country =item.Country,
                    ProfilePicture = item.ProfilePicture,
                    UserCreationDate = item.UserLogin.UserCreationDate,
                    Status = item.UserLogin.Status
                };
                allInfo.Add(load);
            }
            return View(allInfo);
        }

        [HttpGet]
        public ActionResult BlockUser(int id)
        {
            IUserLoginService service = ServiceFactory.GetUserLoginService();
            UserLogin UserLogin = service.Get(id);
            UserLogin.Status = "Blocked";
            service.Update(UserLogin);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult UnBlockUser(int id)
        {
            IUserLoginService service = ServiceFactory.GetUserLoginService();
            UserLogin UserLogin = service.Get(id);
            UserLogin.Status = "Active";
            service.Update(UserLogin);
            return RedirectToAction("Index");
        }
        //[HttpGet]
        //public ActionResult FindUser(int id)
        //{
        //    UserLogin UserLogin = context.UserLogins.SingleOrDefault(obj => obj.UserId == id);
        //    UserInformation UserInformation = context.UserInformations.SingleOrDefault(obj => obj.UserId == id);

        //    ViewBag.FirstName = UserInformation.FirstName;
        //    ViewBag.LastName = UserInformation.LastName;
        //    ViewBag.Email = UserLogin.Email;
        //    ViewBag.PresentAddress = UserInformation.PresentAddress;
        //    ViewBag.PermanentAddress = UserInformation.PermanentAddress;
        //    ViewBag.ContactNo = UserInformation.ContactNo;
        //    ViewBag.Country = UserInformation.Country;
        //    ViewBag.Status = UserLogin.Status;
        //    ViewBag.ID = UserLogin.UserId;

        //    return View();
        //}
    }
}