using DapperBLL.BaseBLL;
using DapperBLL.CommonBLL;
using DapperCommonMethod.CommonConfig;
using DapperCommonMethod.CommonEnum;
using DapperCommonMethod.CommonMethod;
using DapperCommonMethod.CommonModel;
using DapperDAL;
using DapperHelp.Dapper;
using DapperModel;
using DapperModel.DataModel;
using DapperModel.ViewModel;
using DapperModel.ViewModel.DBViewModel;
using DapperModel.ViewModel.RequestModel;
using DapperSql.Sys_Sql;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DapperBLL.Sys_BLL
{
    /// <summary>
    /// 菜单业务层
    /// </summary>
    public class MenuBLL : BaseBLLS
    {
        private MenuDAL menuDAL = new MenuDAL();
        private DapperHelps dapperHelps = new DapperHelps();

        /// <summary>
        /// 添加菜单信息
        /// </summary>
        /// <param name="menuModel"></param>
        /// <param name="userModel"></param>
        /// <returns></returns>
        public ResultMsg AddMenuInfo(Sys_Menu menuModel, Sys_Manager userModel)
        {
            if (!userModel.IsDefault)
            {
                return ReturnHelpMethod.ReturnWarning((int)HttpCodeEnum.Http_1017);
            }

            //检查菜单数据是否存在
            List<Sys_Menu> MenuList = menuDAL.GetMenuIsExistList(menuModel);
            if (MenuList.Count > 0)
            {
                if (menuModel.ParentId == "0" && MenuList.Find(p => p.GuId == menuModel.ParentId) == null)
                {
                    return ReturnHelpMethod.ReturnWarning((int)HttpCodeEnum.Http_1024);
                }
                if (MenuList.Find(p => p.FullName == menuModel.FullName) != null)
                {
                    return ReturnHelpMethod.ReturnWarning((int)HttpCodeEnum.Http_1025);
                }
                if (MenuList.Find(p => p.AddressUrl == menuModel.AddressUrl) != null)
                {
                    return ReturnHelpMethod.ReturnWarning((int)HttpCodeEnum.Http_1026);
                }
            }

            Sys_Menu MenuModel = new Sys_Menu()
            {
                GuId = menuModel.GuId,
                ParentId = menuModel.ParentId,
                ResourceType = (int)ResourceTypeEnum.Menu,
                FullName = menuModel.FullName,
                Layers = 1,
                AddressUrl = menuModel.AddressUrl,
                IconUrl = menuModel.IconUrl,
                IsShow = menuModel.IsShow,
                Sort = menuModel.Sort,
                IsDefault = true,
                AddUserId = userModel.Id,
                AddTime = DateTime.Now,
                UpdateTime = DateTime.Now,
                IsDelete = false,
                Remarks = menuModel.Remarks
            };

            L_AdminOperateLog adminOperateLogModel = new L_AdminOperateLog()
            {
                Id = Guid.NewGuid().ToString(),
                AdminId = userModel.Id,
                AdminName = userModel.Name,
                OperateTime = DateTime.Now,
                OperateType = (int)OperateEnum.Add,
                OperateDepict = string.Format(LogDescribeConfig.AddDescribe, userModel.Name, DateTime.Now.ToString(), "菜单" + menuModel.FullName),
            };

            Commonredis.ListSet(RedisPrefixConfig.RedisMenuList, MenuList, TimeSpan.FromHours(24));

            menuDAL.AddMenuThing(MenuModel, adminOperateLogModel);

            return ReturnHelpMethod.ReturnSuccess((int)HttpCodeEnum.Http_Add_600);
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="guId"></param>
        /// <returns></returns>
        public ResultMsg DeleteMenu(string guId, Sys_Manager userModel)
        {
            if (!userModel.IsDefault)
            {
                return ReturnHelpMethod.ReturnWarning((int)HttpCodeEnum.Http_1017);
            }

            Sys_Menu menuModel = menuDAL.GetModelAll<Sys_Menu>("GuId=@GuId", new { GuId = guId });
            if (menuModel == null) 
            {
                return ReturnHelpMethod.ReturnWarning((int)HttpCodeEnum.Http_400);
            }

            List<Sys_Menu> menuList = baseDALS.GetListAll<Sys_Menu>("ParentId=@ParentId", null, new { ParentId = guId });
            if (menuList.Count > 0)
            {
                return ReturnHelpMethod.ReturnWarning((int)HttpCodeEnum.Http_1027);
            }

            L_AdminOperateLog adminOperateLogModel = new L_AdminOperateLog()
            {
                Id = Guid.NewGuid().ToString(),
                AdminId = userModel.Id,
                AdminName = userModel.Name,
                OperateTime = DateTime.Now,
                OperateType = (int)OperateEnum.Delete,
                OperateDepict = string.Format(LogDescribeConfig.DeleteDescribe, userModel.Name, DateTime.Now.ToString(), "菜单" + menuModel.FullName),
            };

            using (var tran = dapperHelps.GetOpenConnection().BeginTransaction())
            {
                dapperHelps.DeleteModel(menuModel, tran);

                dapperHelps.ExecuteInsertGuid(adminOperateLogModel, tran);

                tran.Commit();
            }

            return ReturnHelpMethod.ReturnSuccess((int)HttpCodeEnum.Http_Delete_604);
        }

        /// <summary>
        /// 修改菜单信息
        /// </summary>
        /// <param name="menuModel"></param>
        /// <param name="userModel"></param>
        /// <returns></returns>
        public ResultMsg UpdateMenu(Sys_Menu menuModel, Sys_Manager userModel)
        {
            if (!userModel.IsDefault)
            {
                return ReturnHelpMethod.ReturnWarning((int)HttpCodeEnum.Http_1017);
            }

            Sys_Menu formerMenuModel = baseDALS.GetModelAll<Sys_Menu>("GuId=@GuId", new { GuId = menuModel.GuId });
            if (formerMenuModel == null)
            {
                return ReturnHelpMethod.ReturnWarning((int)HttpCodeEnum.Http_400);
            }

            if (formerMenuModel.ParentId == "0" && menuModel.ParentId != "0")
            {
                return ReturnHelpMethod.ReturnWarning((int)HttpCodeEnum.Http_1028);
            }

            formerMenuModel.ParentId = menuModel.ParentId;
            formerMenuModel.FullName = menuModel.FullName;
            formerMenuModel.AddressUrl = menuModel.AddressUrl;
            formerMenuModel.IconUrl = menuModel.IconUrl;
            formerMenuModel.IsShow = menuModel.IsShow;
            formerMenuModel.Sort = menuModel.Sort;
            formerMenuModel.Remarks = menuModel.Remarks;

            L_AdminOperateLog adminOperateLogModel = new L_AdminOperateLog()
            {
                Id = Guid.NewGuid().ToString(),
                AdminId = userModel.Id,
                AdminName = userModel.Name,
                OperateTime = DateTime.Now,
                OperateType = (int)OperateEnum.Update,
                OperateDepict = string.Format(LogDescribeConfig.UpdateDescribe, userModel.Name, DateTime.Now.ToString(), "菜单" + menuModel.FullName),
            };

            using (var tran = dapperHelps.GetOpenConnection().BeginTransaction())
            {
                dapperHelps.UpdateModel(formerMenuModel, tran);

                dapperHelps.ExecuteInsertGuid(adminOperateLogModel, tran);

                tran.Commit();
            }

            return ReturnHelpMethod.ReturnSuccess((int)HttpCodeEnum.Http_Update_602);
        }

        /// <summary>
        /// 根据用户角色获取对应的菜单信息
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        public ResultMsg GetMenuList(Sys_Manager userModel)
        {
            //根据角色查询不同的菜单权限
            List<Sys_RolePurview> rolePurviewsList = baseDALS.GetListAll<Sys_RolePurview>("RoleId=@RoleId", null, new { RoleId = userModel.RelationId });
            string[] menuarray = rolePurviewsList.Select(p => p.ResourceId).ToArray();

            //菜单超过三十个需要手动分页查询

            if (rolePurviewsList.Count <= 0)
            {
                return ReturnHelpMethod.ReturnWarning((int)HttpCodeEnum.Http_404);
            }

            //查询当前用户的菜单权限
            GetRedisDataBLL redisDataBLL = new GetRedisDataBLL();
            var menuAllList = redisDataBLL.GetAllMenuList(RedisPrefixConfig.RedisMenuList, menuarray);
            List<Sys_MenuViewModel> menuList = menuAllList.Where(p => menuarray.Contains(p.GuId)).ToList();

            return ReturnHelpMethod.ReturnSuccess((int)HttpCodeEnum.Http_200, new { data = menuList });
        }

        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <returns></returns>
        public ResultMsg GetAllMenuList()
        {
            //查询当前用户的菜单权限
            GetRedisDataBLL redisDataBLL = new GetRedisDataBLL();
            List<Sys_MenuViewModel> menuList = redisDataBLL.GetAllMenuList(RedisPrefixConfig.RedisMenuList, null);

            return ReturnHelpMethod.ReturnSuccess((int)HttpCodeEnum.Http_200, new { data = menuList });
        }

        /// <summary>
        /// 获取单个菜单信息
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public ResultMsg GetMenuModel(string guid)
        {
            Sys_Menu menuModel = baseDALS.GetModel<Sys_Menu>(Sys_MenuSql.getmodel, new { Guid = guid });

            List<Sys_Menu> menuList = baseDALS.GetListAll<Sys_Menu>("ParentId=@ParentId and ResourceType=@ResourceType", null, new { ParentId = guid, ResourceType = (int)ResourceTypeEnum.Button });

            if (menuModel == null)
            {
                return ReturnHelpMethod.ReturnError((int)HttpCodeEnum.Http_400);
            }

            MenuReturnModel menuViewModel = new MenuReturnModel()
            {
                MenuModel = menuModel,
                MenuPowerList = menuList,
            };

            return ReturnHelpMethod.ReturnSuccess((int)HttpCodeEnum.Http_200, new { data = menuViewModel });
        }

        /// <summary>
        /// 添加菜单里按钮权限数据
        /// </summary>
        /// <param name="addMenuPower"></param>
        /// <returns></returns>
        public ResultMsg AddMenuPower(AddMenuPowerRequest addMenuPower, Sys_Manager userModel)
        {
            if (!userModel.IsDefault)
            {
                return ReturnHelpMethod.ReturnWarning((int)HttpCodeEnum.Http_1017);
            }

            List<Sys_Menu> MenuList = baseDALS.GetList<Sys_Menu>(Sys_MenuSql.selectMenuPowerSql, null, addMenuPower);

            Sys_Menu NowMenuModel = baseDALS.GetModelAll<Sys_Menu>("GuId=@GuId", new { GuId = addMenuPower.ParentId });
            if (NowMenuModel == null)
            {
                return ReturnHelpMethod.ReturnWarning((int)HttpCodeEnum.Http_400);
            }

            //检查按钮是否存在
            if (MenuList.Find(p => p.FullName == addMenuPower.FullName) != null)
            {
                return ReturnHelpMethod.ReturnWarning((int)HttpCodeEnum.Http_1022);
            }

            if (MenuList.Find(p => p.Purview == addMenuPower.Purview) != null)
            {
                return ReturnHelpMethod.ReturnWarning((int)HttpCodeEnum.Http_1023);
            }

            Sys_Menu MenuModel = new Sys_Menu()
            {
                GuId = Guid.NewGuid().ToString(),
                ParentId = addMenuPower.ParentId,
                ResourceType = (int)ResourceTypeEnum.Button,
                FullName = addMenuPower.FullName,
                Layers = 1,
                IconUrl = String.Empty,
                AddressUrl = addMenuPower.RequestUrl,
                Purview = addMenuPower.Purview,
                IsShow = true,
                IsDefault = true,
                AddUserId = userModel.Id,
                AddTime = DateTime.Now,
                UpdateTime = DateTime.Now,
                IsDelete = false,
            };

            L_AdminOperateLog adminOperateLogModel = new L_AdminOperateLog()
            {
                Id = Guid.NewGuid().ToString(),
                AdminId = userModel.Id,
                AdminName = userModel.Name,
                OperateTime = DateTime.Now,
                OperateType = (int)OperateEnum.Add,
                OperateDepict = string.Format(LogDescribeConfig.AddDescribe, userModel.Name, DateTime.Now.ToString(), "菜单" + NowMenuModel.FullName + "的" + addMenuPower.FullName + "权限"),
            };

            using (var tran = dapperHelps.GetOpenConnection().BeginTransaction())
            {
                dapperHelps.ExecuteInsert(MenuModel, tran);

                dapperHelps.ExecuteInsertGuid(adminOperateLogModel, tran);

                tran.Commit();
            }

            return ReturnHelpMethod.ReturnSuccess((int)HttpCodeEnum.Http_200);
        }

        /// <summary>
        /// 删除按钮权限
        /// </summary>
        /// <param name="guid"></param>
        /// <param name="userModel"></param>
        /// <returns></returns>
        public ResultMsg DeleteMenuPower(string guid, Sys_Manager userModel)
        {
            if (!userModel.IsDefault)
            {
                return ReturnHelpMethod.ReturnWarning((int)HttpCodeEnum.Http_1017);
            }

            Sys_Menu NowMenuModel = baseDALS.GetModelAll<Sys_Menu>("GuId=@GuId", new { GuId = guid });
            if (NowMenuModel == null)
            {
                return ReturnHelpMethod.ReturnWarning((int)HttpCodeEnum.Http_400);
            }

            L_AdminOperateLog adminOperateLogModel = new L_AdminOperateLog()
            {
                Id = Guid.NewGuid().ToString(),
                AdminId = userModel.Id,
                AdminName = userModel.Name,
                OperateTime = DateTime.Now,
                OperateType = (int)OperateEnum.Delete,
                OperateDepict = string.Format(LogDescribeConfig.DeleteDescribe, userModel.Name, DateTime.Now.ToString(), "" + NowMenuModel.FullName + "权限"),
            };

            using (var tran = dapperHelps.GetOpenConnection().BeginTransaction())
            {
                dapperHelps.DeleteModel(NowMenuModel, tran);

                dapperHelps.ExecuteInsertGuid(adminOperateLogModel, tran);

                tran.Commit();
            }

            return ReturnHelpMethod.ReturnSuccess((int)HttpCodeEnum.Http_Delete_604);
        }

        /// <summary>
        /// 修改按钮权限操作
        /// </summary>
        /// <param name="addMenuPower"></param>
        /// <param name="userModel"></param>
        /// <returns></returns>
        public ResultMsg UpdateMenuPower(AddMenuPowerRequest addMenuPower, Sys_Manager userModel)
        {
            Sys_Menu NowMenuModel = baseDALS.GetModelAll<Sys_Menu>("GuId=@GuId", new { GuId = addMenuPower.GuId });
            if (NowMenuModel == null)
            {
                return ReturnHelpMethod.ReturnWarning((int)HttpCodeEnum.Http_400);
            }

            NowMenuModel.FullName = addMenuPower.FullName;
            NowMenuModel.Purview = addMenuPower.Purview;
            NowMenuModel.UpdateTime = DateTime.Now;
            NowMenuModel.UpdateUserId = userModel.Id;

            bool bo = baseDALS.UpdateModel<Sys_Menu>(NowMenuModel);

            return bo ? ReturnHelpMethod.ReturnSuccess((int)HttpCodeEnum.Http_Update_602) : ReturnHelpMethod.ReturnError((int)HttpCodeEnum.Http_Update_603);
        }

        /// <summary>
        /// 获取单个菜单按钮数据
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public ResultMsg GetMenuPower(string guid)
        {
            Sys_Menu NowMenuModel = baseDALS.GetModelAll<Sys_Menu>("GuId=@GuId", new { GuId = guid });
            if (NowMenuModel == null)
            {
                return ReturnHelpMethod.ReturnWarning((int)HttpCodeEnum.Http_400);
            }

            return ReturnHelpMethod.ReturnSuccess((int)HttpCodeEnum.Http_200, new { data = NowMenuModel });
        }
    }
}
