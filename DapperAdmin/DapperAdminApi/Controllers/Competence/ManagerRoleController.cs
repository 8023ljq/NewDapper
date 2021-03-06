﻿using DapperAdminApi.App_Start;
using DapperBLL;
using DapperModel.CommonModel;
using DapperModel.ViewModel.RequestModel;
using System.Web.Http;

namespace DapperAdminApi.Controllers.Competence
{
    /// <summary>
    /// Author：Geek Dog  Content：管理员角色接口 AddTime：2019-8-20 16:23:31  
    /// </summary>
    [ApiAuthorize]
    [RoutePrefix("api/managerrole")]
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
            var model = GetUserInfo();
            return Ok(managerRoledBLL.GetRoleSelectList(GetUserInfo()));
        }

        /// <summary>
        /// 获取管理员角色列表
        /// </summary>
        /// <param name="pageModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("getmanagerrolelist")]
        public IHttpActionResult GetManagerRoleList(SelectModel selectModel)
        {
            return Ok(managerRoledBLL.GetManagerRoleList(selectModel));
        }

        /// <summary>
        /// 添加角色信息
        /// </summary>
        /// <param name="addRoleRequestModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("addnewrole")]
        public IHttpActionResult AddNewRole(AddRoleRequest addRoleRequestModel)
        {
            var UserModel = GetUserInfo();
            return Ok(managerRoledBLL.AddNewRole(addRoleRequestModel, UserModel));
        }

        /// <summary>
        /// 获取当前角色信息
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("selectrolemodel")]
        public IHttpActionResult SelectRoleModel(string roleId)
        {
            return Ok(managerRoledBLL.SelectRoleModel(roleId));
        }

        /// <summary>
        /// 修改当前角色信息
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("updatenowrole")]
        public IHttpActionResult UpdateNowRole(AddRoleRequest UpdateRoleRequestModel)
        {
            var UserModel = GetUserInfo();
            return Ok(managerRoledBLL.UpdateNowRole(UpdateRoleRequestModel, UserModel));
        }

        /// <summary>
        /// 修改当前角色权限信息
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("updatenowpurview")]
        public IHttpActionResult UpdateNowPurview(AddRoleRequest UpdateRoleRequestModel)
        {
            var UserModel = GetUserInfo();
            return Ok(managerRoledBLL.UpdateNowPurview(UpdateRoleRequestModel, UserModel));
        }

        /// <summary>
        /// 停用/启用当前角色
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("enableordisablerole")]
        public IHttpActionResult EnableOrDisableRole(string roleId)
        {
            return Ok(managerRoledBLL.EnableOrDisableRole(roleId, GetUserInfo()));
        }

        /// <summary>
        /// 删除当前角色信息
        /// </summary>
        /// <param name="roleId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("deletenowrole")]
        public IHttpActionResult DeleteNowRole(string roleId)
        {
            return Ok(managerRoledBLL.DeleteNowRole(roleId, GetUserInfo()));
        }
    }
}
