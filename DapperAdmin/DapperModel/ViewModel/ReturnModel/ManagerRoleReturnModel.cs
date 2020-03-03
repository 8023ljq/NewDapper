using DapperModel.DataModel;
using System.Collections.Generic;

namespace DapperModel.ViewModel
{
    /// <summary>
    /// 返回用户信息及角色列表实体
    /// </summary>
    public class ManagerRoleReturnModel
    {
        /// <summary>
        /// 用户信息
        /// </summary>
        public Sys_Manager ManagerModel { get; set; }

        /// <summary>
        /// 角色下拉框数据
        /// </summary>
        public List<SelectViewModel> RoleSelectViewList { get; set; }
    }
}
