using DapperCommonMethod.CommonEnum;
using DapperModel.DataModel;
using DapperModel.ViewModel.RequestModel;
using DapperSql.Sys_Sql;
using System.Collections.Generic;

namespace DapperDAL
{
    /// <summary>
    /// 菜单数据处理层
    /// </summary>
    public class MenuDAL : BaseDALS
    {
        /// <summary>
        /// 获取菜单数据
        /// </summary>
        /// <param name="GuId"></param>
        /// <returns></returns>
        public Sys_Menu GetMenuModel(string GuId)
        {
            return GetModelAll<Sys_Menu>("GuId=@GuId", new { GuId = GuId });
        }

        /// <summary>
        /// 获取菜单下的按钮集合
        /// </summary>
        /// <param name="ParentId"></param>
        /// <returns></returns>
        public List<Sys_Menu> GetMenuPowerList(string ParentId, int ResourceType)
        {
            return GetListAll<Sys_Menu>("ParentId=@ParentId and ResourceType=@ResourceType", null, new { ParentId = ParentId, ResourceType = ResourceType });
        }

        /// <summary>
        /// 获取菜单下的按钮集合(检查按钮是否存在)
        /// </summary>
        /// <param name="ParentId"></param>
        /// <returns></returns>
        public List<Sys_Menu> GetMenuPowerList(AddMenuPowerRequest addMenuPower)
        {
            return GetList<Sys_Menu>(Sys_MenuSql.selectMenuPowerSql, null, addMenuPower);
        }

        /// <summary>
        /// 获取所有菜单数据(添加之前检查是否存在)
        /// </summary>
        /// <param name="menuModel"></param>
        /// <returns></returns>
        public List<Sys_Menu> GetMenuIsExistList(Sys_Menu menuModel)
        {
            return GetList<Sys_Menu>(Sys_MenuSql.selectMenuIsExist, null, menuModel);
        }

        /// <summary>
        /// 添加菜单数据
        /// </summary>
        /// <param name="MenuModel"></param>
        /// <param name="operateLogModel"></param>
        public void AddMenuThing(Sys_Menu MenuModel, L_AdminOperateLog operateLogModel, Sys_RolePurview rolePurviewModel)
        {
            using (var tran = dapperHelps.GetOpenConnection().BeginTransaction())
            {
                dapperHelps.ExecuteInsert(MenuModel, tran);

                dapperHelps.ExecuteInsertGuid(operateLogModel, tran);

                if (rolePurviewModel != null)
                {
                    dapperHelps.ExecuteInsertGuid(rolePurviewModel, tran);
                }

                tran.Commit();
            }
        }

        /// <summary>
        /// 删除菜单事物
        /// </summary>
        /// <param name="menuModel"></param>
        /// <param name="adminOperateLogModel"></param>
        public void DeleteMenuThing(Sys_Menu menuModel, L_AdminOperateLog adminOperateLogModel)
        {
            using (var tran = dapperHelps.GetOpenConnection().BeginTransaction())
            {
                dapperHelps.ExecuteDeleteModel(menuModel, tran);

                dapperHelps.ExecuteInsertGuid(adminOperateLogModel, tran);

                tran.Commit();
            }
        }

        /// <summary>
        /// 修改菜单事物
        /// </summary>
        /// <param name="MenuModel"></param>
        /// <param name="adminOperateLogModel"></param>
        public void UpdateMenuThing(Sys_Menu MenuModel, L_AdminOperateLog adminOperateLogModel)
        {
            using (var tran = dapperHelps.GetOpenConnection().BeginTransaction())
            {
                dapperHelps.ExecuteUpdateModel(MenuModel, tran);

                dapperHelps.ExecuteInsertGuid(adminOperateLogModel, tran);

                tran.Commit();
            }
        }

        /// <summary>
        /// 添加菜单里按钮权限事物
        /// </summary>
        /// <param name="MenuModel"></param>
        /// <param name="adminOperateLogModel"></param>
        public void AddMenuPowerThing(Sys_Menu MenuModel, L_AdminOperateLog adminOperateLogModel)
        {
            using (var tran = dapperHelps.GetOpenConnection().BeginTransaction())
            {
                dapperHelps.ExecuteInsert(MenuModel, tran);

                dapperHelps.ExecuteInsertGuid(adminOperateLogModel, tran);

                tran.Commit();
            }
        }

        /// <summary>
        /// 添加菜单里按钮权限事物
        /// </summary>
        /// <param name="MenuModel"></param>
        /// <param name="adminOperateLogModel"></param>
        public void DeleteMenuPowerThing(Sys_Menu MenuModel, L_AdminOperateLog adminOperateLogModel)
        {
            using (var tran = dapperHelps.GetOpenConnection().BeginTransaction())
            {
                dapperHelps.ExecuteDeleteModel(MenuModel, tran);

                dapperHelps.ExecuteInsertGuid(adminOperateLogModel, tran);

                tran.Commit();
            }
        }
    }
}
