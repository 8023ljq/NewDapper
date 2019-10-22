using DapperCacheHelps.RedisHelper;
using DapperDAL.BaseDAL;
using DapperModel.CommonModel;
using System.Collections.Generic;

namespace DapperBLL.BaseBLL
{
    /// <summary>
    /// 公共业务处理层
    /// </summary>
    public class BaseBLLS
    {
        public BaseDALS baseDALS = new BaseDALS();

        /// <summary>
        /// 缓存管理员信息
        /// </summary>
        public static RedisHelper redis = new RedisHelper();

        /// <summary>
        /// 缓存管理员信息
        /// </summary>
        public static RedisHelper Commonredis = new RedisHelper(1);

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
