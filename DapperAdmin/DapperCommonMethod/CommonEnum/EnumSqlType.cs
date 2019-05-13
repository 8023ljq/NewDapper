using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace DapperCommonMethod.CommonEnum
{
    /// <summary>
    /// 查询类型
    /// </summary>
    public enum EnumSqlType
    {
        /// <summary>
        /// 等于
        /// </summary>
        [Description("等于")]
        Equal = 1,

        /// <summary>
        /// 不等于
        /// </summary>
        [Description("不等于")]
        NoEqual = 2,

        /// <summary>
        /// 大于
        /// </summary>
        [Description("大于")]
        Big = 3,

        /// <summary>
        /// 大于等于
        /// </summary>
        [Description("大于等于")]
        BigEqual = 4,

        /// <summary>
        /// 小于
        /// </summary>
        [Description("小于")]
        Small = 5,

        /// <summary>
        /// 小于等于
        /// </summary>
        [Description("小于等于")]
        SmallEqual = 6,

        /// <summary>
        /// In
        /// </summary>
        [Description("In")]
        In = 7,

        /// <summary>
        /// like
        /// </summary>
        [Description("like")]
        Like = 8,

        /// <summary>
        /// Between...And...
        /// </summary>
        [Description("Between...And...")]
        BetweenAnd = 9,

        /// <summary>
        /// 等于Null
        /// </summary>
        [Description("等于Null")]
        IsNull = 10,

        /// <summary>
        /// 不等于Null
        /// </summary>
        [Description("不等于Null")]
        IsNoNull = 11,
    }
}
