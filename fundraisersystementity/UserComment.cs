using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FundRaiserSystemEntity
{

    public partial class UserComment
    {
        [Key]
        public int CommentId { get; set; }
        public string Comment { get; set; }
        public int PostId { get; set; }
        public int UserInformationId { get; set; }
    }
}
