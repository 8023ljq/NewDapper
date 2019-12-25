using DapperCommonMethod.CommonConfig;
using DapperCommonMethod.CommonMethod;
using DapperModel.DataModel;
using DapperThirdHelps.RedisHelper;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
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
        /// 缓存管理员信息
        /// </summary>
        public static RedisHelper redis = new RedisHelper(AppSettingsConfig.RedisUserDB);

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
                                if (temparyip[i].CheckIP()
                                        && temparyip[i].Substring(0, 3) != "10."
                                        && temparyip[i].Substring(0, 7) != "192.168"
                                        && temparyip[i].Substring(0, 7) != "172.16.")
                                {
                                    return temparyip[i];        //找到不是内网的地址  
                                }
                            }
                        }
                        else if (result.CheckIP())//代理即是IP格式
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

        /// <summary>
        /// 处理导出数据公用方法
        /// </summary>
        /// <param name="byteData"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public HttpResponseMessage GetHttpResponseMessage(byte[] byteData, string fileName)
        {
            HttpResponseMessage httpResponseMessage = new HttpResponseMessage();
            Stream stream = new MemoryStream(byteData);
            httpResponseMessage.Content = new StreamContent(stream);
            httpResponseMessage.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            httpResponseMessage.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
            {
                FileName = HttpUtility.UrlEncode(fileName + ".xls")
            };
            return httpResponseMessage;
        }
    }
}
