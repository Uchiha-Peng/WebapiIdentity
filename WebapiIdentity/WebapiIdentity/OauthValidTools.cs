using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebapiIdentity.Models;

namespace WebapiIdentity
{
    /// <summary>
    /// Oauth2.0校验用户名密码
    /// </summary>
    public class OauthValidTools
    {
        private static ShopDBContext db = new ShopDBContext();

        /// <summary>
        /// 校验用户名密码
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static bool CheckUser(OAuthGrantResourceOwnerCredentialsContext context, out User loginUser)
        {
            string UserName = context.UserName.Trim();
            string Password = context.Password.Trim();
            bool isPass = false;
            if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(Password))
            {
                context.SetError("invalid_grant", "用户名或密码不能为空！");
                loginUser = null;
                return isPass;
            }
            try
            {
                User user = db.User.Where(n => n.Phone == UserName || n.Email == UserName || n.LoginName == UserName).FirstOrDefault();
                if (user != null)
                {
                    RequestLog requestLog = new RequestLog()
                    {
                        ApiName = context.Request.Uri.AbsolutePath,
                        ApiType = 0,
                        ApiTypeName = "Token",
                        RequestUserID = user.Uid,
                        RequestIP = context.Request.Uri.Host,
                        RequestTime = DateTime.Now
                    };
                    if (user.PassWord == Password)
                    {
                        requestLog.IsSuccess = 0;
                        requestLog.Description = "请求Token成功";
                        loginUser = user;
                        db.RequestLogs.Add(requestLog);
                        db.SaveChanges();
                        return true;
                    }
                    else
                    {
                        requestLog.IsSuccess = 1;
                        requestLog.Description = "密码错误！";
                        context.SetError("invalid_grant", "密码错误！");
                        db.RequestLogs.Add(requestLog);
                        db.SaveChanges();
                    }
                }
                else
                {
                    context.SetError("invalid_grant", "不存在此用户！");
                }
            }
            catch (Exception)
            {
                context.SetError("invalid_Error", "服务端校验出错！");
            }
            loginUser = null;
            return isPass;
        }


    }
}