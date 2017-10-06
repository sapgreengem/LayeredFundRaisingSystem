﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FundRaiserSystemEntity;

namespace FundRaiserSystemData
{
    public interface IBankInformationService
    {
        IEnumerable<BankInformation> GetAll();
        BankInformation Get(int id);
        int Insert(BankInformation bankInfo);
        int Update(BankInformation bankInfo);
        int Delete(int id);
    }
}
