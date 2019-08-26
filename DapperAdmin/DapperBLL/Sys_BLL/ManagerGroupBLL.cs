using DapperBLL.BaseBLL;
using DapperCommonMethod.CommonEnum;
using DapperCommonMethod.CommonMethod;
using DapperCommonMethod.CommonModel;
using DapperModel.CommonModel;
using DapperModel.ViewModel.DBViewModel;
using DapperSql.Sys_Sql;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DapperBLL.Sys_BLL
{
    /// <summary>
    /// 管理员组业务层
    /// </summary>
    public class ManagerGroupBLL : BaseBLLS
    {
        /// <summary>
        /// 获取所有用户组的
        /// </summary>
        /// <param name="selectModel"></param>
        /// <returns></returns>
        public ResultMsg GetManagerGroupList(SelectModel selectModel)
        {
            List<Sys_ManagerGroupViewModel> managersList = baseDALS.GetList<Sys_ManagerGroupViewModel>(Sys_ManagerGroupSql.selectListSql, "order by A.AddTime desc", selectModel);

            List<Sys_ManagerGroupViewModel> ManagerGroupList = new List<Sys_ManagerGroupViewModel>();
            ManagerGroupList = GetManagerGroupListNew(managersList, ManagerGroupList, null);

            return ReturnHelpMethod.ReturnSuccess((int)HttpCodeEnum.Http_200, new { data = ManagerGroupList, pageModel= selectModel });
        }

        /// <summary>
        /// 递归获取用户组
        /// </summary>
        /// <param name="menuList"></param>
        /// <param name="sumlist"></param>
        /// <param name="parentid"></param>
        /// <returns></returns>
        public List<Sys_ManagerGroupViewModel> GetManagerGroupListNew(List<Sys_ManagerGroupViewModel> menuList, List<Sys_ManagerGroupViewModel> sumlist, string parentid)
        {
            if (!string.IsNullOrEmpty(parentid))
            {
                var CounList = menuList.Where(p => p.ParentId == parentid).ToList();

                Sys_ManagerGroupViewModel parten = menuList.Find(p => p.Id == parentid);

                List<Sys_ManagerGroupViewModel> menus = new List<Sys_ManagerGroupViewModel>();
                foreach (var item in CounList)
                {
                    menus.Add(item);
                    parten.children = menus;
                    GetManagerGroupListNew(menuList, sumlist, item.Id);
                }
            }
            else
            {
                var CounList = menuList.Where(p => p.ParentId == "0").ToList();
                foreach (var item in CounList)
                {
                    sumlist.Add(item);
                    GetManagerGroupListNew(menuList, sumlist, item.Id);
                }
            }

            return sumlist;
        }
    }
}
