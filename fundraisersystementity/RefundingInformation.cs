using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FundRaiserSystemEntity
{
    
    public partial class RefundingInformation
    {
        [Key]
        public int RefundId { get; set; }
        public double RefundAmount { get; set; }
        public int PostId { get; set; }
        public int AdminId { get; set; }

        public IEnumerable<FundRequestPost> FundRequestPosts { get; set; }
    }
}
