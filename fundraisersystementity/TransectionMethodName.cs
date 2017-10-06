using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FundRaiserSystemEntity
{

    public partial class TransectionMethodName
    {
        [Key]
        public int MethodId { get; set; }
        public string MethodName { get; set; }
        public string BankName { get; set; }
    }
}
