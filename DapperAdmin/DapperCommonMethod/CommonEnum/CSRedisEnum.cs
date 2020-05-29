using System.ComponentModel;

namespace DapperCommonMethod.CommonEnum
{
    /// <summary>
    /// CSRedis连接枚举值
    /// </summary>
    public enum CSRedisEnum
    {
        /// <summary>
        /// 超级管理员
        /// </summary>
        [Description("超级管理员")]
        Administrator = 0,

        [Description("数据库连接配置")]
        Common = 12,
    }
}
