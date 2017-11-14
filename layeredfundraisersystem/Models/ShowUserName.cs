using FundRaiserSystemData;
using FundRaiserSystemEntity;
using FundRaiserSystemService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace layeredFundRaiserSystem.Models
{
    public class ShowUserName
    {
        public IUserInformationService userInformationService = ServiceFactory.GetUserInformationService();
        public string UserName(int id)
        {
            UserInformation userInformation = userInformationService.Get(id);
            return userInformation.FirstName.ToString();
        }
        public string AdminName(int id)
        {
            IAdministrationService adminInfoService = ServiceFactory.GetAdministrationService();
            Administration adminName = adminInfoService.Get(id);
            return adminName.FirstName.ToString();
        }
        public string UserImage(int id)
        {
            UserInformation userInformation = userInformationService.Get(id);
            return userInformation.ProfilePicture.ToString();
        }

    }
}