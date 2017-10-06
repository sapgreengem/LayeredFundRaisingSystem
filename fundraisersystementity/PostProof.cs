using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FundRaiserSystemEntity
{

    public partial class PostProof
    {
        [Key]
        public int ProofId { get; set; }
        public string PictureForProof { get; set; }
        public int PostId { get; set; }
    }
}
