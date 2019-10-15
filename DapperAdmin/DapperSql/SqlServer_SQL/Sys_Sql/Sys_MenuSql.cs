using DapperCommonMethod.CommonEnum;

namespace DapperSql.Sys_Sql
{
    /// <summary>
    /// 菜单表SQL语句
    /// </summary>
    public class Sys_MenuSql
    {
        public static string selectMenuIsExist = "select * from Sys_Menu where FullName=@FullName or AddressUrl=@AddressUrl or GuId=@ParentId";

        /// <summary>
        /// 查询所有菜单sql
        /// </summary>
        //public static string selectListSql = "select * from Sys_Menu where IsDelete=@IsDelete and ResourceType="+ (int)ResourceTypeEnum.Menu + " order by Layers,Sort";
        public static string selectListSql = "select * from Sys_Menu where IsDelete=@IsDelete order by Layers,Sort desc";

        /// <summary>
        /// 查询单个实体
        /// </summary>
        public static string getmodel = "select * from Sys_Menu where GuId=@Guid";

        /// <summary>
        /// 查询添加按钮时是否已存在
        /// </summary>
        public static string selectMenuPowerSql = "select * from Sys_Menu where ParentId=@MenuId and (FullName=@PowerName or Purview=@PowerMark)";
    }
}
