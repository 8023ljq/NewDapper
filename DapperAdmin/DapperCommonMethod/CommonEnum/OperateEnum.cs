using System.ComponentModel;

namespace DapperCommonMethod.CommonEnum
{
    /// <summary>
    /// 操作类型枚举
    /// </summary>
    public enum OperateEnum
    {
        /// <summary>
        /// 新增
        /// </summary>
        [Description("新增")]
        Add=1,

        /// <summary>
        /// 删除
        /// </summary>
        [Description("删除")]
        Delete=2,

        /// <summary>
        /// 修改
        /// </summary>
        [Description("修改")]
        Update=3,
    }
}
