using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperModel.BuilderModel
{
    /// <summary>
    /// 数据库数据树结构
    /// </summary>
    public class DataTreeModel
    {
        /// <summary>
        /// 唯一ID
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// 上级ID
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string label { get; set; }

        /// <summary>
        /// 下级数据
        /// </summary>
        public List<DataTreeModel> children { get; set; }

    }
}
