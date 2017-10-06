using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace layeredFundRaiserSystem.Models
{
    public class JoinBankInformations_UserBankAccounts_UserInfo
    {
        public int BankId { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }

        public int UserBankAccountId { get; set; }
        public string UserAccountNo { get; set; }
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
    }
}