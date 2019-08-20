﻿using DapperAdminApi.App_Start;
using DapperAdminApi.Common.Help;
using DapperBLL.Sys_BLL;
using DapperCommonMethod.CommonEnum;
using DapperModel.CommonModel;
using System.Web.Http;

namespace DapperAdminApi.Controllers.Competence
{
    /// <summary>
    /// Author：Geek Dog  Content：管理员角色接口 AddTime：2019-8-20 16:23:31  
    /// </summary>
    [ApiAuthorize]
    [RoutePrefix("api/manager")]
    public class ManagerRoleController : BaseController
    {
        private ManagerRoledBLL managerRoledBLL = new ManagerRoledBLL();

        /// <summary>
        /// 获取管理员角色下拉框列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getroleselectlist")]
        public IHttpActionResult GetRoleSelectList()
        {
            return Ok(ReturnHelp.ReturnSuccess(managerRoledBLL.GetRoleSelectList()));
        }

        /// <summary>
        /// 获取管理员角色列表
        /// </summary>
        /// <param name="pageModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getroleselectlist")]
        public IHttpActionResult GetManagerRoleList(PageModel pageModel)
        {
            return Ok(ReturnHelp.ReturnSuccess(managerRoledBLL.GetManagerRoleList(pageModel)));
        }
    }
}