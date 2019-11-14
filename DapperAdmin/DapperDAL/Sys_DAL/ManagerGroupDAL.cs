using DapperModel.CommonModel;
using DapperModel.DataModel;
using DapperSql.Sys_Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperDAL
{
    /// <summary>
    /// 管理员组数据处理层
    /// </summary>
    public class ManagerGroupDAL : BaseDALS
    {
        /// <summary>
        /// 获取所有用户组
        /// </summary>
        /// <param name="selectModel"></param>
        /// <returns></returns>
        public List<Sys_ManagerGroup> GetManagerGroupList(SelectModel selectModel)
        {
            return GetList<Sys_ManagerGroup>(Sys_ManagerGroupSql.selectListSql, "order by A.AddTime desc", selectModel);
        }

        /// <summary>
        /// 通过组名获取实体
        /// </summary>
        /// <param name="GroupName"></param>
        /// <returns></returns>
        public Sys_ManagerGroup GetModelByGroupName(string GroupName)
        {
            return GetModelAll<Sys_ManagerGroup>("GroupName=@GroupName", new { GroupName = GroupName });
        }

        /// <summary>
        /// 修改管理员组时获取用户组数据
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="GroupName"></param>
        /// <returns></returns>
        public List<Sys_ManagerGroup> GetUpdateList(string Id, string GroupName)
        {
            return GetListAll<Sys_ManagerGroup>("Id!=@Id and GroupName=@GroupName", null, new { Id = Id, GroupName = GroupName });
        }


        /// <summary>
        /// 删除用户组时检查用户组是否存在上下级
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public List<Sys_ManagerGroup> GetDeleteList(string Id)
        {
            return GetListAll<Sys_ManagerGroup>("ParentId=@ParentId and IsLocking=0 and IsDelete=0", null, new { ParentId = Id });
        }
    }
}
