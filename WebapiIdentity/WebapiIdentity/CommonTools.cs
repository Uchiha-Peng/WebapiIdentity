using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebapiIdentity
{
    public static class CommonTools
    {
        //时间格式化
        public static JsonSerializerSettings serializerSettings = new JsonSerializerSettings
        {
            DateFormatString = "yyyy-MM-dd HH:mm:ss"
        };
    }
}