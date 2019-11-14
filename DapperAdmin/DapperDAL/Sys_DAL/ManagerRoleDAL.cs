
using DapperModel.CommonModel;
using DapperModel.DataModel;
using DapperSql.Sys_Sql;
using System;
using System.Collections.Generic;

namespace DapperDAL
{
    /// <summary>
    /// 获取管理员角色下拉框列表
    /// </summary>
    public class ManagerRoleDAL : BaseDALS
    {
        /// <summary>
        /// 获取所有管理员列表
        /// </summary>
        /// <param name="IsDefault"></param>
        /// <param name="RelationId"></param>
        /// <returns></returns>
        public List<Sys_ManagerRole> GetManagerRoleList(bool IsDefault, string RelationId)
        {
            string whereStr = $"IsDelete=@IsDelete";

            if (IsDefault)
            {
                whereStr += " and Id=@Id";
            }
            return GetListAll<Sys_ManagerRole>(whereStr, null, new { IsDelete = 0, Id = RelationId });
        }

        /// <summary>
        /// 获取管理员分页列表
        /// </summary>
        /// <param name="selectModel"></param>
        /// <returns></returns>
        public List<Sys_ManagerRole> GetManagerRolePageList(SelectModel selectModel)
        {
            string sql = Sys_ManagerRoleSql.getPageList;

            if (!String.IsNullOrEmpty(selectModel.Keyword))
            {
                sql += $@" and A.RoleName=@Keyword";
            }

            return GetPageJoinList<Sys_ManagerRole>(sql, selectModel);
        }

        /// <summary>
        /// 通过管理员获取管理员集合数据
        /// </summary>
        /// <param name="RoleName"></param>
        /// <returns></returns>
        public List<Sys_ManagerRole> GetManagerRoleModel(string RoleName)
        {
            return GetListAll<Sys_ManagerRole>("RoleName=@RoleName", null, new { RoleName = RoleName });
        }

        /// <summary>
        /// 修改管理员权限信息
        /// </summary>
        /// <param name="rolePurviewsList"></param>
        /// <param name="existMuttonPowersList"></param>
        /// <param name="rolePurviewList"></param>
        /// <param name="menuButtonPowerList"></param>
        public void UpdateManagerRole(List<Sys_RolePurview> rolePurviewsList, List<Sys_MenuButtonPower> existMuttonPowersList, List<Sys_RolePurview> rolePurviewList, List<Sys_MenuButtonPower> menuButtonPowerList)
        {
            using (var tran = dapperHelps.GetOpenConnection().BeginTransaction())
            {
                if (rolePurviewsList.Count > 0)
                {
                    dapperHelps.DeleteList(rolePurviewsList, tran);
                }

                if (existMuttonPowersList.Count > 0)
                {
                    dapperHelps.DeleteList(existMuttonPowersList, tran);
                }

                dapperHelps.ExecuteInsertList(rolePurviewList, tran);

                dapperHelps.ExecuteInsertList(menuButtonPowerList, tran);

                tran.Commit();
            }
        }

        /// <summary>
        /// 删除管理员权限信息
        /// </summary>
        /// <param name="managerRoleModel"></param>
        /// <param name="ExistRolePurviewList"></param>
        public void DeleteManagerRole(Sys_ManagerRole managerRoleModel , List<Sys_RolePurview> ExistRolePurviewList)
        {
            using (var tran = dapperHelps.GetOpenConnection().BeginTransaction())
            {
                dapperHelps.DeleteModel(managerRoleModel, tran);

                dapperHelps.DeleteList(ExistRolePurviewList, tran);
                tran.Commit();
            }
        }

    }
}
