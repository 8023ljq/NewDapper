using System;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Microsoft.Owin.Cors;
using Owin;

[assembly: OwinStartup(typeof(DapperAdminApi.App_Start.Startup))]

namespace DapperAdminApi.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //消息总线--集线器Hub配置
            app.Map("/MessageHub", map => {
                //SignalR允许跨域调用
                map.UseCors(CorsOptions.AllowAll);
                HubConfiguration config = new HubConfiguration()
                {
                    //禁用JavaScript代理
                    EnableJavaScriptProxies = false,
                    //启用JSONP跨域
                    EnableJSONP = true,
                    //反馈结果给客户端
                    EnableDetailedErrors = true
                };
                map.RunSignalR(config);
            });

            //WebApi允许跨域调用
            app.UseCors(CorsOptions.AllowAll);
        }
    }
}
