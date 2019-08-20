using DapperAdminApi.Models.RequestModel;
using DapperBLL.Sys_BLL;
using DapperCacheHelps.RedisHelper;
using DapperHelp.Dapper;
using DapperModel;
using System;
using System.Collections.Generic;
using System.Net;
using System.Web;
using System.Web.Http;

namespace DapperAdminApi.Controllers.Text
{
    /// <summary>
    /// 测试控制器
    /// </summary>
    [RoutePrefix("v1/api/text")]
    public class TextController : ApiController
    {
        /// <summary>
        /// 缓存管理员信息
        /// </summary>
        public static RedisHelper redis = new RedisHelper();

        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("addmenu")]
        public IHttpActionResult AddMenu()
        {
            ManagerdBLL managerdBLL = new ManagerdBLL();

            string Key = "5D929524E066B8EEBAB756364775AACC4F0F16B5A5E5FD8DE970A0F0F2AE4E42A5274795E5C2684D";

            Sys_Manager managerModel = managerdBLL.GetModelAll<Sys_Manager>("Id=@Id", new { Id = "524eed52-1a33-40ca-9a70-1c621c8d2640" });

            managerModel.IsLocking = false;

            redis.StringSet(Key, managerModel, TimeSpan.FromMinutes(30));


            return Ok();
        }

    }
}
