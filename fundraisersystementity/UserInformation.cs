using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FundRaiserSystemEntity
{
    public partial class UserInformation
    {
        [Key]
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

        public UserLogin UserLogin { get; set; }
        public IEnumerable<FundRequestPost> FundRequestPosts { get; set; }
        public IEnumerable<DonationOnPost> DonationOnPosts { get; set; }
        public IEnumerable<UserBankAccount> UserBankAccounts { get; set; }
        public IEnumerable<FundWithdraw> FundWithdraws { get; set; }
    }
}
