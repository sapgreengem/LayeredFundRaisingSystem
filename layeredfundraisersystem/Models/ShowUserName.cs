﻿using FundRaiserSystemData;
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
        public string UserName(int id)
        {
            IUserInformationService userInformationService = ServiceFactory.GetUserInformationService();
            UserInformation userInformation = userInformationService.Get(id);
            return userInformation.FirstName.ToString();
        }

    }
}