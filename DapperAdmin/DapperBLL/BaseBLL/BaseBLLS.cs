using Dapper;
using DapperCommonMethod.CommonModel;
using DapperDAL.BaseDAL;
using DapperModel.CommonModel;
using System;
using System.Collections.Generic;

namespace DapperBLL.BaseBLL
{
    /// <summary>
    /// 公共业务处理层
    /// </summary>
    public class BaseBLLS
    {
        public BaseDALS baseDALS = new BaseDALS();

        #region 增

        /// <summary>
        /// 新增操作并返回主键ID(主键为Int类型)
        /// </summary>
        /// <typeparam name="T">实体对象</typeparam>
        /// <param name="Model">赋值的实体对象</param>
        /// <param name="ID">主键ID</param>
        /// <returns></returns>
        public bool InsertModelInt<T>(T Model, out long ID) where T : class
        {
            ID = baseDALS.InsertModelInt(Model);
            if (ID > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 新增操作(主键为Int类型)
        /// </summary>
        /// <typeparam name="T">实体对象</typeparam>
        /// <param name="Model">赋值的实体对象</param>
        /// <returns></returns>
        public bool InsertModelInt<T>(T Model) where T : class
        {
            long ID = baseDALS.InsertModelInt(Model);
            if (ID > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 新增操作并返回主键ID(主键为Guid类型)
        /// </summary>
        /// <typeparam name="T">实体对象</typeparam>
        /// <param name="Model">赋值的实体对象</param>
        /// <param name="ID">主键ID</param>
        /// <returns></returns>
        public bool InsertModelGuid<T>(T Model, out string ID) where T : class
        {
            ID = baseDALS.InsertModelGuid(Model);
            if (!String.IsNullOrEmpty(ID))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 新增操作(主键为Guid类型)
        /// </summary>
        /// <typeparam name="T">实体对象</typeparam>
        /// <param name="Model">赋值的实体对象</param>
        /// <returns></returns>
        public bool InsertModelGuid<T>(T Model) where T : class
        {
            string ID = baseDALS.InsertModelGuid(Model);
            if (!String.IsNullOrEmpty(ID))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 添加集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Model"></param>
        /// <returns></returns>
        public bool InsertList<T>(List<T> modelList) where T : class
        {
            if (modelList.Count <= 0)
            {
                return false;
            }
            baseDALS.InsertList(modelList);
            return true;
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
            return baseDALS.DeleteIntId<T>(array);
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
            return baseDALS.DeleteStringId<T>(array);
        }

        #endregion

        #region 改

        /// <summary>
        /// 修改操作(单个实体)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Model"></param>
        /// <returns></returns>
        public bool UpdateModel<T>(T Model) where T : class
        {
            return baseDALS.UpdateModel(Model);
        }

        /// <summary>
        /// 批量更新实体,返回更新状态
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="List"></param>
        /// <returns></returns>
        public bool UpdateList<T>(List<T> List) where T : class
        {
            return baseDALS.UpdateList(List);
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
            return baseDALS.UpdateList(List, out ErrorCount);
        }

        /// <summary>
        /// 修改功能(sql语句修改)
        /// </summary>
        /// <param name="sqlStr"></param>
        /// <returns></returns>
        public int Update(string sqlStr)
        {
            return baseDALS.Update(sqlStr);
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
            return baseDALS.GetModelById<T>(Id);
        }

        /// <summary>
        /// 通过主键查询实体(GUID类型主键)
        /// </summary>
        /// <typeparam name="T">泛型实体类</typeparam>
        /// <param name="Id">主键id</param>
        /// <returns></returns>
        public T GetModelById<T>(string Id) where T : class
        {
            return baseDALS.GetModelById<T>(Id);
        }

        /// <summary>
        /// 获取单个实体(sql语句查)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sqlStr"></param>
        /// <returns></returns>
        public T GetModel<T>(string sqlStr)
        {
            return baseDALS.GetModel<T>(sqlStr);
        }

        /// <summary>
        /// 获取单个实体(所有字段)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="whereStr"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public T GetModelAll<T>(string whereStr, object parameter = null)
        {
            return baseDALS.GetModelAll<T>(whereStr, parameter);
        }

        /// <summary>
        /// 获取集合对象(sql语句查询)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sqlStr"></param>
        /// <returns></returns>
        public List<T> GetList<T>(string sqlStr)
        {
            return baseDALS.GetList<T>(sqlStr);
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
            return baseDALS.GetListAll<T>(whereStr, orderbystr, parameter);
        }

        /// <summary>
        /// 分页获取列表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="wherestr"></param>
        /// <param name="pageModel"></param>
        /// <returns></returns>
        public List<T> GetPageList<T>(string wherestr, PageModel pageModel, string orderbystr = null)
        {
            return baseDALS.GetPageList<T>(wherestr, pageModel, orderbystr);
        }

        /// <summary>
        /// 连接查询分页方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sqlstr">查询语句(正常连接查询语句)</param>
        /// <param name="pageModel">查询条件</param>
        /// <param name="orderbystr">排序条件</param>
        /// <returns></returns>
        public List<T> GetPageJoinList<T>(string sqlstr, PageModel pageModel, string orderbystr = null)
        {
            return baseDALS.GetPageJoinList<T>(sqlstr, pageModel, orderbystr);
        }

        #endregion
    }
}
