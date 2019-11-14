using Dapper.Contrib.Extensions;

namespace DapperModel.DataModel.DBPartModel.Sys_Model
{
    /// <summary>
    /// 管理角扩展字段
    /// </summary>
    public partial class Sys_ManagerRole
    {
        /// <summary>
        /// 角色类型Str
        /// </summary>
        [Computed]
        public string RoleTypeStr { get; set; }

        /// <summary>
        /// 添加人姓名
        /// </summary>
        [Computed]
        public string AddUserName { get; set; }

        /// <summary>
        /// 修改人姓名
        /// </summary>
        [Computed]
        public string UpdateUserName { get; set; }
    }
}
