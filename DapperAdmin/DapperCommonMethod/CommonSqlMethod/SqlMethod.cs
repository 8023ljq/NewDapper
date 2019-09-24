using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperCommonMethod.CommonSqlMethod
{
    public class SqlMethod
    {
        /// <summary>
        /// 拼接分页查询语句
        /// </summary>
        /// <param name="Field"></param>
        /// <param name="SortType"></param>
        /// <returns></returns>
        public static string GetRowNum(string Field, string SortType)
        {
            return $@", ROW_NUMBER() OVER (ORDER BY {Field} {SortType}) rownum ";
        }
    }
}
