using DapperCommonMethod.CommonEnum;
using DapperCommonMethod.CommonMethod;
using DapperCommonMethod.CommonModel;
using DapperDAL;
using DapperModel.CommonModel;
using DapperModel.DataModel;
using DapperModel.ViewModel;
using DapperModel.ViewModel.RequestModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DapperBLL
{
    /// <summary>
    /// 管理员组业务层
    /// </summary>
    public class ManagerGroupBLL 
    {
        private ManagerGroupDAL managerGroupDAL = new ManagerGroupDAL();

        /// <summary>
        /// 获取所有用户组
        /// </summary>
        /// <param name="selectModel"></param>
        /// <returns></returns>
        public ResultMsg GetManagerGroupList(SelectModel selectModel)
        {
            List<Sys_ManagerGroup> managersList = managerGroupDAL.GetManagerGroupList(selectModel);

            List<Sys_ManagerGroup> ManagerGroupList = new List<Sys_ManagerGroup>();
            ManagerGroupList = GetManagerGroupListNew(managersList, ManagerGroupList, null);

            return ReturnHelpMethod.ReturnSuccess((int)HttpCodeEnum.Http_200, new { data = ManagerGroupList, pageModel = selectModel });
        }

        /// <summary>
        /// 添加用户组操作
        /// </summary>
        /// <param name="managerGroup"></param>
        /// <returns></returns>
        public ResultMsg AddManagerGroup(AddManagerGroupRequest managerGroup)
        {
            string ParentId = String.Empty;

            switch (managerGroup.AddType)
            {
                case 1://添加组
                    if (String.IsNullOrEmpty(managerGroup.GroupName))
                    {
                        return ReturnHelpMethod.ReturnError((int)HttpCodeEnum.Http_1000);
                    }
                    ParentId = "0";
                    break;
                case 2://添加子级
                    if (String.IsNullOrEmpty(managerGroup.ParentId))
                    {
                        return ReturnHelpMethod.ReturnError((int)HttpCodeEnum.Http_1000);
                    }
                    var GroupModel = managerGroupDAL.GetModelById<Sys_ManagerGroup>(managerGroup.ParentId);
                    if (GroupModel == null)
                    {
                        return ReturnHelpMethod.ReturnError((int)HttpCodeEnum.Http_1014);
                    }
                    ParentId = managerGroup.ParentId;
                    break;
                default:
                    break;
            }

            var managerGroupModel = managerGroupDAL.GetModelByGroupName(managerGroup.GroupName);

            if (managerGroupModel != null)
            {
                return ReturnHelpMethod.ReturnError((int)HttpCodeEnum.Http_1013);
            }

            Sys_ManagerGroup AddmanagerGroup = new Sys_ManagerGroup();
            AddmanagerGroup.Id = Guid.NewGuid().ToString();
            AddmanagerGroup.ParentId = ParentId;
            AddmanagerGroup.GroupName = managerGroup.GroupName;
            AddmanagerGroup.AddUserId = managerGroup.AddUserId;
            AddmanagerGroup.AddTime = DateTime.Now;
            AddmanagerGroup.IsLocking = false;
            AddmanagerGroup.IsDelete = false;
            AddmanagerGroup.Remarks = managerGroup.Remarks;

            string Id = managerGroupDAL.InsertModelGuid<Sys_ManagerGroup>(AddmanagerGroup);

            return !String.IsNullOrEmpty(Id) ? ReturnHelpMethod.ReturnSuccess((int)HttpCodeEnum.Http_Add_600) : ReturnHelpMethod.ReturnError((int)HttpCodeEnum.Http_Add_601);
        }

        /// <summary>
        /// 获取用户组详细信息
        /// </summary>
        /// <returns></returns>
        public ResultMsg GetManagerGroup(string groupid)
        {
            if (String.IsNullOrEmpty(groupid))
            {
                return ReturnHelpMethod.ReturnError((int)HttpCodeEnum.Http_1000);
            }

            var managerGroup = managerGroupDAL.GetModelById<Sys_ManagerGroup>(groupid);

            if (managerGroup == null)
            {
                return ReturnHelpMethod.ReturnError((int)HttpCodeEnum.Http_400);
            }

            return ReturnHelpMethod.ReturnSuccess((int)HttpCodeEnum.Http_200, new { data = managerGroup });
        }

        /// <summary>
        /// 修改用户组信息
        /// </summary>
        /// <returns></returns>
        public ResultMsg UpdateManagerGroup(AddManagerGroupRequest managerGroup)
        {
            if (String.IsNullOrEmpty(managerGroup.Id))
            {
                return ReturnHelpMethod.ReturnError((int)HttpCodeEnum.Http_1000);
            }

            Sys_ManagerGroup ManagerGroupModel = managerGroupDAL.GetModelById<Sys_ManagerGroup>(managerGroup.Id);
            if (ManagerGroupModel == null)
            {
                return ReturnHelpMethod.ReturnError((int)HttpCodeEnum.Http_1014);
            }

            var GroupList = managerGroupDAL.GetUpdateList(managerGroup.Id, managerGroup.GroupName);
            if (GroupList.Count() > 0)
            {
                return ReturnHelpMethod.ReturnError((int)HttpCodeEnum.Http_1013);
            }

            ManagerGroupModel.ParentId = managerGroup.ParentId;
            ManagerGroupModel.GroupName = managerGroup.GroupName;
            ManagerGroupModel.Remarks = managerGroup.Remarks;
            ManagerGroupModel.UpdateTime = DateTime.Now;
            ManagerGroupModel.UpdateUserId = managerGroup.AddUserId;

            bool bo = managerGroupDAL.UpdateModel<Sys_ManagerGroup>(ManagerGroupModel);

            return bo ? ReturnHelpMethod.ReturnSuccess((int)HttpCodeEnum.Http_Update_602) : ReturnHelpMethod.ReturnError((int)HttpCodeEnum.Http_Update_603);
        }

        /// <summary>
        /// 删除用户组信息
        /// </summary>
        /// <returns></returns>
        public ResultMsg DeleteManagerGroup(string groupid)
        {
            if (String.IsNullOrEmpty(groupid))
            {
                return ReturnHelpMethod.ReturnError((int)HttpCodeEnum.Http_1000);
            }

            var managerGroup = managerGroupDAL.GetModelById<Sys_ManagerGroup>(groupid);

            if (managerGroup == null)
            {
                return ReturnHelpMethod.ReturnError((int)HttpCodeEnum.Http_400);
            }

            var managerGroupList = managerGroupDAL.GetDeleteList(managerGroup.Id);

            if (managerGroupList.Count() > 0)
            {
                return ReturnHelpMethod.ReturnError((int)HttpCodeEnum.Http_1015);
            }

            managerGroup.IsLocking = true;

            bool bo = managerGroupDAL.UpdateModel<Sys_ManagerGroup>(managerGroup);

            return bo ? ReturnHelpMethod.ReturnSuccess((int)HttpCodeEnum.Http_Delete_604) : ReturnHelpMethod.ReturnError((int)HttpCodeEnum.Http_Delete_605);
        }

        /// <summary>
        /// 获取用户组下拉框列表
        /// </summary>
        /// <returns></returns>
        public ResultMsg GetGroupSelectList()
        {
            ResultMsg resultMsg = new ResultMsg();

            List<Sys_ManagerGroup> ManagerRoleList = managerGroupDAL.GetDeleteList("0");

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
        /// 递归获取用户组
        /// </summary>
        /// <param name="menuList"></param>
        /// <param name="sumlist"></param>
        /// <param name="parentid"></param>
        /// <returns></returns>
        public List<Sys_ManagerGroup> GetManagerGroupListNew(List<Sys_ManagerGroup> menuList, List<Sys_ManagerGroup> sumlist, string parentid)
        {
            if (!string.IsNullOrEmpty(parentid))
            {
                var CounList = menuList.Where(p => p.ParentId == parentid).ToList();

                Sys_ManagerGroup parten = menuList.Find(p => p.Id == parentid);

                List<Sys_ManagerGroup> menus = new List<Sys_ManagerGroup>();
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
