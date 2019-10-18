using System.ComponentModel;

namespace DapperCommonMethod.CommonEnum
{
    /// <summary>
    /// 菜单类型枚举
    /// </summary>
    public enum ResourceTypeEnum
    {
        /// <summary>
        /// 菜单
        /// </summary>
        [Description("菜单")]
        Menu=0,

        /// <summary>
        /// 按钮
        /// </summary>
        [Description("按钮")]
        Button = 1,
    }
}
