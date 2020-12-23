using DapperBLL.BaseBLL;
using DapperCommonMethod.CommonEnum;
using DapperModel.ViewModel.DBViewModel;
using DapperSql.Sys_Sql;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DapperBLL.CommonBLL
{
    /// <summary>
    /// 获取Redis中缓存的数据
    /// </summary>
    public class GetRedisDataBLL : BaseBLLS
    {
        /// <summary>
        /// 获取所有缓存菜单数据
        /// </summary>
        /// <param name="redisPrefix">key前缀</param>
        /// <returns></returns>
        public List<Sys_MenuViewModel> GetAllMenuList(string redisPrefix, int MenuType, string[] menuarray = null)
        {
            List<Sys_MenuViewModel> menuViewModelsList = new List<Sys_MenuViewModel>();
            List<Sys_MenuViewModel> orderlist = new List<Sys_MenuViewModel>();

            //menuViewModelsList = CSRedis.ListGet<Sys_MenuViewModel>((int)CSRedisEnum.Administrator,redisPrefix);

            //if (menuViewModelsList.Count <= 0)
            //{
            //    menuViewModelsList = baseDALS.GetList<Sys_MenuViewModel>(Sys_MenuSql.selectListSql, "Layers,Sort desc", new { IsDelete = 0 });
            //    CSRedis.ListSet<Sys_MenuViewModel>((int)CSRedisEnum.Administrator,redisPrefix, menuViewModelsList, TimeSpan.FromHours(12));
            //}
              menuViewModelsList = baseDALS.GetList<Sys_MenuViewModel>(Sys_MenuSql.selectListSql, "Layers,Sort desc", new { IsDelete = 0 });

            if (MenuType == (int)MenuEnum.LeftSide)
            {
                menuViewModelsList= menuViewModelsList.Where(p => p.IsShow==true).ToList();
            }

            if (menuarray != null && menuarray.Length > 0)
            {
                menuViewModelsList = menuViewModelsList.Where(p => menuarray.Contains(p.GuId)).ToList();
                orderlist = GetMenuListNew(menuViewModelsList, orderlist, null);
            }
            else
            {
                orderlist = GetMenuListNew(menuViewModelsList, orderlist, null);
            }

            return orderlist;
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
                var MenuList = menuList.Where(p => p.ParentId == parentid && p.ResourceType == (int)ResourceTypeEnum.Menu).OrderByDescending(p=>p.Sort).ToList();

                var ButtonList = menuList.Where(p => p.ParentId == parentid && p.ResourceType == (int)ResourceTypeEnum.Button).OrderByDescending(p => p.AddTime).ToList();

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
