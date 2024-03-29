﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FundRaiserSystemEntity;

namespace FundRaiserSystemData
{
    public interface IFundWithdrawDataAccess
    {
        IEnumerable<FundWithdraw> GetAll(bool includeUserInformations = false, bool includeFundRequestPosts = false);
        FundWithdraw Get(int id, bool includeUserInformations = false, bool includeFundRequestPosts = false);
        int Insert(FundWithdraw fundWithdraw);
        int Update(FundWithdraw fundWithdraw);
        int Delete(int id);
    }
}
