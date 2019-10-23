using DapperCacheHelps.RedisHelper;
using DapperCommonMethod.CommonMethod;
using DapperCommonMethod.CommonModel;
using DapperModel;
using Microsoft.AspNetCore.Rewrite.Internal.ApacheModRewrite;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web;
using System.Web.Http;

namespace DapperAdminApi.Controllers
{
    /// <summary>
    /// 公用控制器
    /// </summary>
    public class BaseController : ApiController
    {
        /// <summary>
        /// 查询排序条件
        /// </summary>
        public Dictionary<string, WhereModel> whereStr = new Dictionary<string, WhereModel>();
        public Dictionary<string, OrderByModel> orderByStr = new Dictionary<string, OrderByModel>();

        /// <summary>
        /// 缓存管理员信息
        /// </summary>
        public static RedisHelper redis = new RedisHelper();

        /// <summary>
        /// 获取token
        /// </summary>
        public string GetToken
        {
            get
            {
                return HttpContext.Current.Session["token"].ToString();
            }
        }

        /// <summary>
        /// 获取登录人信息
        /// </summary>
        /// <returns></returns>
        public Sys_Manager GetUserInfo()
        {
            return redis.StringGet<Sys_Manager>(GetToken);
        }

        /// <summary>
        /// 获取登录人主键ID
        /// </summary>
        public string GetUserId
        {
            get
            {
                return redis.StringGet<Sys_Manager>(GetToken).Id;
            }
        }

        /// <summary>
        /// 获取登录IP
        /// </summary>
        public string GetLoginIp
        {
            get
            {
                //string strHostName = Dns.GetHostName(); //得到本机的主机名
                //IPHostEntry ipEntry = Dns.GetHostByName(strHostName); //取得本机IP
                //return ipEntry.AddressList[0].ToString();
                string result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (String.IsNullOrEmpty(result))
                {
                    result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }

                if (String.IsNullOrEmpty(result))
                {
                    result = HttpContext.Current.Request.UserHostAddress;
                }
                return result;
            }
        }

        /// <summary>
        /// 获取真实IP地址
        /// </summary>
        public string GetIPAddress
        {
            get
            {
                var result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (!string.IsNullOrEmpty(result))
                {
                    //可能有代理  
                    if (result.IndexOf(".") == -1) //没有“.”肯定是非IPv4格式  
                    {
                        result = null;
                    }
                    else
                    {
                        if (result.IndexOf(",") != -1)
                        {
                            //有“,”，估计多个代理。取第一个不是内网的IP。  
                            result = result.Replace("  ", "").Replace("'", "");
                            string[] temparyip = result.Split(",;".ToCharArray());
                            for (int i = 0; i < temparyip.Length; i++)
                            {
                                if (RegexUtilsMethod.CheckIP(temparyip[i])
                                        && temparyip[i].Substring(0, 3) != "10."
                                        && temparyip[i].Substring(0, 7) != "192.168"
                                        && temparyip[i].Substring(0, 7) != "172.16.")
                                {
                                    return temparyip[i];        //找到不是内网的地址  
                                }
                            }
                        }
                        else if (RegexUtilsMethod.CheckIP(result))//代理即是IP格式
                        {
                            return result;
                        }
                        else
                        {
                            result = null;        //代理中的内容  非IP，取IP  
                        }
                    }
                }

                string IpAddress = (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null && HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != String.Empty) ? HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] : HttpContext.Current.Request.ServerVariables["HTTP_X_REAL_IP"];

                if (string.IsNullOrEmpty(result))
                {
                    result = HttpContext.Current.Request.ServerVariables["HTTP_X_REAL_IP"];
                }

                if (string.IsNullOrEmpty(result))
                {
                    result = HttpContext.Current.Request.UserHostAddress;
                }
                return result;
            }
        }
    }
}
