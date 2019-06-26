using Dapper;
using DapperCommonMethod.CommonMethod;
using DapperCommonMethod.CommonModel;
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

        DapperHelps dapperHelps = new DapperHelps();


        #region 增

        /// <summary>
        /// 新增操(主键为Int类型)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public long InsertModelInt<T>(T model) where T : class
        {
            try
            {
                if (model == null)
                {
                    return 0;
                }
                var id = dapperHelps.ExecuteInsert<T>(model);
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
        public string InsertModelGuid<T>(T model) where T : class
        {
            try
            {
                if (model == null)
                {
                    return String.Empty;
                }
                var id = dapperHelps.ExecuteInsertGuid<T>(model);
                return id;
            }
            catch (Exception ex)
            {
                WriteLogMethod.WriteLogs(ex);
                return String.Empty;
            }
        }

        /// <summary>
        /// 添加集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="modelList"></param>
        /// <returns></returns>
        public bool InsertList<T>(List<T> modelList) where T : class
        {
            try
            {
                if (modelList.Count <= 0)
                {
                    return false;
                }
                dapperHelps.ExecuteInsertList<T>(modelList);
                return true;
            }
            catch (Exception ex)
            {
                WriteLogMethod.WriteLogs(ex);
                return false;
            }
        }

        #endregion

        #region 删

        /// <summary>
        /// 根据主键删除(主键为int类型)
        /// </summary>
        /// <param name="array">删除主键数组集合</param>
        /// <returns></returns>
        public bool DeleteIntId<T>(int[] array)
        {
            if (array.Length <= 0)
            {
                return false;
            }
            var result = dapperHelps.ExecuteSqlInt(" DELETE @tableme WHERE Id in @ID ", new { tableme = typeof(T).Name.ToString(), ID = array });
            return result > 0;
        }

        /// <summary>
        /// 根据主键删除(主键为string类型)
        /// </summary>
        /// <param name="array">删除主键数组集合</param>
        /// <returns></returns>
        public bool DeleteStringId<T>(string[] array)
        {
            if (array.Length <= 0)
            {
                return false;
            }
            var result = dapperHelps.ExecuteSqlInt(" DELETE @tableme WHERE Id in @ID ", new { tableme = typeof(T).Name.ToString(), ID = array });
            return result > 0;
        }

        #endregion

        #region 改

        /// <summary>
        /// 修改单个实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool UpdateModel<T>(T model) where T : class
        {
            try
            {
                if (model == null)
                {
                    return false;
                }
                return dapperHelps.ExecuteUpdate<T>(model);
            }
            catch (Exception ex)
            {
                WriteLogMethod.WriteLogs(ex);
                return false;
            }
        }

        #endregion

        #region 查

        /// <summary>
        /// 通过主键查询实体(int类型主键)
        /// </summary>
        /// <typeparam name="T">泛型实体类</typeparam>
        /// <param name="Id">主键id</param>
        /// <returns></returns>
        public T GetModelById<T>(int Id) where T : class
        {
            string sqlstr = string.Format("select * from {0} where Id=@ID", typeof(T).Name.ToString());
            return dapperHelps.ExecuteReaderReturnT<T>(sqlstr, new { ID = Id });
        }

        /// <summary>
        /// 通过主键查询实体(string类型主键)
        /// </summary>
        /// <typeparam name="T">泛型实体类</typeparam>
        /// <param name="Id">主键id</param>
        /// <returns></returns>
        public T GetModelById<T>(string Id) where T : class
        {
            string sqlstr = string.Format("select * from {0} where Id=@ID", typeof(T).Name.ToString());
            return dapperHelps.ExecuteReaderReturnT<T>(sqlstr, new { ID = Id });
        }

        /// <summary>
        /// 获取单个实体(条件查询)
        /// </summary>
        /// <typeparam name="T">泛型实体类</typeparam>
        /// <param name="whereStr">查询条件</param>
        /// <param name="orderByStr">排序条件</param>
        /// <returns></returns>
        public T GetModel<T>(Dictionary<string, WhereModel> whereStr, Dictionary<string, OrderByModel> orderByStr)
        {
            string sqlstr = string.Format("select * from {0}", typeof(T).Name.ToString());
            DynamicParameters parameters = new DynamicParameters();
            sqlstr = DapperSpliceCondition.GetWhereStr(sqlstr, whereStr, orderByStr, out parameters);
            return dapperHelps.ExecuteReaderReturnT<T>(sqlstr, parameters);
        }

        /// <summary>
        /// 获取集合对象
        /// </summary>
        /// <typeparam name="T">泛型实体</typeparam>
        /// <param name="whereStr">查询条件</param>
        /// <param name="orderByStr">排序条件</param>
        /// <returns></returns>
        public List<T> GetList<T>(Dictionary<string, WhereModel> whereStr, Dictionary<string, OrderByModel> orderByStr)
        {
            string sqlstr = string.Format("select * from {0}", typeof(T).Name.ToString());
            DynamicParameters parameters = new DynamicParameters();
            sqlstr = DapperSpliceCondition.GetWhereStr(sqlstr, whereStr, orderByStr, out parameters);
            return dapperHelps.ExecuteReaderReturnList<T>(sqlstr, parameters);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T">泛型实体</typeparam>
        /// <param name="wherestr">条件</param>
        /// <param name="parametersp">参数</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="curPage">第几页</param>
        /// <param name="count">总行数</param>
        /// <returns></returns>
        public List<T> PageListAdmin<T>(string wherestr, DynamicParameters parametersp, int pageSize, int curPage, out int count)
        {
            string orderby = " ORDER BY CreateTime DESC ";
            string sqlpage = "SELECT * FROM (SELECT a.*, ROW_NUMBER() OVER ({0}) rownum FROM {2} as a  where {1} ) b WHERE b.rownum > @start AND b.rownum<= @end ORDER BY b.rownum";
            string countSql = "select count(1) from {0} where ";
            count = dapperHelps.ExecuteReaderReturnT<int>(string.Format(countSql, typeof(T).Name.ToString()) + wherestr, parametersp);

            parametersp.Add("@start", (curPage - 1) * pageSize);
            parametersp.Add("@end", curPage * pageSize);
            string sql = string.Format(sqlpage, orderby, wherestr, typeof(T).Name.ToString());
            var list = dapperHelps.ExecuteReaderReturnList<T>(sql, parametersp);
            return list;
        }

        #endregion

    }
}
