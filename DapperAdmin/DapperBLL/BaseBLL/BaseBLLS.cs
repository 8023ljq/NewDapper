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
        private BaseDALS baseDALS = new BaseDALS();

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
        /// 通过主键查询实体(string类型主键)
        /// </summary>
        /// <typeparam name="T">泛型实体类</typeparam>
        /// <param name="Id">主键id</param>
        /// <returns></returns>
        public T GetModelById<T>(string Id) where T : class
        {
            return baseDALS.GetModelById<T>(Id);
        }

        /// <summary>
        /// 获取单个实体(条件查询)
        /// </summary>
        /// <typeparam name="T">泛型实体</typeparam>
        /// <param name="whereStr">查询条件</param>
        /// <param name="orderByStr">排序条件</param>
        /// <returns></returns>
        public T GetModel<T>(Dictionary<string, WhereModel> whereStr, Dictionary<string, OrderByModel> orderByStr)
        {
            return baseDALS.GetModel<T>(whereStr, orderByStr);
        }

        /// <summary>
        /// 获取集合对象(条件查询)
        /// </summary>
        /// <typeparam name="T">泛型实体</typeparam>
        /// <param name="whereStr">查询条件</param>
        /// <param name="orderByStr">排序条件</param>
        /// <returns></returns>
        public List<T> GetList<T>(Dictionary<string, WhereModel> whereStr, Dictionary<string, OrderByModel> orderByStr)
        {
            return baseDALS.GetList<T>(whereStr, orderByStr);
        }

        /// <summary>
        /// 获取集合对象(条件查询)
        /// </summary>
        /// <typeparam name="T">泛型实体</typeparam>
        /// <param name="whereStr">查询条件</param>
        /// <param name="orderByStr">排序条件</param>
        /// <returns></returns>
        public List<T> GetList<T>(string whereStr, object parameter = null)
        {
            return baseDALS.GetList<T>(whereStr, parameter);
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
