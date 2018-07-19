using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace WebapiIdentity.Controllers
{
    public class AuthorizeController : ApiController
    {
        public string Token()
        {
            return "";
        }
    }
}
