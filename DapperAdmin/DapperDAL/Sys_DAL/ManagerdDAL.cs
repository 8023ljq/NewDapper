using DapperModel.CommonModel;
using DapperModel.DataModel;
using DapperSql.Sys_Sql;
using System;
using System.Collections.Generic;

namespace DapperDAL
{
    /// <summary>
    /// 管理员数据访问层
    /// </summary>
    public class ManagerdDAL : BaseDALS
    {
        /// <summary>
        /// 获取管理员列表数据
        /// </summary>
        /// <param name="selectModel"></param>
        /// <returns></returns>
        public List<Sys_Manager> GetManagerList(SelectModel selectModel)
        {
            string sql = Sys_ManagerSql.getPageList;

            if (!String.IsNullOrEmpty(selectModel.Keyword))
            {
                sql += $@" and (A.Name like @Keyword OR A.Nickname like @Keyword OR A.Phone like @Keyword OR A.Email like @Keyword OR A.LastLoginIP like @Keyword)";
            }

            return GetPageJoinList<Sys_Manager>(sql, selectModel); 
        }

        /// <summary>
        /// 添加时获取管理员列表数据(检查是否存在)
        /// </summary>
        /// <param name="managerModel"></param>
        /// <returns></returns>
        public List<Sys_Manager> GetManagerList(Sys_Manager managerModel)
        {
            return GetListAll<Sys_Manager>("(Name=@Name or Nickname=@Nickname or Phone=@Phone or Email=@Email)", null, managerModel);
        }

        /// <summary>
        /// 通过关联ID查询管理员列表(删除角色时检查是否绑定管理员)
        /// </summary>
        /// <returns></returns>
        public List<Sys_Manager> GetManagerListByRelationId(string RelationId)
        {
           return GetListAll<Sys_Manager>("RelationId=@RelationId", null, new { RelationId = RelationId });
        }
    }
}
