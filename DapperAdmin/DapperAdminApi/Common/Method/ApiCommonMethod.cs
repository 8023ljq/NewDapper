using DapperModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DapperAdminApi.Common.Method
{
    public class ApiCommonMethod
    {
        /// <summary>
        /// 递归获取菜单集合
        /// </summary>
        /// <param name="menuList">上合集</param>
        /// <param name="sumlist">下级合集</param>
        /// <param name="parentid"></param>
        /// <returns></returns>
        public static List<Sys_Menu> GetMenuListNew(List<Sys_Menu> menuList, List<Sys_Menu> sumlist, string parentid)
        {
            if (!string.IsNullOrEmpty(parentid))
            {
                var CounList = menuList.Where(p => p.ParentId == parentid).ToList();

                Sys_Menu parten = menuList.Find(p => p.Id == parentid);

                List<Sys_Menu> menus = new List<Sys_Menu>();
                foreach (var item in CounList)
                {
                    menus.Add(item);
                   // parten.children = menus;
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