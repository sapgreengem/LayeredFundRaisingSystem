﻿//
using FundRaiserSystemService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace layeredFundRaiserSystem.Controllers
{
    public class CountPendings
    {
        public int CountPendingPost()
        {
            IFundRequestPostService service = ServiceFactory.GetFundRequestPostService();
            return service.GetAll().Where(a => a.PostStatus == "Pending").Count();
        }
        public int CountPendingWithdraws()
        {
            IFundWithdrawService service = ServiceFactory.GetFundWithdrawService();
            return service.GetAll().Where(a => a.RequestStatus == "Pending").Count();
        }

        public int CountTimeExtendReq()
        {
            IFundRequestPostService service = ServiceFactory.GetFundRequestPostService();
            return service.GetAll().Where(a => a.PostStatus == "ExtendRequest").Count();
        }
    }
}