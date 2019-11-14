using DapperModel.DataModel;
using System.Collections.Generic;

namespace DapperDAL
{
    /// <summary>
    /// 菜单按钮权限数据处理层
    /// </summary>
    public class MenuButtonPowerDAL: BaseDALS
    {
        /// <summary>
        /// 获取对应角色的所有菜单按钮权限
        /// </summary>
        /// <param name="Id">角色ID</param>
        /// <returns></returns>
        public List<Sys_MenuButtonPower> GetMenuButtonList(string Id)
        {
           return GetListAll<Sys_MenuButtonPower>("RelationRoleId=@RelationRoleId and IsDelete=0", null, new { RelationRoleId = Id });
        }
    }
}
