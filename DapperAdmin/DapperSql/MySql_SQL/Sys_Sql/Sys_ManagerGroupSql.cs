namespace DapperSql.MySql_SQL
{

    public class Sys_ManagerGroupSql
    {
        /// <summary>
        /// 全列插入sql语句
        /// </summary>
       public static string InsertAllSqlStr=$@"insert into Sys_ManagerGroup 
                                             (Id,
                                              GroupName,
                                              AddUserId,
                                              AddTime,
                                              UpdateUserId,
                                              UpdateTime,
                                              IsLocking,
                                              IsDelete,
                                              Remarks)
                                              value(
                                               @Id,
                                               @GroupName,
                                               @AddUserId,
                                               @AddTime,
                                               @UpdateUserId,
                                               @UpdateTime,
                                               @IsLocking,
                                               @IsDelete,
                                               @Remarks)";

        /// <summary>
        /// 全列查查询
        /// </summary>
        public static string SelectSqlStr = $@"select * from Sys_ManagerGroup ";
    }
}

