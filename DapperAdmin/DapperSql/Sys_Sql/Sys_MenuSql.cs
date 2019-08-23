using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperSql.Sys_Sql
{
    /// <summary>
    /// 菜单表SQL语句
    /// </summary>
    public class Sys_MenuSql
    {
        /// <summary>
        /// 查询所有菜单sql
        /// </summary>
        public static string selectListSql = "select * from Sys_Menu where IsDelete=@IsDelete order by Layers,Sort";
    }
}
