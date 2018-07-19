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
        public static bool CheckUser(OAuthGrantResourceOwnerCredentialsContext context)
        {
            string UserName = context.UserName.Trim();
            string Password = context.Password.Trim();
            bool isPass = false;
            if (string.IsNullOrWhiteSpace(UserName) || string.IsNullOrWhiteSpace(Password))
            {
                context.SetError("invalid_grant", "用户名或密码不能为空！");
                return isPass;
            }
            try
            {
                User user = db.User.Where(n => n.Phone == UserName || n.Email == UserName || n.LoginName == UserName).FirstOrDefault();
                if (user != null)
                {
                    if (user.PassWord == Password)
                    {
                        return true;
                    }
                    else
                    {
                        context.SetError("invalid_grant", "密码错误！");
                    }
                }
                else
                {
                    context.SetError("invalid_grant", "不存在此用户！");
                }
                return isPass;
            }
            catch (Exception ex)
            {
                context.SetError("invalid_Error", "服务端校验出错！");
                return isPass;
            }

        }
    }
}