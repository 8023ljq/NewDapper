using DapperBLL.BaseBLL;
using DapperCommonMethod.CommonEnum;
using DapperCommonMethod.CommonMethod;
using DapperCommonMethod.CommonModel;
using DapperModel;
using DapperModel.ViewModel.DBViewModel;
using DapperSql.Sys_Sql;
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
            List<Sys_MenuViewModel> menuList = baseDALS.GetList<Sys_MenuViewModel>(Sys_MenuSql.selectListSql, new { IsDelete = 0 });
            List<Sys_MenuViewModel> orderlist = new List<Sys_MenuViewModel>();
            orderlist = GetMenuListNew(menuList, orderlist, null);

            return ReturnHelpMethod.ReturnSuccess((int)HttpCodeEnum.Http_200, new { data = orderlist });
        }

        /// <summary>
        /// 获取单个菜单信息
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public ResultMsg GetMenuModel(string menuId)
        {
            Sys_Menu menuModel = baseDALS.GetModelById<Sys_Menu>(menuId);

            if (menuModel == null)
            {
                return ReturnHelpMethod.ReturnError((int)HttpCodeEnum.Http_400);
            }

            return ReturnHelpMethod.ReturnSuccess((int)HttpCodeEnum.Http_200, new { data = menuModel });
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

                Sys_MenuViewModel parten = menuList.Find(p => p.Id == parentid);

                List<Sys_MenuViewModel> menus = new List<Sys_MenuViewModel>();
                foreach (var item in CounList)
                {
                    menus.Add(item);
                    parten.children = menus;
                    GetMenuListNew(menuList, sumlist, item.Id);
                }
            }
            else
            {
                var CounList = menuList.Where(p => p.ParentId == "0").ToList();
                foreach (var item in CounList)
                {
                    sumlist.Add(item);
                    GetMenuListNew(menuList, sumlist, item.Id);
                }
            }
            return sumlist;
        }
    }
}
