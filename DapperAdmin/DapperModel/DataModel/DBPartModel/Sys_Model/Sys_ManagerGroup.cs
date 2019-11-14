using Dapper.Contrib.Extensions;
using System.Collections.Generic;

namespace DapperModel.DataModel
{
    /// <summary>
    /// 管理员组扩展类
    /// </summary>
    public partial class Sys_ManagerGroup
    {
        /// <summary>
        /// 添加人
        /// </summary>
        [Computed]
        public string AddName { get; set; }

        /// <summary>
        /// 子级菜单
        /// </summary>
        [Computed]
        public List<Sys_ManagerGroup> children { get; set; }
    }
}
