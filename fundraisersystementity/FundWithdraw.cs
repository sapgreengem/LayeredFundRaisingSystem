using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FundRaiserSystemEntity
{

    public partial class FundWithdraw
    {
        [Key]
        public int WithdrawId { get; set; }
        public double WithdrawAmount { get; set; }
        public System.DateTime WithdrawDate { get; set; }
        public string RequestStatus { get; set; }
        public int UserInformationId { get; set; }
        public int PostId { get; set; }

        public UserInformation UserInformation { get; set; }
        public FundRequestPost FundRequestPost { get; set; }
    }
}
