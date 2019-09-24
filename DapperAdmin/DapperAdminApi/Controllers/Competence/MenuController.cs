﻿using DapperAdminApi.App_Start;
using DapperBLL.Sys_BLL;
using System.Web.Http;

namespace DapperAdminApi.Controllers.Competence
{
    /// <summary>
    /// Author：Geek Dog  Content：菜单控制器 AddTime：2019-8-20 16:42:30  
    /// </summary>
    [ApiAuthorize]
    [RoutePrefix("api/menu")]
    public class MenuController : BaseController
    {
        private MenuBLL menuBLL = new MenuBLL();

        /// <summary>
        /// Author：Geek Dog  Content：获取单个菜单信息 AddTime：2019-8-21 9:23:54  
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getmenumodel")]
        public IHttpActionResult GetMenuModel(string guid)
        {
            return Ok(menuBLL.GetMenuModel(guid));
        }

        /// <summary>
        /// Author：Geek Dog  Content：获取菜单列表 AddTime：2019-5-29 9:21:15  
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getmenulist")]
        public IHttpActionResult GetMenuList()
        {
            string NowUserRoleId = GetUserInfo().Id;
            return Ok(menuBLL.GetMenuList());
        }
    }
}
