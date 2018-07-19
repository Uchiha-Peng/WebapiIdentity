using WebapiIdentity.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebapiIdentity.Models
{
    public partial class User
    {
        [Key]
        public int Uid { get; set; }
        public string NickName { get; set; }
        public string RealName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int AddressId { get; set; }
        public string LoginName { get; set; }
        public string PassWord { get; set; }
        public DateTime RegisterTime { get; set; }
        public DateTime LastLoginTime { get; set; }
    }
}
