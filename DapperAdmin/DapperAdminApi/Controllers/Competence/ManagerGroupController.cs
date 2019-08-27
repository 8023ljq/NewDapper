using DapperAdminApi.App_Start;
using DapperBLL.Sys_BLL;
using DapperModel.CommonModel;
using DapperModel.ViewModel.RequestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DapperAdminApi.Controllers.Competence
{
    /// <summary>
    /// 管理员组控制器
    /// </summary>
    [ApiAuthorize]
    [RoutePrefix("api/managergroup")]
    public class ManagerGroupController : BaseController
    {
        private ManagerGroupBLL managerGroupBLL = new ManagerGroupBLL();

        /// <summary>
        /// 获取所有用户组数据
        /// </summary>
        /// <param name="selectModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getmanagergrouplist")]
        public IHttpActionResult GetManagerGroupList(SelectModel selectModel)
        {
            return Ok(managerGroupBLL.GetManagerGroupList(selectModel));
        }

        /// <summary>
        /// 获取用户组下拉框列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getgroupselectlist")]
        public IHttpActionResult GetGroupSelectList()
        {
            return Ok(managerGroupBLL.GetGroupSelectList());
        }

        /// <summary>
        /// 添加用户组
        /// </summary>
        /// <param name="ManagerGroup">添加组实体类</param>
        /// <returns></returns>
        [HttpPost]
        [Route("addmanagergroup")]
        public IHttpActionResult AddManagerGroup(AddManagerGroupRequest ManagerGroup)
        {
            return Ok(managerGroupBLL.AddManagerGroup(ManagerGroup));
        }
    }
}
