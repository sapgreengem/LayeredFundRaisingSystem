using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FundRaiserSystemEntity
{

    public partial class DonationOnPost
    {
        [Key]

        public int DonationId { get; set; }
        public System.DateTime DonationDate { get; set; }
        public double DonationAmount { get; set; }
        public int TransectionId { get; set; }
        public int PostId { get; set; }
        public int UserInformationId { get; set; }
        public string ShowDonationInfo { get; set; }

        public UserInformation UserInformation { get; set; }
        public FundRequestPost FundRequestPost { get; set; }
        public OnlineTransection OnlineTransection { get; set; }
    }
}
