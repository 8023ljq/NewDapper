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
            MenuBLL menuBLL = new MenuBLL();

            Sys_Menu menu = new Sys_Menu()
            {
                Id = Guid.NewGuid().ToString(),
                ParentId = "0",
                FullName = "测试菜单",
                Layers = 1,
                IconUrl = "",
                AddressUrl = "",
                Sort = 12,
                Purview = "",
                IsShow = true,
                IsDefault = true,
                AddUserId = "",
                AddTime = DateTime.Now,
                UpdateUserId = "",
                UpdateTime = DateTime.Now,
                IsDelete = false,
                Remarks = "",
            };

            menuBLL.InsertModelGuid(menu);

            return Ok();
        }

    }
}
