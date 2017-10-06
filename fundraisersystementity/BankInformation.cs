using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FundRaiserSystemEntity
{

    public partial class BankInformation
    {
        [Key]
        public int BankId { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }

        public IEnumerable<UserBankAccount> UserBankAccounts { get; set; }
    }
}
