using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperSql.Sys_Sql
{
    public static class Sys_ManagerSql
    {
        /// <summary>
        /// 查询所有数据
        /// </summary>
        public static string selectAllList = "select * from Sys_Manager where 1=1 and ";

        /// <summary>
        /// 获取管理员分页列表
        /// </summary>
        public static string getPageList = "select B.RoleName,A.* from Sys_Manager A left join Sys_ManagerRole B on A.RoleId=B.Id where A.IsDelete=0";




     }
}
