﻿using Dapper;
using DapperCommonMethod.CommonEnum;
using DapperCommonMethod.CommonModel;
using System.Collections.Generic;
using System.Linq;

namespace DapperHelp.Dapper
{
    public class DapperSpliceCondition
    {
        /// <summary>
        /// 拼接SQL语句
        /// </summary>
        /// <param name="sql">基础语句(select * from 表名 后面给空格用户拼接)</param>
        /// <param name="whereStr"></param>
        /// <param name="orderByStr"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static string GetWhereStr(string sql, Dictionary<string, WhereModel> whereStr, Dictionary<string, OrderByModel> orderByStr, out DynamicParameters parameters)
        {
            //返回查询语句
            sql += " where 1=1";
            parameters = new DynamicParameters();
         
            //拼接查询条件
            if (whereStr.Any())
            {
                foreach (var item in whereStr)
                {
                    var itemKey = item.Key;
                    var itemValue = item.Value;
                    switch (itemValue.InquireManner)
                    {
                        case (int)EnumSqlType.Equal:
                            sql += " and [" + itemKey + "]=@" + itemKey + "";
                            parameters.Add("@" + itemKey + "", itemValue.Content);
                            break;
                        case (int)EnumSqlType.NoEqual:
                            sql += " and [" + itemKey + "]!=@" + itemKey + "";
                            parameters.Add("@" + itemKey + "", itemValue.Content);
                            break;
                        case (int)EnumSqlType.Big:
                            sql += " and [" + itemKey + "]>@" + itemKey + "";
                            parameters.Add("@" + itemKey + "", itemValue.Content);
                            break;
                        case (int)EnumSqlType.BigEqual:
                            sql += " and [" + itemKey + "]>=@" + itemKey + "";
                            parameters.Add("@" + itemKey + "", itemValue.Content);
                            break;
                        case (int)EnumSqlType.Small:
                            sql += " and [" + itemKey + "]<@" + itemKey + "";
                            parameters.Add("@" + itemKey + "", itemValue.Content);
                            break;
                        case (int)EnumSqlType.SmallEqual:
                            sql += " and [" + itemKey + "]<=@" + itemKey + "";
                            parameters.Add("@" + itemKey + "", itemValue.Content);
                            break;
                        case (int)EnumSqlType.In:
                            sql += " and [" + itemKey + "] in @" + itemKey + "";
                            parameters.Add("@" + itemKey + "", itemValue.Content);
                            break;
                        case (int)EnumSqlType.Like:
                            sql += " and [" + itemKey + "] Like @" + itemKey + "";
                            parameters.Add("@" + itemKey + "", itemValue.Content);
                            break;
                        case (int)EnumSqlType.BetweenAnd:
                            sql += " and [" + itemKey + "] Like @" + itemKey + "";

                            parameters.Add("@" + itemKey + "", itemValue.Content);
                            break;
                        default:
                            break;
                    }
                }
            }
            //拼接查询条件
            if (orderByStr.Any())
            {
                foreach (var item in orderByStr)
                {

                }
            }
            //parameters = null;
            return sql;
        }
    }
}
