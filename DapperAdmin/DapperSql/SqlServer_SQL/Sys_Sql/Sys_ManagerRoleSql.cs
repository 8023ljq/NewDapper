namespace DapperSql.Sys_Sql
{
    /// <summary>
    /// 管理角色sql语句
    /// </summary>
    public class Sys_ManagerRoleSql: BaseSql
    {
        /// <summary>
        /// 获取角色分页列表集合
        /// </summary>
        public static string getPageList = $@"select (select Name from Sys_Manager B where A.AddUserId=b.Id) as AddUserName,
                                              (select Name from Sys_Manager B where A.UpdateUserId = b.Id) as UpdateUserName,
                                              A.* {GetRowNum("A.AddTime", "desc")} from Sys_ManagerRole A where A.IsDelete=0";

        /// <summary>
        /// 关联查询角色分配权限不包含父级菜单的数据集合
        /// </summary>
        public static string getMenuPurview = $@"select A.* from  [dbo].[Sys_RolePurview] A left join [dbo].[Sys_Menu] B
                                                 on A.ResourceId=B.GuId where A.RoleId=@RoleId AND B.ParentId!='0'";
    }
}
