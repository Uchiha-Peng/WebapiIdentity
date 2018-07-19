using WebapiIdentity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebapiIdentity.Models
{
    public partial class Shopcart
    {
        [Key]
        public int CartId { get; set; }
        public int Uid { get; set; }
        public int ProId { get; set; }
        public int Num { get; set; }
    }
}
