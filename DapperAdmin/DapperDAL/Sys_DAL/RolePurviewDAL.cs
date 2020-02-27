using DapperModel.DataModel;
using DapperSql.Sys_Sql;
using System.Collections.Generic;

namespace DapperDAL
{
    /// <summary>
    /// 角色数据处理层
    /// </summary>
    public class RolePurviewDAL : BaseDALS
    {
        /// <summary>
        /// 关联查询角色分配权限不包含父级菜单的数据集合
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public List<Sys_RolePurview> GetMenuPurview(string Id)
        {
            return GetList<Sys_RolePurview>(Sys_ManagerRoleSql.getMenuPurview, null, new { RoleId = Id });
        }

        /// <summary>
        /// 通过关联ID查询角色权限列表(修改角色时检查角色权限是否存在)
        /// </summary>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        public List<Sys_RolePurview> GetRolePurviewByRoleId(string RoleId)
        {
           return  GetListAll<Sys_RolePurview>("RoleId=@RoleId and IsLocking=0 and IsDelete=0", null, new { RoleId = RoleId });
        }

        /// <summary>
        /// 根据角色查询不同的菜单权限
        /// </summary>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        public List<Sys_RolePurview> GetMenuPurviewList(string RoleId)
        {
            return GetListAll<Sys_RolePurview>("RoleId=@RoleId", null, new { RoleId = RoleId });
        }
    }
}
