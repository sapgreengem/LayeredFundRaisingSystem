using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FundRaiserSystemEntity
{

    public partial class PostingCategory
    {
        [Key]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public IEnumerable<FundRequestPost> FundRequestPosts{ get; set; }
    }
}
