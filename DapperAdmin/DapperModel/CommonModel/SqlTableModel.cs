using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperModel.CommonModel
{
    /// <summary>
    /// 表内容实体
    /// </summary>
    public class SqlTableModel
    {
        /// <summary>
        /// 表名
        /// </summary>
        public string TableName { get; set; }

        private string tableremark;

        /// <summary>
        /// 表备注
        /// </summary>
        public string TableRemark
        {
            get { return tableremark; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    tableremark = TableName;
                }
                else
                {
                    tableremark = value;
                }
            }
        }
    }
}
