using DapperBLL.BaseBLL;
using DapperCommonMethod.CommonEnum;
using DapperCommonMethod.CommonMethod;
using DapperCommonMethod.CommonModel;
using DapperDAL;
using DapperModel.CommonModel;
using DapperModel.DataModel;
using DapperModel.ViewModel;
using DapperModel.ViewModel.DBViewModel;
using DapperModel.ViewModel.RequestModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DapperBLL.Sys_BLL
{
    /// <summary>
    /// 管理员角色业务层
    /// </summary>
    public class ManagerRoledBLL : BaseBLLS
    {
        private ManagerRoleDAL managerRoleDAL = new ManagerRoleDAL();
        private RolePurviewDAL rolePurviewDAL = new RolePurviewDAL();
        private MenuButtonPowerDAL buttonPowerDAL = new MenuButtonPowerDAL();
        private ManagerdDAL managerdDAL = new ManagerdDAL();

        /// <summary>
        /// 获取管理员角色下拉框列表
        /// </summary>
        /// <returns></returns>
        public ResultMsg GetRoleSelectList(Sys_Manager ManagerModel)
        {
            List<Sys_ManagerRole> ManagerRoleList = managerRoleDAL.GetManagerRoleList(ManagerModel.IsDefault, ManagerModel.RelationId);

            List<SelectViewModel> RoleSelectViewList = new List<SelectViewModel>();
            if (ManagerRoleList.Count > 0)
            {
                foreach (var item in ManagerRoleList)
                {
                    RoleSelectViewList.Add(new SelectViewModel
                    {
                        value = item.Id,
                        label = item.RoleName,
                        disabled = item.IsDelete,
                    });
                }
            }

            return ReturnHelpMethod.ReturnSuccess((int)HttpCodeEnum.Http_200, new { data = RoleSelectViewList });
        }

        /// <summary>
        /// 获取所有管理员角色列表
        /// </summary>
        /// <param name="pageModel"></param>
        /// <returns></returns>
        public ResultMsg GetManagerRoleList(SelectModel selectModel)
        {
            List<Sys_ManagerRole> ManagerRoleList = managerRoleDAL.GetManagerRolePageList(selectModel);

            return ReturnHelpMethod.ReturnSuccess((int)HttpCodeEnum.Http_200, new { data = ManagerRoleList, pageModel = selectModel });
        }

        /// <summary>
        /// 添加角色信息
        /// </summary>
        /// <param name="addRoleRequestModel"></param>
        /// <param name="UserModel"></param>
        /// <returns></returns>
        public ResultMsg AddNewRole(AddRoleRequest addRoleRequestModel, Sys_Manager UserModel)
        {
            List<Sys_ManagerRole> ManagerRoleList = managerRoleDAL.GetManagerRoleModel(addRoleRequestModel.RoleName);

            if (ManagerRoleList.Count > 0)
            {
                return ReturnHelpMethod.ReturnWarning((int)HttpCodeEnum.Http_1016);
            }

            Sys_ManagerRole managerRoleModel = new Sys_ManagerRole()
            {
                Id = Guid.NewGuid().ToString(),
                RoleName = addRoleRequestModel.RoleName,
                RoleType = (int)RoleTypeEnum.System,
                IsDefault = false,
                AddUserId = UserModel.Id,
                AddTime = DateTime.Now,
                IsDelete = UserModel.IsDelete,
                Remarks = addRoleRequestModel.Remarks
            };

            string Id = managerRoleDAL.InsertModelGuid<Sys_ManagerRole>(managerRoleModel);

            if (!String.IsNullOrEmpty(Id))
            {
                return ReturnHelpMethod.ReturnSuccess((int)HttpCodeEnum.Http_Add_600);
            }
            else
            {
                return ReturnHelpMethod.ReturnError((int)HttpCodeEnum.Http_Add_601);
            }
        }

        /// <summary>
        /// 获取当前角色信息
        /// </summary>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        public ResultMsg SelectRoleModel(string RoleId)
        {
            Sys_ManagerRole managerRoleModel = new Sys_ManagerRole();

            if (!DataCheck(RoleId, out managerRoleModel))
            {
                return ReturnHelpMethod.ReturnWarning((int)HttpCodeEnum.Http_400);
            }

            List<string> RoleArray = rolePurviewDAL.GetMenuPurview(managerRoleModel.Id).Select(p => p.ResourceId).ToList();

            List<string> PowerArray = buttonPowerDAL.GetMenuButtonList(managerRoleModel.Id).Select(p => p.RelationButtonId).ToList();

            return ReturnHelpMethod.ReturnSuccess((int)HttpCodeEnum.Http_200, new { Model = managerRoleModel, RoleArray = RoleArray, PowerArray = PowerArray }); ;
        }

        /// <summary>
        /// 修改当前角色信息
        /// </summary>
        /// <param name="UpdateRoleRequestModel"></param>
        /// <param name="UserModel"></param>
        /// <returns></returns>
        public ResultMsg UpdateNowRole(AddRoleRequest UpdateRoleRequestModel, Sys_Manager UserModel)
        {
            Sys_ManagerRole managerRoleModel = new Sys_ManagerRole();

            if (!DataCheck(UpdateRoleRequestModel.Id, out managerRoleModel))
            {
                return ReturnHelpMethod.ReturnWarning((int)HttpCodeEnum.Http_400);
            }

            View_ManagerRoleDetails view_ManagerRoleModel = managerRoleDAL.GetModelById<View_ManagerRoleDetails>(UserModel.Id);

            if (!view_ManagerRoleModel.IsDefault)
            {
                return ReturnHelpMethod.ReturnWarning((int)HttpCodeEnum.Http_1017);
            }

            managerRoleModel.RoleName = UpdateRoleRequestModel.RoleName;
            managerRoleModel.IsDelete = UpdateRoleRequestModel.IsDelete;
            managerRoleModel.Remarks = UpdateRoleRequestModel.Remarks;

            bool bo = managerRoleDAL.UpdateModel<Sys_ManagerRole>(managerRoleModel);

            return bo ? ReturnHelpMethod.ReturnSuccess((int)HttpCodeEnum.Http_Update_602) : ReturnHelpMethod.ReturnError((int)HttpCodeEnum.Http_Update_603); ;
        }

        /// <summary>
        /// 修改当前角色权限信息
        /// </summary>
        /// <param name="UpdateRoleRequestModel"></param>
        /// <param name="UserModel"></param>
        /// <returns></returns>
        public ResultMsg UpdateNowPurview(AddRoleRequest UpdateRoleRequestModel, Sys_Manager UserModel)
        {
            Sys_ManagerRole managerRoleModel = new Sys_ManagerRole();

            UpdateRoleRequestModel.SelectedArray = UpdateRoleRequestModel.SelectedArray.Concat(UpdateRoleRequestModel.FatherArray).ToList();

            if (!DataCheck(UpdateRoleRequestModel.Id, out managerRoleModel))
            {
                return ReturnHelpMethod.ReturnWarning((int)HttpCodeEnum.Http_400);
            }

            View_ManagerRoleDetails view_ManagerRoleModel = managerRoleDAL.GetModelById<View_ManagerRoleDetails>(UserModel.Id);

            if (!view_ManagerRoleModel.IsDefault)
            {
                return ReturnHelpMethod.ReturnWarning((int)HttpCodeEnum.Http_1017);
            }

            //检查权限数据
            //角色权限处理
            List<Sys_RolePurview> ExistRolePurviewList = rolePurviewDAL.GetRolePurviewByRoleId(managerRoleModel.Id);

            List<Sys_RolePurview> RolePurviewList = new List<Sys_RolePurview>();

            if (UpdateRoleRequestModel.SelectedArray.Count > 0)
            {
                foreach (var item in UpdateRoleRequestModel.SelectedArray)
                {
                    RolePurviewList.Add(new Sys_RolePurview()
                    {
                        Id = Guid.NewGuid().ToString(),
                        RoleId = managerRoleModel.Id,
                        ResourceId = item,
                        ResourceType = 0,
                        AddUserId = UserModel.Id,
                        AddTime = DateTime.Now,
                        IsLocking = false,
                        IsDelete = false,
                    });
                }
            }

            //按钮权限处理
            List<Sys_MenuButtonPower> ExistMenuButtonPowersList = buttonPowerDAL.GetMenuButtonList(managerRoleModel.Id);

            List<Sys_MenuButtonPower> MenuButtonPowerList = new List<Sys_MenuButtonPower>();

            if (UpdateRoleRequestModel.MenuPowerArry.Count > 0)
            {
                foreach (var Poweritem in UpdateRoleRequestModel.MenuPowerArry)
                {
                    MenuButtonPowerList.Add(new Sys_MenuButtonPower
                    {
                        Id = Guid.NewGuid().ToString(),
                        RelationButtonId = Poweritem,
                        RelationRoleId = managerRoleModel.Id,
                        IsShow = false,
                        IsDefault = false,
                        AddUserId = UserModel.Id,
                        AddTime = DateTime.Now,
                        UpdateTime = DateTime.Now,
                        IsDelete = false
                    });
                }
            }

            managerRoleDAL.UpdateManagerRole(ExistRolePurviewList, ExistMenuButtonPowersList, RolePurviewList, MenuButtonPowerList);

            return ReturnHelpMethod.ReturnSuccess((int)HttpCodeEnum.Http_Update_602);
        }

        /// <summary>
        /// 停用/启用当前角色
        /// </summary>
        /// <param name="RoleId"></param>
        /// <param name="UserModel"></param>
        /// <returns></returns>
        public ResultMsg EnableOrDisableRole(string RoleId, Sys_Manager UserModel)
        {
            Sys_ManagerRole managerRoleModel = new Sys_ManagerRole();

            if (!DataCheck(RoleId, out managerRoleModel))
            {
                return ReturnHelpMethod.ReturnWarning((int)HttpCodeEnum.Http_400);
            }

            if (managerRoleModel.IsDefault)
            {
                return ReturnHelpMethod.ReturnWarning((int)HttpCodeEnum.Http_1019);
            }

            View_ManagerRoleDetails view_ManagerRoleModel = managerRoleDAL.GetModelById<View_ManagerRoleDetails>(UserModel.Id);

            if (!view_ManagerRoleModel.IsDefault)
            {
                return ReturnHelpMethod.ReturnWarning((int)HttpCodeEnum.Http_1017);
            }

            managerRoleModel.IsLocking = managerRoleModel.IsLocking ? false : true;

            bool bo = managerRoleDAL.UpdateModel<Sys_ManagerRole>(managerRoleModel);

            return bo ? ReturnHelpMethod.ReturnSuccess((int)HttpCodeEnum.Http_Update_602) : ReturnHelpMethod.ReturnSuccess((int)HttpCodeEnum.Http_Update_603);

        }

        /// <summary>
        /// 删除当前角色信息
        /// </summary>
        /// <param name="RoleId"></param>
        /// <param name="UserModel"></param>
        /// <returns></returns>
        public ResultMsg DeleteNowRole(string RoleId, Sys_Manager UserModel)
        {
            Sys_ManagerRole managerRoleModel = new Sys_ManagerRole();

            if (!DataCheck(RoleId, out managerRoleModel))
            {
                return ReturnHelpMethod.ReturnWarning((int)HttpCodeEnum.Http_400);
            }

            if (managerRoleModel.IsDefault)
            {
                return ReturnHelpMethod.ReturnWarning((int)HttpCodeEnum.Http_1019);
            }

            View_ManagerRoleDetails view_ManagerRoleModel = managerRoleDAL.GetModelById<View_ManagerRoleDetails>(UserModel.Id);

            if (!view_ManagerRoleModel.IsDefault)
            {
                return ReturnHelpMethod.ReturnWarning((int)HttpCodeEnum.Http_1017);
            }

            List<Sys_Manager> ManagerList = managerdDAL.GetManagerListByRelationId(managerRoleModel.Id );

            if (ManagerList.Count > 0)
            {
                return ReturnHelpMethod.ReturnWarning((int)HttpCodeEnum.Http_1018);
            }

            List<Sys_RolePurview> ExistRolePurviewList = rolePurviewDAL.GetRolePurviewByRoleId(managerRoleModel.Id);

            managerRoleDAL.DeleteManagerRole(managerRoleModel, ExistRolePurviewList);

            return ReturnHelpMethod.ReturnSuccess((int)HttpCodeEnum.Http_Delete_604);
        }

        #region 提公方法

        /// <summary>
        /// 数据检查(参数是否为空,是否查询到数据)
        /// </summary>
        /// <param name="RoleId"></param>
        /// <param name="managerRoleModel"></param>
        /// <returns></returns>
        public bool DataCheck(string RoleId, out Sys_ManagerRole managerRoleModel)
        {
            managerRoleModel = new Sys_ManagerRole();

            if (String.IsNullOrEmpty(RoleId))
            {
                return false;
            }

            managerRoleModel = managerRoleDAL.GetModelById<Sys_ManagerRole>(RoleId);

            if (managerRoleModel == null)
            {
                return false;
            }
            return true;
        }

        #endregion
    }
}
