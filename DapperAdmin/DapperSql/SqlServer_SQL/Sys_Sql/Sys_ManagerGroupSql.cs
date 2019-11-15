namespace DapperSql.Sys_Sql
{
    /// <summary>
    /// 管理员组
    /// </summary>
    public class Sys_ManagerGroupSql
    {
        public static string selectListSql = $@"select A.*,B.Name as AddName from [dbo].[Sys_ManagerGroup] A
                                                left join [dbo].[Sys_Manager] B on A.AddUserId=B.Id
                                                where A.IsLocking=0 and A.IsDelete=0 ";
    }
}


