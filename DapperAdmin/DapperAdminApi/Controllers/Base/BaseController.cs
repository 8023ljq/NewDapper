using DapperCacheHelps.RedisHelper;
using DapperCommonMethod.CommonModel;
using DapperModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
                string strHostName = Dns.GetHostName(); //得到本机的主机名
                IPHostEntry ipEntry = Dns.GetHostByName(strHostName); //取得本机IP
                return ipEntry.AddressList[0].ToString();
            }
        }
    }
}
