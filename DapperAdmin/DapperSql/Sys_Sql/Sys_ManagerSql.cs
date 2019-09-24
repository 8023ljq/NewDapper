namespace DapperSql.Sys_Sql
{
    /// <summary>
    /// 管理员查询时调用的sql语句统一存放位置
    /// </summary>
    public class Sys_ManagerSql: BaseSql
    {
        /// <summary>
        /// 查询所有数据
        /// </summary>
        public static string selectAllList = "select * from Sys_Manager where 1=1 and ";

        /// <summary>
        /// 获取管理员分页列表
        /// </summary>
        public static string getPageList = $@"select B.RoleName,A.* {GetRowNum("A.AddTime", "desc")} from Sys_Manager A left join Sys_ManagerRole B on A.RelationId=B.Id where A.IsDelete=0";
     }
}
