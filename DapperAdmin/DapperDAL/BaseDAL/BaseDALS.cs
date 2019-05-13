using Dapper;
using DapperCommonMethod.CommonMethod;
using DapperHelp.Dapper;
using System;
using System.Collections.Generic;

namespace DapperDAL.BaseDAL
{
    /// <summary>
    /// 公共数据访问层
    /// </summary>
    public class BaseDALS
    {
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="tableName">表名</param>
        /// <param name="wherestr">条件</param>
        /// <param name="parametersp">参数</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="curPage">第几页</param>
        /// <param name="count">总行数</param>
        /// <returns></returns>
        public static List<T> PageListAdmin<T>(string tableName, string wherestr, DynamicParameters parametersp, int pageSize, int curPage, out int count)
        {
            string orderby = " ORDER BY CreateTime DESC ";
            string sqlpage = "SELECT * FROM (SELECT a.*, ROW_NUMBER() OVER ({0}) rownum FROM {2} as a  where {1} ) b WHERE b.rownum > @start AND b.rownum<= @end ORDER BY b.rownum";
            string countSql = "select count(1) from {0} where ";
            count = DapperHelps.ExecuteReaderReturnT<int>(string.Format(countSql, tableName) + wherestr, parametersp);

            parametersp.Add("@start", (curPage - 1) * pageSize);
            parametersp.Add("@end", curPage * pageSize);
            string sql = string.Format(sqlpage, orderby, wherestr, tableName);
            var list = DapperHelps.ExecuteReaderReturnList<T>(sql, parametersp);
            return list;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="array">Ids</param>
        /// <returns></returns>
        public static bool Delete(int[] array, string tableName)
        {
            if (array.Length <= 0) return false;
            var result = DapperHelps.ExecuteSqlInt(" DELETE " + tableName + " WHERE  ID in @ID ", new { tableme = tableName, ID = array });
            return result > 0;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="array">Ids</param>
        /// <returns></returns>
        public static bool Deletestr(string[] array, string tableName)
        {
            if (array.Length <= 0) return false;
            var result = DapperHelps.ExecuteSqlInt(" DELETE " + tableName + " WHERE  ID in @ID ", new { tableme = tableName, ID = array });
            return result > 0;
        }

        /// <summary>
        /// 新增操作(主键为Int类型)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public static long CreateInt<T>(T model) where T : class
        {
            try
            {
                if (model == null)
                {
                    return 0;
                }
                var id = DapperHelps.ExecuteInsert<T>(model);
                return id;
            }
            catch (Exception ex)
            {
                WriteLogMethod.WriteLogs(ex);
                return 0;
            }
        }

        /// <summary>
        /// 新增操作(主键为Guid)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string CreateGuid<T>(T model) where T : class
        {
            try
            {
                if (model == null)
                {
                    return String.Empty;
                }
                var id = DapperHelps.ExecuteInsertGuid<T>(model);
                return id;
            }
            catch (Exception ex)
            {
                WriteLogMethod.WriteLogs(ex);
                return String.Empty;
            }
        }
    }
}
