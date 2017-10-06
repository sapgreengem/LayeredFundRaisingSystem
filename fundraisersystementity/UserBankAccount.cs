using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FundRaiserSystemEntity
{

    public partial class UserBankAccount
    {
        [Key]
        public int UserBankAccountId { get; set; }
        public string UserAccountNo { get; set; }
        public int UserInformationId { get; set; }
        public int BankId { get; set; }

        public UserInformation UserInformation { get; set; }
        public BankInformation BankInformation { get; set; }
    }
}
