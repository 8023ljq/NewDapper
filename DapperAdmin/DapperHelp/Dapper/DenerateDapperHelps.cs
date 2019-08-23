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
    /// 传入连接字符串连接数据库
    /// </summary>
    public class DenerateDapperHelps
    {
        //读数据库
        private static string writesqlconnection ="";

        //写数据库
        private static string readsqlconnection = "";

        //数据库连接超时时间
        static int commandTimeout = 30;

        SqlConnection sqlconn = new SqlConnection(writesqlconnection);

        /// <summary>
        /// 事物操作打开数据库
        /// </summary>
        /// <returns></returns>
        public IDbConnection GetOpenConnection()
        {
            var conn = new SqlConnection(readsqlconnection);
            conn.Open();
            return conn;
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
        /// 关闭数据库连接 SqlConnection
        /// </summary>
        private static void CloseConnect(IDbConnection conn)
        {
            if (conn.State == ConnectionState.Open)
            {
                try
                {
                    conn.Close();
                }
                catch (Exception ex)
                {
                    WriteLogMethod.WriteLogs(ex);
                }
            }
        }

        /// <summary>
        /// 使用读或写数据库
        /// </summary>
        /// <param name="useWriteConn"></param>
        /// <returns></returns>
        IDbConnection GetConnection(bool useWriteConn)
        {
            if (useWriteConn)
            {
                return new SqlConnection(writesqlconnection);
            }
            return new SqlConnection(readsqlconnection);
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
                using (IDbConnection conn = GetConnection(useWriteConn))
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

        /// 执行sql返回一个对象
        /// </summary>
        /// <typeparam name="T">对象名</typeparam>
        /// <param name="sql">sql语句或者存储过程名</param>
        /// <param name="param">sql参数,可匿名类型，可对象类型</param>
        /// <param name="useWriteConn">读或写数据库</param>
        /// <param name="transaction">是否执行事务</param>
        /// <returns></returns>
        public T ExecuteReaderReturnT<T>(string sql, object parameter = null, bool useWriteConn = false, IDbTransaction transaction = null)
        {
            if (transaction == null)
            {
                using (IDbConnection conn = GetConnection(useWriteConn))
                {
                    OpenConnect(conn);
                    return conn.QueryFirstOrDefault<T>(sql, parameter, commandTimeout: commandTimeout);
                }
            }
            else
            {
                var conn = transaction.Connection;
                return conn.QueryFirstOrDefault<T>(sql, parameter, commandTimeout: commandTimeout, transaction: transaction);
            }
        }

    }
}
