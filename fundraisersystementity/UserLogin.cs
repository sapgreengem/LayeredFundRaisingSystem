using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FundRaiserSystemEntity
{

    public partial class UserLogin
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string Email { get; set; }
        public string Password { get; set; }
        public string AutoGeneratedLink { get; set; }
        public System.DateTime UserCreationDate { get; set; }
        public string Status { get; set; }

        //public UserInformation UserInformation { get; set; }
    }
}
