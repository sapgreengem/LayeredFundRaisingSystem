using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FundRaiserSystemEntity
{
    public partial class UserRating
    {
        [Key]
        public int RatingId { get; set; }
        public int Rating { get; set; }
        public int PostId { get; set; }
        public int UserInformationId { get; set; }
    }
}