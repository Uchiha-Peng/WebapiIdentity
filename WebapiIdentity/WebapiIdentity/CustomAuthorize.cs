using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using WebapiIdentity.Models;

namespace WebapiIdentity
{
    public class CustomAuthorize : AuthorizationFilterAttribute
    {
        private static ShopDBContext db = new ShopDBContext();
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            //响应消息
            HttpResponseMessage message = null;
            //角色
            string Role = string.Empty;
            //用户ID
            string Sid = string.Empty;
            //用户名
            string IdentityName = string.Empty;
            //调用的ActionName
            string ActionName = string.Empty;
            //调用的IP地址
            string Ipaddress = string.Empty;

            //授权是否通过
            var isOAuthValid = actionContext.RequestContext.Principal.Identity.IsAuthenticated;
            if (!isOAuthValid)
            {
                message = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, new HttpError("Token is invalid"));
                actionContext.Response = message;
                return;
            }
            //2. 接口调用次数是否达到上限
            ActionName = actionContext.Request.RequestUri.AbsolutePath;
            IdentityName = actionContext.RequestContext.Principal.Identity.Name;
            Ipaddress = actionContext.Request.RequestUri.Host;
            ClaimsPrincipal claimsPrincipal = actionContext.RequestContext.Principal as ClaimsPrincipal;
            if (claimsPrincipal != null)
            {
                Role = claimsPrincipal.Claims.Where(n => n.Type != null && n.Type.Contains("role")).FirstOrDefault().Value;
                Sid = claimsPrincipal.Claims.Where(n => n.Type != null && n.Type.Contains("sid")).FirstOrDefault().Value;
            }
            RequestLog requestLog = new RequestLog()
            {
                ApiName = ActionName,
                ApiType = 1,
                ApiTypeName = "Action",
                RequestUserID = int.Parse(Sid),
                RequestIP = Ipaddress,
                RequestTime = DateTime.Now,
                IsSuccess = 0,
                Description = "请求成功"
            };
            db.RequestLogs.Add(requestLog);
            db.SaveChanges();
            //message = actionContext.Request.CreateErrorResponse(HttpStatusCode.Forbidden, new HttpError("API Request up to top limit"));
            //actionContext.Response = message;
        }
    }
}