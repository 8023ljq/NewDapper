using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using System.Web.SessionState;

namespace DapperAdminApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            var json = GlobalConfiguration.Configuration.Formatters.JsonFormatter;
            json.SerializerSettings.DateTimeZoneHandling = Newtonsoft.Json.DateTimeZoneHandling.Local;
            json.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            json.SerializerSettings.DateFormatHandling = Newtonsoft.Json.DateFormatHandling.MicrosoftDateFormat;
            //settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }

        public override void Init()
        {
            this.PostAuthenticateRequest += (sender, e) => HttpContext.Current.SetSessionStateBehavior(SessionStateBehavior.Required);
            base.Init();
        }
    }
}
