using DapperModel.CommonModel;
using System.Collections.Generic;

namespace DapperDAL
{
    /// <summary>
    /// 生成数据数据逻辑层
    /// </summary>
    public class GenerateDataDAL : BaseDALS
    {
        /// <summary>
        /// 获取当前数据库所有表名
        /// </summary>
        /// <returns></returns>
        public List<SqlTableModel> GetNowTableName()
        {
            return GetList<SqlTableModel>($@"SELECT DISTINCT
                                      TableName = d.name,
                                      TableRemark = f.value
                                      FROM syscolumns a
                                      INNER JOIN sysobjects d ON a.id = d.id AND d.xtype = 'U' AND d.name <> 'dtproperties'
                                      LEFT JOIN sys.extended_properties f ON d.id = f.major_id AND f.minor_id = 0");
        }

        /// <summary>
        /// 获取当前所有表的表名,字段名,字段属性,字段注释
        /// </summary>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public List<SqlColumnModel> GetTableInfo(string TableName)
        {
            string sql = $@"SELECT
                            ColumnTableName=d.name,
                            ColumnName=a.name,
                            ColumnType=b.name,
                            ColumnRemark=isnull(g.[value],'')
                            FROM syscolumns a
                            left join systypes b on a.xusertype=b.xusertype
                            inner join sysobjects d on a.id=d.id and d.xtype='U' and d.name<>'dtproperties'
                            left join sys.extended_properties g on a.id=g.major_id and a.colid=g.minor_id
                            --where d.name=@TableName
                            order by a.id,a.colorder";

            return GetList<SqlColumnModel>(sql, null, new { TableName = TableName });
        }
    }
}
