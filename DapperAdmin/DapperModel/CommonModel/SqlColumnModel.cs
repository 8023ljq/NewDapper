namespace DapperModel.CommonModel
{
    /// <summary>
    /// 数据库table列信息
    /// </summary>
    public class SqlColumnModel
    {
        /// <summary>
        /// 表名
        /// </summary>
        public string ColumnTableName { get; set; }

        /// <summary>
        /// 列名
        /// </summary>
        public string ColumnName { get; set; }

        private string columnremark;
        /// <summary>
        /// 备注
        /// </summary>
        public string ColumnRemark
        {
            get { return columnremark; }
            set
            {
                if (string.IsNullOrEmpty(value))
                    columnremark = ColumnName;
                else
                    columnremark = value;
            }
        }
        /// <summary>
        /// 列类型
        /// </summary>
        public string ColumnType { get; set; }
    }
}
