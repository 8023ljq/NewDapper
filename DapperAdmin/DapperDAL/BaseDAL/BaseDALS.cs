using Dapper;
using DapperCommonMethod.CommonMethod;
using DapperCommonMethod.CommonModel;
using DapperHelp.Dapper;
using DapperModel.CommonModel;
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
                return dapperHelps.UpdateModel<T>(model);
            }
            catch (Exception ex)
            {
                WriteLogMethod.WriteLogs(ex);
                return false;
            }
        }

        /// <summary>
        /// 批量更新实体,返回更新状态
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="List"></param>
        /// <returns></returns>
        public bool UpdateList<T>(List<T> List) where T : class
        {
            try
            {
                if (List.Count <= 0)
                {
                    return false;
                }
                return dapperHelps.ExecuteUpdateList<T>(List);
            }
            catch (Exception ex)
            {
                WriteLogMethod.WriteLogs(ex);
                return false;
            }
        }

        /// <summary>
        /// 批量修改返回成功和失败的条数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="List"></param>
        /// <param name="ErrorCount"></param>
        /// <returns></returns>
        public int UpdateList<T>(List<T> List, out int ErrorCount) where T : class
        {
            try
            {
                if (List.Count <= 0)
                {
                    ErrorCount = 0;
                    return 0;
                }
                return dapperHelps.ExecuteUpdateList<T>(List, out ErrorCount);
            }
            catch (Exception ex)
            {
                WriteLogMethod.WriteLogs(ex);
                ErrorCount = 0;
                return 0;
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
        /// 
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
        /// 获取集合对象
        /// </summary>
        /// <typeparam name="T">泛型实体</typeparam>
        /// <param name="whereStr">查询条件</param>
        /// <param name="orderByStr">排序条件</param>
        /// <returns></returns>
        public List<T> GetList<T>(string whereStr,object parameter)
        {
            string sqlstr = string.Format("select * from {0} where {1}", typeof(T).Name.ToString(), whereStr);
          
            return dapperHelps.ExecuteReaderReturnList<T>(sqlstr, parameter);
        }
        
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T">泛型实体</typeparam>
        /// <param name="wherestr">查询条件</param>
        /// <param name="orderbystr">排序条件</param>
        /// <param name="parametersp">参数</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="curPage">第几页</param>
        /// <param name="count">总行数</param>
        /// <returns></returns>
        public List<T> GetPageList<T>(string wherestr, PageModel pageModel, string orderbystr = null)
        {
            DynamicParameters parametersp = new DynamicParameters();
            string orderby = String.Empty;
            if (String.IsNullOrEmpty(orderbystr))
            {
                orderby = " ORDER BY AddTime DESC ";
            }
            else
            {
                orderby = $@" ORDER BY {orderbystr} DESC ";
            }

            string sqlpage = "SELECT * FROM (SELECT A.*, ROW_NUMBER() OVER ({0}) rownum FROM {2} as A  where {1} ) Z WHERE Z.rownum > @start AND Z.rownum<= @end ORDER BY Z.rownum";
            string countSql = "select count(1) from {0} where {1}";

            parametersp.Add("@start", (pageModel.curPage - 1) * pageModel.pageSize);
            parametersp.Add("@end", pageModel.curPage * pageModel.pageSize);

            pageModel.count = dapperHelps.ExecuteReaderReturnT<int>(string.Format(countSql, typeof(T).Name.ToString(), wherestr), parametersp);

            string sql = string.Format(sqlpage, orderby, wherestr, typeof(T).Name.ToString());
            var list = dapperHelps.ExecuteReaderReturnList<T>(sql, parametersp);
            return list;
        }

        /// <summary>
        /// 连接查询分页方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sqlstr"></param>
        /// <param name="pageModel"></param>
        /// <param name="orderbystr">排序条件需要带表名</param>
        /// <returns></returns>
        public List<T> GetPageJoinList<T>(string sqlstr, PageModel pageModel, string orderbystr = null)
        {
            DynamicParameters parametersp = new DynamicParameters();
            List<T> List = new List<T>();
            string numberStr = String.Empty;

            pageModel.count = dapperHelps.ExecuteReaderReturnList<T>(sqlstr).Count;

            if (String.IsNullOrEmpty(orderbystr))
            {
                numberStr = $@" , ROW_NUMBER() OVER (ORDER BY A.AddTime DESC) rownum from ";
            }
            else
            {
                numberStr = $@", ROW_NUMBER() OVER (ORDER BY {orderbystr} DESC) rownum from ";
            }

            sqlstr = sqlstr.ToUpper();

            if (!sqlstr.Contains("FROM"))
            {
                return List;
            }
            string[] sqlArry = sqlstr.Split(new string[] { "FROM" }, StringSplitOptions.RemoveEmptyEntries);

            sqlstr = sqlArry[0] + numberStr + sqlArry[1];
            string sqlpage = string.Format("SELECT * FROM ( {0}) Z WHERE Z.rownum > @start AND Z.rownum<= @end ORDER BY Z.rownum", sqlstr);

            parametersp.Add("@start", (pageModel.curPage - 1) * pageModel.pageSize);
            parametersp.Add("@end", pageModel.curPage * pageModel.pageSize);

            List = dapperHelps.ExecuteReaderReturnList<T>(sqlpage, parametersp);
            return List;
        }

        #endregion

    }
}
