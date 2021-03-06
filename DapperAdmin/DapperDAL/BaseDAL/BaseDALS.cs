﻿using Dapper;
using DapperCommonMethod.CommonMethod;
using DapperHelp.Dapper;
using DapperModel.CommonModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DapperDAL
{
    /// <summary>
    /// 公共数据访问层
    /// </summary>
    public class BaseDALS
    {
        public DapperHelps dapperHelps = new DapperHelps();

        public DynamicParameters parametersp = new DynamicParameters();

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

            string sqlstr = string.Format("DELETE {0} WHERE Id in @ID ", typeof(T).Name.ToString());
            parametersp.Add("@ID", array);

            var result = dapperHelps.ExecuteSqlInt(sqlstr, parametersp);
            return result > 0;
        }

        /// <summary>
        /// 根据主键删除(主键为GUID类型)
        /// </summary>
        /// <param name="array">删除主键数组集合</param>
        /// <returns></returns>
        public bool DeleteStringId<T>(string[] array)
        {
            if (array.Length <= 0)
            {
                return false;
            }
            string sqlstr = string.Format("DELETE {0} WHERE Id in @ID ", typeof(T).Name.ToString());
            parametersp.Add("@ID", array);

            var result = dapperHelps.ExecuteSqlInt(sqlstr, parametersp);
            return result > 0;
        }

        /// <summary>
        /// 根据主键删除(主键为GUID类型)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Id"></param>
        /// <returns></returns>
        public bool DeleteStringId<T>(string Id)
        {
            if (String.IsNullOrEmpty(Id))
            {
                return false;
            }
            string sqlstr = string.Format("DELETE {0} WHERE Id = @ID ", typeof(T).Name.ToString());
            parametersp.Add("@ID", Id);

            var result = dapperHelps.ExecuteSqlInt(sqlstr, parametersp);
            return result > 0;
        }

        /// <summary>
        /// 根据sql语句删除数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="whereStr"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool DeleteBySql<T>(string whereStr, object parameter = null) where T : class
        {
            if (String.IsNullOrEmpty(whereStr))
            {
                return false;
            }

            string sqlstr = string.Format("DELETE {0} WHERE {1}", typeof(T).Name.ToString(), whereStr);

            parametersp.AddDynamicParams(parameter);

            var result = dapperHelps.ExecuteSqlInt(sqlstr, parametersp);
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
                return dapperHelps.ExecuteUpdateModel<T>(model);
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

        /// <summary>
        /// 修改功能(sql语句修改)
        /// </summary>
        /// <param name="sqlStr"></param>
        /// <returns></returns>
        public int Update(string sqlStr)
        {
            try
            {
                return dapperHelps.ExecuteSqlInt(sqlStr);
            }
            catch (Exception ex)
            {
                WriteLogMethod.WriteLogs(ex);
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
            parametersp.Add("@ID", Id);
            return dapperHelps.ExecuteReaderReturnT<T>(sqlstr, parametersp);
        }

        /// <summary>
        /// 通过主键查询实体(GUID类型主键)
        /// </summary>
        /// <typeparam name="T">泛型实体类</typeparam>
        /// <param name="Id">主键id</param>
        /// <returns></returns>
        /// 
        public T GetModelById<T>(string Id) where T : class
        {
            string sqlstr = string.Format("select * from {0} where Id=@ID", typeof(T).Name.ToString());
            parametersp.Add("@ID", Id);
            return dapperHelps.ExecuteReaderReturnT<T>(sqlstr, parametersp);
        }

        /// <summary>
        /// 获取单个实体(sql语句查)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sqlStr"></param>
        /// <returns></returns>
        public T GetModel<T>(string sqlStr, object parameter = null)
        {
            parametersp.AddDynamicParams(parameter);

            return dapperHelps.ExecuteReaderReturnT<T>(sqlStr, parametersp);
        }

        /// <summary>
        /// 获取单个实体(所有字段)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="whereStr"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public T GetModelAll<T>(string whereStr, object parameter)
        {
            string sqlstr = string.Format("select * from {0} where {1}", typeof(T).Name.ToString(), whereStr);

            parametersp.AddDynamicParams(parameter);

            return dapperHelps.ExecuteReaderReturnT<T>(sqlstr, parametersp);
        }

        /// <summary>
        /// 获取单个实体(获取指定字段/获取所有字段)
        /// </summary>
        /// <typeparam name="T">查询的实体类</typeparam>
        /// <param name="whereStr">查询条件</param>
        /// <param name="parameter">参数化</param>
        /// <param name="FieldStr">查询字段(默认查全部,传参与sql保持一致)</param>
        /// <returns></returns>
        public T GetModelAssign<T>(string whereStr, object parameter, string FieldStr = "*")
        {
            string sqlstr = string.Format("select {2} from {0} where {1}", typeof(T).Name.ToString(), whereStr, FieldStr);

            parametersp.AddDynamicParams(parameter);

            return dapperHelps.ExecuteReaderReturnT<T>(sqlstr, parametersp);
        }

        /// <summary>
        /// 获取数据总行数
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="whereStr"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public int GetCount<T>(string whereStr, object parameter) where T : class
        {
            string sqlstr = string.Format("select count(*) from {0} where {1}", typeof(T).Name.ToString(), whereStr);

            parametersp.AddDynamicParams(parameter);

            return dapperHelps.ExecuteSqlInt(sqlstr, parametersp);
        }

        /// <summary>
        /// 检查数据是否存在
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="whereStr"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool IsExist<T>(string whereStr, object parameter) where T : class
        {
            string sqlstr = string.Format("select count(*) from {0} where {1}", typeof(T).Name.ToString(), whereStr);

            parametersp.AddDynamicParams(parameter);

            return dapperHelps.ExecuteSqlInt(sqlstr, parametersp) > 0 ? true : false;
        }

        /// <summary>
        /// 获取集合对象(in查询方式,根据业务需要使用必要时需要手动分页查询)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="whereArry"></param>
        /// <returns></returns>
        public List<T> GetListByIn<T>(string[] whereArry, int size = 10)
        {
            List<T> allList = new List<T>();
            var Idlist = whereArry.ToList();
            //总页数
            int PageNumber = (Idlist.Count % size) > 0 ? (Idlist.Count / size) + 1 : (Idlist.Count / size);
            for (int Page = 1; Page <= PageNumber; Page++)
            {
                List<string> pageinfo = Idlist.Skip((Page - 1) * size).Take(size).ToList();
                string sqlstr = string.Format("select * from {0} where GuId in @Arry", typeof(T).Name.ToString());
                parametersp.Add("@Arry", whereArry);
                List<T> pagelist = dapperHelps.ExecuteReaderReturnList<T>(sqlstr, parametersp);
                allList.AddRange(pagelist);
            }
            return allList;
        }

        /// <summary>
        /// 获取集合对象(sql语句查询)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sqlStr"></param>
        /// <returns></returns>
        public List<T> GetList<T>(string sqlStr, string orderbystr = null, object parameter = null)
        {
            if (!String.IsNullOrEmpty(orderbystr))
            {
                sqlStr += " order by " + orderbystr;
            }

            parametersp.AddDynamicParams(parameter);

            return dapperHelps.ExecuteReaderReturnList<T>(sqlStr, parametersp);
        }

        /// <summary>
        /// 获取集合对象(所有字段)
        /// </summary>
        /// <typeparam name="T">泛型实体</typeparam>
        /// <param name="whereStr">查询条件</param>
        /// <param name="orderByStr">排序条件</param>
        /// <returns></returns>
        public List<T> GetListAll<T>(string whereStr, string orderbystr = null, object parameter = null)
        {
            string sqlstr = string.Format("select * from {0} where {1} order by {2}", typeof(T).Name.ToString(), whereStr, orderbystr == null ? "Id" : orderbystr);

            parametersp.AddDynamicParams(parameter);

            return dapperHelps.ExecuteReaderReturnList<T>(sqlstr, parametersp);
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
        public List<T> GetPageList<T>(string wherestr, PageModel pageModel, string orderbystr = null, string SortStr = "desc")
        {
            string orderby = String.Empty;
            if (String.IsNullOrEmpty(orderbystr))
            {
                orderby = $@" ORDER BY CreateTime {SortStr} ";
            }
            else
            {
                orderby = $@" ORDER BY {orderbystr} {SortStr} ";
            }

            string sqlpage = "SELECT * FROM (SELECT A.*, ROW_NUMBER() OVER ({0}) rownum FROM {2} as A  where {1} ) Z WHERE Z.rownum > @start AND Z.rownum<= @end ORDER BY Z.rownum";
            string countSql = "select count(1) from {0} where {1}";

            parametersp.Add("@start", (pageModel.curPage - 1) * pageModel.pageSize);
            parametersp.Add("@end", pageModel.curPage * pageModel.pageSize);
            parametersp.AddDynamicParams(pageModel);

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
            string numberStr = String.Empty;
            string sqlpage = string.Format("SELECT * FROM ( {0}) Z WHERE Z.rownum > @start AND Z.rownum<= @end ORDER BY Z.rownum", sqlstr);

            parametersp.Add("@start", (pageModel.curPage - 1) * pageModel.pageSize);
            parametersp.Add("@end", pageModel.curPage * pageModel.pageSize);
            parametersp.AddDynamicParams(pageModel);

            pageModel.count = dapperHelps.ExecuteReaderReturnList<T>(sqlstr, pageModel).Count;

            return dapperHelps.ExecuteReaderReturnList<T>(sqlpage, parametersp);
        }

        /// <summary>
        /// 获取GetDatatableData数据集
        /// </summary>
        /// <returns></returns>
        public DataTable GetDatatableData<T>(string sqlstr, object parameter = null)
        {
            parametersp.AddDynamicParams(parameter);

            return dapperHelps.GetDatatableData(sqlstr, parametersp);
        }
        #endregion
    }
}
