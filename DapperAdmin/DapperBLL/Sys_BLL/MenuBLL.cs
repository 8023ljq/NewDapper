using DapperBLL.BaseBLL;
using DapperCommonMethod.CommonConfig;
using DapperCommonMethod.CommonEnum;
using DapperCommonMethod.CommonMethod;
using DapperCommonMethod.CommonModel;
using DapperHelp.Dapper;
using DapperModel;
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
            List<Sys_Menu> MenuList = baseDALS.GetList<Sys_Menu>(Sys_MenuSql.selectMenuIsExist, null, menuModel);
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

            using (var tran = dapperHelps.GetOpenConnection().BeginTransaction())
            {
                dapperHelps.ExecuteInsert<Sys_Menu>(MenuModel, tran);

                dapperHelps.ExecuteInsertGuid<L_AdminOperateLog>(adminOperateLogModel, tran);

                tran.Commit();
            }

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

            Sys_Menu menuModel = baseDALS.GetModelAll<Sys_Menu>("GuId=@GuId", new { GuId = guId });
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
        /// 获取菜单列表
        /// </summary>
        /// <returns></returns>
        public ResultMsg GetMenuList()
        {
            //查询当前用户的菜单权限
            List<Sys_MenuViewModel> menuList = baseDALS.GetList<Sys_MenuViewModel>(Sys_MenuSql.selectListSql, null, new { IsDelete = 0 });
            List<Sys_MenuViewModel> orderlist = new List<Sys_MenuViewModel>();
            orderlist = GetMenuListNew(menuList, orderlist, null);

            return ReturnHelpMethod.ReturnSuccess((int)HttpCodeEnum.Http_200, new { data = orderlist });
        }

        /// <summary>
        /// 获取单个菜单信息
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public ResultMsg GetMenuModel(string guid)
        {
            Sys_Menu menuModel = baseDALS.GetModel<Sys_Menu>(Sys_MenuSql.getmodel, null, new { Guid = guid });

            List<Sys_Menu> menuList = baseDALS.GetListAll<Sys_Menu>("ParentId=@ParentId and ResourceType=@ResourceType", null, new { ParentId = guid, ResourceType = (int)ResourceTypeEnum.Button });

            if (menuModel == null)
            {
                return ReturnHelpMethod.ReturnError((int)HttpCodeEnum.Http_400);
            }

            MenuViewModel menuViewModel = new MenuViewModel()
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

            Sys_Menu NowMenuModel = baseDALS.GetModelAll<Sys_Menu>("GuId=@GuId", new { GuId = addMenuPower.MenuId });
            if (NowMenuModel == null)
            {
                return ReturnHelpMethod.ReturnWarning((int)HttpCodeEnum.Http_400);
            }

            //检查按钮是否存在
            if (MenuList.Find(p => p.FullName == addMenuPower.PowerName) != null)
            {
                return ReturnHelpMethod.ReturnWarning((int)HttpCodeEnum.Http_1022);
            }

            if (MenuList.Find(p => p.Purview == addMenuPower.PowerMark) != null)
            {
                return ReturnHelpMethod.ReturnWarning((int)HttpCodeEnum.Http_1023);
            }

            Sys_Menu MenuModel = new Sys_Menu()
            {
                GuId = Guid.NewGuid().ToString(),
                ParentId = addMenuPower.MenuId,
                ResourceType = (int)ResourceTypeEnum.Button,
                FullName = addMenuPower.PowerName,
                Layers = 1,
                IconUrl = String.Empty,
                AddressUrl = addMenuPower.RequestUrl,
                Purview = addMenuPower.PowerMark,
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
                OperateDepict = string.Format(LogDescribeConfig.AddDescribe, userModel.Name, DateTime.Now.ToString(), "菜单" + NowMenuModel.FullName + "的" + addMenuPower.PowerName + "权限"),
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
        /// 递归获取菜单集合
        /// </summary>
        /// <param name="menuList">上合集</param>
        /// <param name="sumlist">下级合集</param>
        /// <param name="parentid"></param>
        /// <returns></returns>
        public static List<Sys_MenuViewModel> GetMenuListNew(List<Sys_MenuViewModel> menuList, List<Sys_MenuViewModel> sumlist, string parentid)
        {
            if (!string.IsNullOrEmpty(parentid))
            {
                var MenuList = menuList.Where(p => p.ParentId == parentid && p.ResourceType == (int)ResourceTypeEnum.Menu).ToList();

                var ButtonList = menuList.Where(p => p.ParentId == parentid && p.ResourceType == (int)ResourceTypeEnum.Button).ToList();

                Sys_MenuViewModel parten = menuList.Find(p => p.GuId == parentid);

                List<Sys_MenuViewModel> menus = new List<Sys_MenuViewModel>();
                foreach (var item in MenuList)
                {
                    menus.Add(item);
                    parten.children = menus;
                    GetMenuListNew(menuList, sumlist, item.GuId);
                }
                if (ButtonList.Count > 0)
                {
                    parten.Buttonchildren = ButtonList;
                }
            }
            else
            {
                var CounList = menuList.Where(p => p.ParentId == "0").ToList();
                foreach (var item in CounList)
                {
                    sumlist.Add(item);
                    GetMenuListNew(menuList, sumlist, item.GuId);
                }
            }
            return sumlist;
        }


    }
}
