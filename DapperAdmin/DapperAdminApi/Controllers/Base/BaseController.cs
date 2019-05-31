using DapperCacheHelps.RedisHelper;
using DapperCommonMethod.CommonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
    }
}
