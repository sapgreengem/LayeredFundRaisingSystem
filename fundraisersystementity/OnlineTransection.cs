using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FundRaiserSystemEntity
{

    public partial class OnlineTransection
    {
        [Key]
        public int TransectionId { get; set; }
        public string GetwayId { get; set; }
        public int MethodId { get; set; }
        public int DonationId { get; set; }
        //public DonationOnPost DonationOnPost { get; set; }
    }
}
