using System.ComponentModel;

namespace DapperCommonMethod.CommonEnum
{
    /// <summary>
    /// 角色类型枚举
    /// </summary>
    public enum RoleTypeEnum
    {
        /// <summary>
        /// 超级管理员
        /// </summary>
        [Description("超级管理员")]
        Administrator = 1,

        /// <summary>
        /// 系统管理员
        /// </summary>
        [Description("系统管理员")]
        System = 2,
    }
}
