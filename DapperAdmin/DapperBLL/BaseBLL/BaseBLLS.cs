using DapperCommonMethod.CommonModel;
using DapperDAL.BaseDAL;
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

        /// <summary>
        /// 新增操作(主键为Int类型)
        /// </summary>
        /// <typeparam name="T">实体对象</typeparam>
        /// <param name="Model">赋值的实体对象</param>
        /// <param name="ID">主键ID</param>
        /// <returns></returns>
        public bool AddModelInt<T>(T Model, out long ID) where T : class
        {
            ID = baseDALS.CreateInt(Model);
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
        public bool AddModelInt<T>(T Model) where T : class
        {
            long ID = baseDALS.CreateInt(Model);
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
        /// 新增操作(主键为Guid类型)
        /// </summary>
        /// <typeparam name="T">实体对象</typeparam>
        /// <param name="Model">赋值的实体对象</param>
        /// <param name="ID">主键ID</param>
        /// <returns></returns>
        public bool AddModelGuid<T>(T Model, out string ID) where T : class
        {
            ID = baseDALS.CreateGuid(Model);
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
        public bool AddModelGuid<T>(T Model) where T : class
        {
            string ID = baseDALS.CreateGuid(Model);
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
        /// 获取单个实体
        /// </summary>
        /// <typeparam name="T">泛型实体</typeparam>
        /// <param name="tableName">表名</param>
        /// <param name="whereStr">查询条件</param>
        /// <param name="orderByStr">排序条件</param>
        /// <returns></returns>
        public T GetModel<T>(string tableName, Dictionary<string, WhereModel> whereStr, Dictionary<string, OrderByModel> orderByStr)
        {
            return baseDALS.GetModel<T>(tableName, whereStr, orderByStr);
        }

        /// <summary>
        /// 获取集合对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tableName"></param>
        /// <param name="whereStr"></param>
        /// <param name="orderByStr"></param>
        /// <returns></returns>
        public List<T> GetList<T>(string tableName, Dictionary<string, WhereModel> whereStr, Dictionary<string, OrderByModel> orderByStr)
        {
            return baseDALS.GetList<T>(tableName, whereStr, orderByStr);
        }

    }
}
