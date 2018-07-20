using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebapiIdentity.Models
{
    public class RequestLog
    {
        [Key]
        public int Rid { get; set; }
        public string ApiName { get; set; }
        public int ApiType { get; set; }
        public string ApiTypeName { get; set; }
        public int RequestUserID { get; set; }
        public string RequestIP { get; set; }
        public DateTime RequestTime { get; set; }
        public int IsSuccess { get; set; }
        public string Description { get; set; }
    }
}