using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace layeredFundRaiserSystem.Models
{
    public class JoinDonationOnPosts_UserInformations_FundReqPosts
    {
        public int DonationId { get; set; }
        public System.DateTime DonationDate { get; set; }
        public double DonationAmount { get; set; }
        public int TransectionId { get; set; }
        public int PostId { get; set; }
        public int UserInformationId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PresentAddress { get; set; }
        public string PermanentAddress { get; set; }
        public string ContactNo { get; set; }
        public string NationalId { get; set; }
        public string Country { get; set; }
        public string ProfilePicture { get; set; }
        public int UserId { get; set; }

        public string PostTitle { get; set; }
        public string PostDetails { get; set; }
        public double RequiredAmount { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public double CollectedAmount { get; set; }
        public double RemainingAmount { get; set; }
        public string PostImage { get; set; }
        public string PostStatus { get; set; }
        public int CategoryId { get; set; }
        public Nullable<int> RefundId { get; set; }
        public Nullable<int> ClickCounter { get; set; }
    }
}