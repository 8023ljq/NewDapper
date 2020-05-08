using Dapper;
using DapperCommonMethod.CommonMethod;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperHelp.Dapper
{
    /// <summary>
    /// 代码生成时连接数据库
    /// </summary>
    public class BuilderSqlHelps
    {
        //练级字符串
        private static string writesqlconnection;

        //数据库连接超时时间
        static int commandTimeout = 30;

        /// <summary>
        ///  //构造函数，初始化对象
        /// </summary>
        /// <param name="connstr"></param>
        public BuilderSqlHelps(string connstr)
        {
            writesqlconnection = connstr;
        }

        /// <summary>
        /// 连接数据库
        /// </summary>
        /// <param name="useWriteConn"></param>
        /// <returns></returns>
        IDbConnection GetConnection()
        {
            return new SqlConnection(writesqlconnection);
        }

        /// <summary>
        /// 检查是否连接
        /// </summary>
        /// <returns></returns>
        public bool IsOpen()
        {
            using (IDbConnection conn = GetConnection())
            {
                OpenConnect(conn);

                if (conn.State == ConnectionState.Closed)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        /// <summary>
        /// 打开数据库连接
        /// </summary>
        private void OpenConnect(IDbConnection conn)
        {
            if (conn.State == ConnectionState.Closed)
            {
                try
                {
                    conn.Open();
                }
                catch (Exception ex)
                {
                    WriteLogMethod.WriteLogs(ex);
                }
            }
        }


        /// <summary>
        /// 执行sql返回多个对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="useWriteConn">true=写，默认false=读</param>
        /// <returns></returns>
        public List<T> ExecuteReaderReturnList<T>(string sql, object param = null, bool useWriteConn = false, IDbTransaction transaction = null)
        {
            if (transaction == null)
            {
                //using 语句块在执行完代码块之后会自动关闭占用资源
                using (IDbConnection conn = GetConnection())
                {
                    OpenConnect(conn);
                    var a = conn.Query<T>(sql, param, commandTimeout: commandTimeout, transaction: transaction).AsList<T>();
                    return a;
                }
            }
            else
            {
                var conn = transaction.Connection;
                return conn.Query<T>(sql, param, commandTimeout: commandTimeout, transaction: transaction).ToList();
            }
        }

    }
}
