using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace DapperAdminApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务

            // Web API 配置和服务
            //跨域配置
            var allowOrigins = ConfigurationManager.AppSettings["cors_allowOrigins"];
            var allowHeaders = ConfigurationManager.AppSettings["cors_allowHeaders"];
            var allowMethods = ConfigurationManager.AppSettings["cors_allowMethods"];
            var globalCors = new EnableCorsAttribute(allowOrigins, allowHeaders, allowMethods)
            {
                SupportsCredentials = true,
            };
            config.EnableCors(globalCors);

            // Web API 路由
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
