using System.ComponentModel;

namespace DapperCommonMethod.CommonEnum
{
    /// <summary>
    /// 生成随机数枚举
    /// </summary>
    public enum RandNumEnum
    {
        /// <summary>
        /// 纯数字
        /// </summary>
        [Description("纯数字")]
        Number = 0,

        /// <summary>
        /// 数字加字母(大小写混合)
        /// </summary>
        [Description("数字加字母(大小写混合)")]
        NumberAndLetter=1,
    }
}
