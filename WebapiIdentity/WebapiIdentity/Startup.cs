using System;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Microsoft.Owin.Security.OAuth;
using Owin;
using WebapiIdentity.Models;
using System.Linq;

[assembly: OwinStartup(typeof(WebapiIdentity.Startup))]

namespace WebapiIdentity
{
    public class Startup
    {
        public static OAuthAuthorizationServerOptions OAuthOptions { get; private set; }
        public void Configuration(IAppBuilder app)
        {
            //web.api注册token生成路由
            HttpConfiguration config = new HttpConfiguration();
            ConfigOAuth(app);
            WebApiConfig.Register(config);
            app.UseCors(CorsOptions.AllowAll);
        }

        public void ConfigOAuth(IAppBuilder app)
        {
            OAuthOptions = new OAuthAuthorizationServerOptions()
            {
                TokenEndpointPath = new PathString("/token"),
                Provider = new OAuthAuthorizationServerProvider()
                {
                    //带有正常可用Token  
                    OnValidateClientAuthentication = async (context) =>
                    {
                        await Task.Run(() =>
                        {
                            context.Validated();
                        });
                    },
                    //请求Token验证
                    OnGrantResourceOwnerCredentials = async (context) =>
                    {
                        //验证用户名密码
                        //模拟固定用户名密码测试验证，也可通过数据库中的用户名密码进行验证
                        //if (context.UserName == "Reiko" && context.Password == "123")
                        await Task.Run(() =>
                        {
                            //允许所有域名都可以访问
                            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
                            if (OauthValidTools.CheckUser(context, out User user))
                            {
                                ClaimsIdentity oAuthIdentity = new ClaimsIdentity(context.Options.AuthenticationType);
                                //oAuthIdentity.AddClaim(new Claim(ClaimTypes.Name, context.UserName));
                                oAuthIdentity.AddClaim(new Claim(ClaimTypes.Sid, user.Uid.ToString()));
                                oAuthIdentity.AddClaim(new Claim(ClaimTypes.Role, "admin"));
                                context.Validated(oAuthIdentity);
                            }
                        });
                    }
                },
                AllowInsecureHttp = true,
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(10)
            };
            //使应用程序能够使用承载令牌对用户进行身份验证
            app.UseOAuthBearerTokens(OAuthOptions);
        }


    }
}
