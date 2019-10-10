using DapperBLL.BaseBLL;
using DapperCommonMethod.CommonConfig;
using DapperCommonMethod.CommonEnum;
using DapperCommonMethod.CommonMethod;
using DapperCommonMethod.CommonModel;
using DapperModel;
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

            if (menuModel == null)
            {
                return ReturnHelpMethod.ReturnError((int)HttpCodeEnum.Http_400);
            }

            return ReturnHelpMethod.ReturnSuccess((int)HttpCodeEnum.Http_200, new { data = menuModel });
        }

        /// <summary>
        /// 添加菜单里按钮权限数据
        /// </summary>
        /// <param name="addMenuPower"></param>
        /// <returns></returns>
        public ResultMsg AddMenuPower(AddMenuPowerRequest addMenuPower, Sys_Manager userModel)
        {
            List<Sys_Menu> MenuList = baseDALS.GetList<Sys_Menu>(Sys_MenuSql.selectMenuPowerSql, null, addMenuPower);
            Sys_Menu NowMenuModel = baseDALS.GetModelById<Sys_Menu>(addMenuPower.MenuId);

            if (MenuList.Count <= 0 || NowMenuModel == null)
            {
                return ReturnHelpMethod.ReturnError((int)HttpCodeEnum.Http_400);
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
                OperateDepict = string.Format(LogDescribeConfig.AddDescribe, userModel.Name, DateTime.Now.ToString(), "菜单" + NowMenuModel.FullName + "的" + addMenuPower.PowerName + "按钮权限"),
            };


            return ReturnHelpMethod.ReturnSuccess((int)HttpCodeEnum.Http_200);
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
                var CounList = menuList.Where(p => p.ParentId == parentid).ToList();

                Sys_MenuViewModel parten = menuList.Find(p => p.GuId == parentid);

                List<Sys_MenuViewModel> menus = new List<Sys_MenuViewModel>();
                foreach (var item in CounList)
                {
                    menus.Add(item);
                    parten.children = menus;
                    GetMenuListNew(menuList, sumlist, item.GuId);
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
