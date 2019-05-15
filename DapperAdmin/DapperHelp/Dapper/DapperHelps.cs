﻿using Dapper;
using DapperCommonMethod.CommonMethod;
using DapperExtensions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperHelp.Dapper
{
    public static class DapperHelps
    {
        //读数据库
        private static string writesqlconnection = ConfigurationManager.ConnectionStrings["WriteData"].ConnectionString;

        //写数据库
        private static string readsqlconnection = ConfigurationManager.ConnectionStrings["ReadData"].ConnectionString;

        //数据库连接超时时间
        static int commandTimeout = 30;

        static SqlConnection sqlconn = new SqlConnection(writesqlconnection);

        /// <summary>
        /// 打开数据库连接
        /// </summary>
        private static void OpenConnect(IDbConnection conn)
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
        static IDbConnection GetConnection(bool useWriteConn)
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
        public static List<T> ExecuteReaderReturnList<T>(string sql, object param = null, bool useWriteConn = false, IDbTransaction transaction = null)
        {
            if (transaction == null)
            {
                //using 语句块在执行完代码块之后会自动关闭占用资源
                using (IDbConnection conn = GetConnection(useWriteConn))
                {
                    OpenConnect(conn);
                    var a= conn.Query<T>(sql, param, commandTimeout: commandTimeout, transaction: transaction).AsList<T>();
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
        public static T ExecuteReaderReturnT<T>(string sql, object parameter = null, bool useWriteConn = false, IDbTransaction transaction = null)
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

        /// <summary>
        /// 执行sql返回一个对象--异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="useWriteConn"></param>
        /// <returns></returns>
        public static async Task<T> ExecuteReaderRetTAsync<T>(string sql, object param = null, bool useWriteConn = false)
        {
            using (IDbConnection conn = GetConnection(useWriteConn))
            {
                OpenConnect(conn);
                return await conn.QueryFirstOrDefaultAsync<T>(sql, param, commandTimeout: commandTimeout).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// 执行sql返回多个对象--异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="useWriteConn"></param>
        /// <returns></returns>
        public static async Task<List<T>> ExecuteReaderRetListAsync<T>(string sql, object param = null, bool useWriteConn = false)
        {
            using (IDbConnection conn = GetConnection(useWriteConn))
            {
                OpenConnect(conn);
                var list = await conn.QueryAsync<T>(sql, param, commandTimeout: commandTimeout).ConfigureAwait(false);
                return list.ToList();
            }
        }


        /// <summary>
        /// 执行sql，返回影响行数 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static int ExecuteSqlInt(string sql, object param = null, IDbTransaction transaction = null)
        {
            if (transaction == null)
            {
                using (IDbConnection conn = GetConnection(true))
                {
                    OpenConnect(conn);
                    return conn.Execute(sql, param, commandTimeout: commandTimeout, commandType: CommandType.Text);
                }
            }
            else
            {
                var conn = transaction.Connection;
                return conn.Execute(sql, param, transaction: transaction, commandTimeout: commandTimeout, commandType: CommandType.Text);
            }
        }

        /// <summary>
        /// 执行sql，返回影响行数--异步
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="param"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static async Task<int> ExecuteSqlIntAsync(string sql, object param = null, IDbTransaction transaction = null)
        {
            if (transaction == null)
            {
                using (IDbConnection conn = GetConnection(true))
                {
                    OpenConnect(conn);
                    return await conn.ExecuteAsync(sql, param, commandTimeout: commandTimeout, commandType: CommandType.Text).ConfigureAwait(false);
                }
            }
            else
            {
                var conn = transaction.Connection;
                return await conn.ExecuteAsync(sql, param, transaction: transaction, commandTimeout: commandTimeout, commandType: CommandType.Text).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// 根据id获取实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="transaction"></param>
        /// <param name="useWriteConn"></param>
        /// <returns></returns>
        public static T GetById<T>(int id, IDbTransaction transaction = null, bool useWriteConn = false) where T : class
        {
            if (transaction == null)
            {
                using (IDbConnection conn = GetConnection(useWriteConn))
                {
                    OpenConnect(conn);
                    return conn.Get<T>(id, commandTimeout: commandTimeout);
                }
            }
            else
            {
                var conn = transaction.Connection;
                return conn.Get<T>(id, transaction: transaction, commandTimeout: commandTimeout);
            }
        }


        /// <summary>
        /// 根据id获取实体--异步
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="transaction"></param>
        /// <param name="useWriteConn"></param>
        /// <returns></returns>
        public static async Task<T> GetByIdAsync<T>(int id, IDbTransaction transaction = null, bool useWriteConn = false) where T : class
        {
            if (transaction == null)
            {
                using (IDbConnection conn = GetConnection(useWriteConn))
                {
                    OpenConnect(conn);
                    return await conn.GetAsync<T>(id, commandTimeout: commandTimeout);
                }
            }
            else
            {
                var conn = transaction.Connection;
                return await conn.GetAsync<T>(id, transaction: transaction, commandTimeout: commandTimeout);
            }
        }

        /// <summary>
        /// 插入实体(int主键)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static long ExecuteInsert<T>(T item, IDbTransaction transaction = null) where T : class
        {
            if (transaction == null)
            {
                using (IDbConnection conn = GetConnection(true))
                {
                    OpenConnect(conn);
                    var a = conn.Insert<T>(item, commandTimeout: commandTimeout);
                    return a;
                }
            }
            else
            {
                var conn = transaction.Connection;
                return conn.Insert(item, transaction: transaction, commandTimeout: commandTimeout);
            }
        }

        /// <summary>
        /// 插入实体(主键Guid)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static string ExecuteInsertGuid<T>(T item, IDbTransaction transaction = null) where T : class
        {
            if (transaction == null)
            {
                using (IDbConnection conn = GetConnection(true))
                {
                    OpenConnect(conn);
                    var a = conn.Insert<T>(item, commandTimeout: commandTimeout);
                    return a;
                }
            }
            else
            {
                var conn = transaction.Connection;
                return conn.Insert(item, transaction: transaction, commandTimeout: commandTimeout);
            }
        }


        /// <summary>
        /// 批量插入实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="transaction"></param>
        public static void ExecuteInsertList<T>(IEnumerable<T> list, IDbTransaction transaction = null) where T : class
        {
            if (transaction == null)
            {
                using (IDbConnection conn = GetConnection(true))
                {
                    OpenConnect(conn);
                    conn.Insert<T>(list, commandTimeout: commandTimeout);
                }
            }
            else
            {
                var conn = transaction.Connection;
                conn.Insert(list, transaction: transaction, commandTimeout: commandTimeout);
            }
        }

        /// <summary>
        /// 更新单个实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static bool ExecuteUpdate<T>(T item, IDbTransaction transaction = null) where T : class
        {
            if (transaction == null)
            {
                using (IDbConnection conn = GetConnection(true))
                {
                    OpenConnect(conn);
                    return conn.Update(item, commandTimeout: commandTimeout);
                }
            }
            else
            {
                var conn = transaction.Connection;
                return conn.Update(item, transaction: transaction, commandTimeout: commandTimeout);
            }
        }

        /// <summary>
        /// 批量更新实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public static bool ExecuteUpdateList<T>(List<T> item, IDbTransaction transaction = null) where T : class
        {
            bool isOk = true;

            if (transaction == null)
            {
                using (IDbConnection conn = GetConnection(true))
                {
                    OpenConnect(conn);
                    foreach (var obj in item)
                    {
                        isOk = conn.Update(obj, commandTimeout: commandTimeout);
                        if (!isOk)
                        {
                            return false;
                        }
                    }
                }
            }
            else
            {
                var conn = transaction.Connection;
                foreach (var obj in item)
                {
                    isOk = conn.Update(obj, transaction: transaction, commandTimeout: commandTimeout);
                    if (!isOk)
                    {
                        return false;
                    }
                }
            }
            return true;
        }


        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql">主sql 不带 order by</param>
        /// <param name="sort">排序内容 id desc，add_time asc</param>
        /// <param name="pageIndex">第几页</param>
        /// <param name="pageSize">每页多少条</param>
        /// <param name="useWriteConn">是否主库</param>
        /// <returns></returns>
        public static List<T> ExecutePageList<T>(string sql, string sort, int pageIndex, int pageSize, bool useWriteConn = false, object param = null)
        {
            string pageSql = @"SELECT TOP {0} * FROM (SELECT ROW_NUMBER() OVER (ORDER BY {1}) _row_number_,* FROM ({2})temp )temp1 WHERE temp1._row_number_>{3} ORDER BY _row_number_";
            string execSql = string.Format(pageSql, pageSize, sort, sql, pageSize * (pageIndex - 1));
            using (IDbConnection conn = GetConnection(useWriteConn))
            {
                OpenConnect(conn);
                return conn.Query<T>(execSql, param, commandTimeout: commandTimeout).ToList();
            }
        }
    }
}