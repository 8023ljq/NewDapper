using DapperAdminApi.App_Start;
using DapperBLL.Sys_BLL;
using DapperModel.CommonModel;
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
    }
}
