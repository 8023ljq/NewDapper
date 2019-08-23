using System.Collections.Generic;

namespace DapperModel.CodeGenerateModel
{
    /// <summary>
    /// 查询数据库表的树形结构
    /// </summary>
    public class DataBaseTable
    {
        /// <summary>
        /// 自增标识
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 库名或表名
        /// </summary>
        public string label { get; set; }

        /// <summary>
        /// 子级数据集合
        /// </summary>
        public List<DataBaseTable> children { get; set; }
    }
}
