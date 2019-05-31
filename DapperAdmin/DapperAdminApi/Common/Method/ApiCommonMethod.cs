using DapperCommonMethod.DBModel.Sys_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DapperAdminApi.Common.Method
{
    public class ApiCommonMethod
    {
        /// <summary>
        /// 递归获取菜单集合
        /// </summary>
        /// <param name="menuList"></param>
        /// <returns></returns>
        public static List<Sys_Menu> GetMenuList(List<Sys_Menu> menuList, string parentid = "")
        {
            List<Sys_Menu> menusList = new List<Sys_Menu>();

            if (menuList.Count > 0)
            {
                foreach (var item in menuList)
                {
                    menusList.Add(item);
                    var CounList = menuList.Where(p => p.ParentId == item.Id).ToList();
                    if (CounList.Count > 0)
                    {
                        GetMenuList(CounList, item.ParentId);
                    }
                }
            }

            return menusList;
        }
    }
}