using DapperDAL.DB_DAL;
using System;

namespace DapperBLL.BaseBLL
{
    /// <summary>
    /// 公共业务处理层
    /// </summary>
    public  class BaseBLLS
    {
        /// <summary>
        /// 新增操作(主键为Int类型)
        /// </summary>
        /// <typeparam name="T">实体对象</typeparam>
        /// <param name="Model">赋值的实体对象</param>
        /// <param name="ID">主键ID</param>
        /// <returns></returns>
        public bool AddModelInt<T>(T Model, out long ID) where T : class
        {
            ID = FreighterDAL.CreateInt(Model);
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
            long ID = FreighterDAL.CreateInt(Model);
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
            ID = FreighterDAL.CreateGuid(Model);
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
            string ID = FreighterDAL.CreateGuid(Model);
            if (!String.IsNullOrEmpty(ID))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
