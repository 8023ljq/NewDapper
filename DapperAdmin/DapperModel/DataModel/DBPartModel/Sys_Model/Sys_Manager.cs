using Dapper.Contrib.Extensions;

namespace DapperModel.DataModel
{
    /// <summary>
    /// 管理员扩展类
    /// </summary>
    public partial class Sys_Manager
    {
        /// <summary>
        /// 角色名字    
        /// </summary>
        [Computed]
        public string RoleName { get; set; }
    }
}
