using DapperAdminApi.Common.SignalR;
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
            // 有关如何配置应用程序的详细信息，请访问 https://go.microsoft.com/fwlink/?LinkID=316888
            app.Map("/messageHub", map =>
            {
                map.UseCors(CorsOptions.AllowAll);
                var hubConfiguration = new HubConfiguration
                {
                    EnableJSONP = true,//跨域的关键语句
                    EnableJavaScriptProxies = false,
                    EnableDetailedErrors = true
                };
                map.RunSignalR(hubConfiguration);
            });
            app.MapSignalR();
            app.UseCors(CorsOptions.AllowAll);
        }
    }
}
