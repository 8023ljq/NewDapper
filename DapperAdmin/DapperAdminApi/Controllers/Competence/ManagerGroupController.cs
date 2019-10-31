using DapperAdminApi.App_Start;
using DapperBLL.Sys_BLL;
using DapperModel.CommonModel;
using DapperModel.ViewModel.RequestModel;
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
        /// 添加用户组
        /// </summary>
        /// <param name="ManagerGroup">添加组实体类</param>
        /// <returns></returns>
        [HttpPost]
        [Route("addmanagergroup")]
        public IHttpActionResult AddManagerGroup(AddManagerGroupRequest ManagerGroup)
        {
            ManagerGroup.AddUserId = GetUserId;
            return Ok(managerGroupBLL.AddManagerGroup(ManagerGroup));
        }

        /// <summary>
        /// 获取用户组
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getmanagergroup")]
        public IHttpActionResult GetManagerGroup(string groupid)
        {
            return Ok(managerGroupBLL.GetManagerGroup(groupid));
        }

        /// <summary>
        /// 修改用户组信息
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("updatemanagergroup")]
        public IHttpActionResult UpdateManagerGroup(AddManagerGroupRequest managerGroup)
        {
            managerGroup.AddUserId = GetUserId;
            return Ok(managerGroupBLL.UpdateManagerGroup(managerGroup));
        }

        /// <summary>
        /// 删除用户组信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("deletemanagergroup")]
        public IHttpActionResult DeleteManagerGroup(string groupid)
        {
            return Ok(managerGroupBLL.DeleteManagerGroup(groupid));
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
    }
}
