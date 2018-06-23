using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FundRaiserSystemEntity
{

    public partial class Setting
    {
        [Key]
        public int SettingId { get; set; }
        public double ServiceCharge { get; set; }
        public double RefundCharge { get; set; }
        public string SystemContactNo { get; set; }
        public string SystemAddress { get; set; }
        public double TotalIncome { get; set; }
        public string SystemBankAccount { get; set; }
        public double CollectedAmount { get; set; }
        public string SystemEmail { get; set; }
    }
}
