using System.Collections.Generic;

namespace DapperCommonMethod.CommonModel
{
    /// <summary>
    /// 查询条件
    /// </summary>
    public class WhereModel
    {
        /// <summary>
        /// 字段名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 查询方式
        /// </summary>
        public int InquireManner { get; set; }

        /// <summary>
        /// 查询内容
        /// </summary>
        public string Content { get; set; }
    }

    /// <summary>
    /// 排序条件
    /// </summary>
    public class OrderByModel
    {
        /// <summary>
        /// 字段名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 排序方式
        /// </summary>
        public string Sort { get; set; }
    }

    public class WhereModelList
    {
        /// <summary>
        /// 查询方式
        /// </summary>
        public int InquireManner { get; set; }

        /// <summary>
        /// 参数集合
        /// </summary>
        public List<Parameter> ContentList { get; set; }
    }

    /// <summary>
    /// 查询参数
    /// </summary>
    public class Parameter
    {
        /// <summary>
        /// 字段名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 参数
        /// </summary>
        public string Content { get; set; }
    }
}
