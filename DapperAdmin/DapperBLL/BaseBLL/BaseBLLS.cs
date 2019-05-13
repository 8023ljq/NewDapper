using DapperDAL.C_DAL;
using System;

namespace DapperBLL.BaseBLL
{
    /// <summary>
    /// 公共业务处理层
    /// </summary>
    public class BaseBLLS
    {
        /// <summary>
        /// 新增操作(主键为Int类型)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool AddModelInt<T>(T Model, out long ID) where T : class
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
        /// 新增操作(主键为Guid类型)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static bool AddModelGuid<T>(T Model, out string ID) where T : class
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
    }
}
