using Dapper;
using DapperCacheHelps.CSRedisHelper;
using DapperHelp.Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperDAL
{
    /// <summary>
    /// 代码生成数据处理层
    /// </summary>
    public class BuilderDAL
    {
        public static string connstring;

        public BuilderSqlHelps builderSqlHelps;

        public BuilderDAL(string connstr)
        {
            builderSqlHelps=new BuilderSqlHelps(connstr);
        }

        public DynamicParameters parametersp = new DynamicParameters();

        public bool IsOpen()
        {
            return builderSqlHelps.IsOpen();
        }


        #region 封装连接方法

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

            return builderSqlHelps.ExecuteReaderReturnList<T>(sqlStr, parametersp);
        }

        #endregion
    }
}
