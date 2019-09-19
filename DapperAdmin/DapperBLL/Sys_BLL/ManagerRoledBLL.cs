using DapperBLL.BaseBLL;
using DapperCommonMethod.CommonEnum;
using DapperCommonMethod.CommonMethod;
using DapperCommonMethod.CommonModel;
using DapperHelp.Dapper;
using DapperModel;
using DapperModel.CommonModel;
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
    /// 管理员角色业务层
    /// </summary>
    public class ManagerRoledBLL : BaseBLLS
    {
        /// <summary>
        /// 获取管理员角色下拉框列表
        /// </summary>
        /// <returns></returns>
        public ResultMsg GetRoleSelectList()
        {
            ResultMsg resultMsg = new ResultMsg();

            List<Sys_ManagerRole> ManagerRoleList = baseDALS.GetListAll<Sys_ManagerRole>("IsDelete=@IsDelete", null, new { IsDelete = 0 });

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
            ResultMsg resultMsg = new ResultMsg();

            List<Sys_ManagerRoleViewModel> ManagerRoleList = baseDALS.GetPageJoinList<Sys_ManagerRoleViewModel>(Sys_ManagerRoleSql.getPageList, selectModel);

            return ReturnHelpMethod.ReturnSuccess((int)HttpCodeEnum.Http_200, new { data = ManagerRoleList, pageModel = selectModel }); ;
        }

        /// <summary>
        /// 添加角色信息
        /// </summary>
        /// <param name="addRoleRequestModel"></param>
        /// <returns></returns>
        public ResultMsg AddNewRole(AddRoleRequest addRoleRequestModel, Sys_Manager UserModel)
        {
            ResultMsg resultMsg = new ResultMsg();

            List<Sys_ManagerRole> ManagerRoleList = baseDALS.GetListAll<Sys_ManagerRole>("RoleName=@RoleName", null, addRoleRequestModel);

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
                Remarks = UserModel.Remarks
            };

            List<Sys_RolePurview> RolePurviewList = new List<Sys_RolePurview>();

            if (addRoleRequestModel.SelectedArray.Count > 0)
            {
                foreach (var item in addRoleRequestModel.SelectedArray)
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

            DapperHelps dapperHelps = new DapperHelps();

            using (var tran = dapperHelps.GetOpenConnection().BeginTransaction())
            {
                dapperHelps.ExecuteInsertGuid(managerRoleModel, tran);

                dapperHelps.ExecuteInsertList(RolePurviewList, tran);

                tran.Commit();
            }

            return ReturnHelpMethod.ReturnSuccess((int)HttpCodeEnum.Http_200);
        }

        /// <summary>
        /// 获取当前角色信息
        /// </summary>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        public ResultMsg SelectRoleModel(string RoleId)
        {
            if (String.IsNullOrEmpty(RoleId))
            {
                return ReturnHelpMethod.ReturnSuccess((int)HttpCodeEnum.Http_400);
            }

            Sys_ManagerRole managerRoleModel = baseDALS.GetModelById<Sys_ManagerRole>(RoleId);

            if (managerRoleModel == null)
            {
                return ReturnHelpMethod.ReturnSuccess((int)HttpCodeEnum.Http_400);
            }

            List<string> RoleArray = baseDALS.GetListAll<Sys_RolePurview>("RoleId=@RoleId", null, new { RoleId = managerRoleModel.Id }).Select(p => p.ResourceId).ToList();

            return ReturnHelpMethod.ReturnSuccess((int)HttpCodeEnum.Http_200, new { Model = managerRoleModel, RoleArray = RoleArray }); ;
        }

        /// <summary>
        /// 修改当前角色信息
        /// </summary>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        public ResultMsg UpdateNowRole(string RoleId)
        {


            return ReturnHelpMethod.ReturnSuccess((int)HttpCodeEnum.Http_200);
        }

        /// <summary>
        /// 停用/启用当前角色
        /// </summary>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        public ResultMsg EnableOrDisableRole(string RoleId)
        {
            return ReturnHelpMethod.ReturnSuccess((int)HttpCodeEnum.Http_200);
        }

        /// <summary>
        /// 删除当前角色信息
        /// </summary>
        /// <param name="RoleId"></param>
        /// <returns></returns>
        public ResultMsg DeleteNowRole(string RoleId)
        {
            return ReturnHelpMethod.ReturnSuccess((int)HttpCodeEnum.Http_200);
        }
    }
}
