using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FundRaiserSystemEntity
{

    public partial class FundRequestPost
    {
        [Key]
        public int PostId { get; set; }
        public string PostTitle { get; set; }
        public string PostDetails { get; set; }
        public double RequiredAmount { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public double CollectedAmount { get; set; }
        public double RemainingAmount { get; set; }
        public string PostImage { get; set; }
        public string PostStatus { get; set; }
        public int UserInformationId { get; set; }
        public int CategoryId { get; set; }
        public Nullable<int> RefundId { get; set; }
        public Nullable<int> ClickCounter { get; set; }

        public PostingCategory PostingCategory { get; set; }
        public UserInformation UserInformation { get; set; }
        public RefundingInformation RefundingInformation { get; set; }

        public IEnumerable<DonationOnPost> DonationOnPosts { get; set; }
        public IEnumerable<FundWithdraw> FundWithdraws { get; set; }


    }
}
