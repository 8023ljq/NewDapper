using DapperBLL.BaseBLL;
using DapperCommonMethod.CommonEnum;
using DapperCommonMethod.CommonMethod;
using DapperCommonMethod.CommonModel;
using DapperModel;
using DapperModel.CommonModel;
using DapperModel.ViewModel;
using DapperModel.ViewModel.DBViewModel;
using DapperModel.ViewModel.RequestModel;
using DapperSql.Sys_Sql;
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

            return ReturnHelpMethod.ReturnSuccess((int)HttpCodeEnum.Http_200, new { data = ManagerGroupList, pageModel = selectModel });
        }

        /// <summary>
        /// 获取用户组下拉框列表
        /// </summary>
        /// <returns></returns>
        public ResultMsg GetGroupSelectList()
        {
            ResultMsg resultMsg = new ResultMsg();

            List<Sys_ManagerGroup> ManagerRoleList = baseDALS.GetListAll<Sys_ManagerGroup>("IsDelete=@IsDelete and ParentId=@ParentId", null, new { IsDelete = 0, ParentId = "0" });

            List<SelectViewModel> RoleSelectViewList = new List<SelectViewModel>();
            if (ManagerRoleList.Count > 0)
            {
                foreach (var item in ManagerRoleList)
                {
                    RoleSelectViewList.Add(new SelectViewModel
                    {
                        value = item.Id,
                        label = item.GroupName,
                        disabled = item.IsDelete,
                    });
                }
            }

            return ReturnHelpMethod.ReturnSuccess((int)HttpCodeEnum.Http_200, new { data = RoleSelectViewList });
        }

        /// <summary>
        /// 添加用户组操作
        /// </summary>
        /// <param name="managerGroup"></param>
        /// <returns></returns>
        public ResultMsg AddManagerGroup(AddManagerGroupRequest managerGroup)
        {
            Sys_ManagerGroup managerGroupModel = baseDALS.GetModel<Sys_ManagerGroup>("GroupName=@GroupName", null, new { GroupName = managerGroup.GroupName });

            if (managerGroupModel != null)
            {
                return ReturnHelpMethod.ReturnError((int)HttpCodeEnum.Http_1013);
            }

            return ReturnHelpMethod.ReturnSuccess((int)HttpCodeEnum.Http_200);
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
