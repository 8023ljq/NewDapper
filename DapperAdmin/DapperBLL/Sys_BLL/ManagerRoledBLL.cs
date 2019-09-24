using DapperBLL.BaseBLL;
using DapperCommonMethod.CommonEnum;
using DapperCommonMethod.CommonMethod;
using DapperCommonMethod.CommonModel;
using DapperCommonMethod.CommonSqlMethod;
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

            string sql = $@"select (select Name from Sys_Manager B where A.AddUserId=b.Id) as AddUserName,
                                              (select Name from Sys_Manager B where A.UpdateUserId = b.Id) as UpdateUserName,
                                              A.* {SqlMethod.GetRowNum("A.AddTime", "desc")} from Sys_ManagerRole A where A.IsDelete=0";

            if (!String.IsNullOrEmpty(selectModel.Keyword))
            {
                sql += $@" and A.RoleName=@Keyword";
            }

            List<Sys_ManagerRoleViewModel> ManagerRoleList = baseDALS.GetPageJoinList<Sys_ManagerRoleViewModel>(sql, selectModel);

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
                Remarks = addRoleRequestModel.Remarks
            };


            string Id = baseDALS.InsertModelGuid<Sys_ManagerRole>(managerRoleModel);

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

            if (!DataCheck(RoleId,out managerRoleModel))
            {
                return ReturnHelpMethod.ReturnWarning((int)HttpCodeEnum.Http_400);
            }

            List<string> RoleArray = baseDALS.GetListAll<Sys_RolePurview>("RoleId=@RoleId", null, new { RoleId = managerRoleModel.Id }).Select(p => p.ResourceId).ToList();

            return ReturnHelpMethod.ReturnSuccess((int)HttpCodeEnum.Http_200, new { Model = managerRoleModel, RoleArray = RoleArray }); ;
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

            View_ManagerRoleDetails view_ManagerRoleModel = baseDALS.GetModelById<View_ManagerRoleDetails>(UserModel.Id);

            if (!view_ManagerRoleModel.IsDefault)
            {
                return ReturnHelpMethod.ReturnWarning((int)HttpCodeEnum.Http_1017);
            }

            managerRoleModel.RoleName = UpdateRoleRequestModel.RoleName;
            managerRoleModel.IsDelete = UpdateRoleRequestModel.IsDelete;
            managerRoleModel.Remarks = UpdateRoleRequestModel.Remarks;

            return baseDALS.UpdateModel<Sys_ManagerRole>(managerRoleModel) ? ReturnHelpMethod.ReturnSuccess((int)HttpCodeEnum.Http_Update_602) : ReturnHelpMethod.ReturnError((int)HttpCodeEnum.Http_Update_603); ;
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

            if (!DataCheck(UpdateRoleRequestModel.Id, out managerRoleModel))
            {
                return ReturnHelpMethod.ReturnWarning((int)HttpCodeEnum.Http_400);
            }

            View_ManagerRoleDetails view_ManagerRoleModel = baseDALS.GetModelById<View_ManagerRoleDetails>(UserModel.Id);

            if (!view_ManagerRoleModel.IsDefault)
            {
                return ReturnHelpMethod.ReturnWarning((int)HttpCodeEnum.Http_1017);
            }

            //检查权限数据

            List<Sys_RolePurview> ExistRolePurviewList = baseDALS.GetListAll<Sys_RolePurview>("RoleId=@RoleId and IsLocking=0 and IsDelete=0", null, new { RoleId = managerRoleModel.Id });

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

            DapperHelps dapperHelps = new DapperHelps();

            using (var tran = dapperHelps.GetOpenConnection().BeginTransaction())
            {
                if (ExistRolePurviewList.Count > 0)
                {
                    dapperHelps.DeleteList(ExistRolePurviewList, tran);
                }

                dapperHelps.ExecuteInsertList(RolePurviewList, tran);

                tran.Commit();
            }

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

            View_ManagerRoleDetails view_ManagerRoleModel = baseDALS.GetModelById<View_ManagerRoleDetails>(UserModel.Id);

            if (!view_ManagerRoleModel.IsDefault)
            {
                return ReturnHelpMethod.ReturnWarning((int)HttpCodeEnum.Http_1017);
            }

            managerRoleModel.IsLocking = managerRoleModel.IsLocking ? false : true;

            return baseDALS.UpdateModel<Sys_ManagerRole>(managerRoleModel) ? ReturnHelpMethod.ReturnSuccess((int)HttpCodeEnum.Http_Update_602) : ReturnHelpMethod.ReturnSuccess((int)HttpCodeEnum.Http_Update_603);

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

            View_ManagerRoleDetails view_ManagerRoleModel = baseDALS.GetModelById<View_ManagerRoleDetails>(UserModel.Id);

            if (!view_ManagerRoleModel.IsDefault)
            {
                return ReturnHelpMethod.ReturnWarning((int)HttpCodeEnum.Http_1017);
            }

            List<Sys_Manager> ManagerList = baseDALS.GetListAll<Sys_Manager>("RelationId=@RelationId", null, new { RelationId = managerRoleModel.Id });

            if (ManagerList.Count > 0)
            {
                return ReturnHelpMethod.ReturnWarning((int)HttpCodeEnum.Http_1018);
            }

            List<Sys_RolePurview> ExistRolePurviewList = baseDALS.GetListAll<Sys_RolePurview>("RoleId=@RoleId and IsLocking=0 and IsDelete=0", null, new { RoleId = managerRoleModel.Id });

            DapperHelps dapperHelps = new DapperHelps();

            using (var tran = dapperHelps.GetOpenConnection().BeginTransaction())
            {
                dapperHelps.DeleteModel(managerRoleModel, tran);

                dapperHelps.DeleteList(ExistRolePurviewList, tran);
                tran.Commit();
            }

            return ReturnHelpMethod.ReturnSuccess((int)HttpCodeEnum.Http_Delete_604);
        }

        #region 提公方法

        /// <summary>
        /// 数据检查(参数是否为空,是否查询到数据)
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public bool DataCheck(string RoleId, out Sys_ManagerRole managerRoleModel)
        {
            managerRoleModel = new Sys_ManagerRole();
            if (String.IsNullOrEmpty(RoleId))
            {
                return false;
            }

            managerRoleModel = baseDALS.GetModelById<Sys_ManagerRole>(RoleId);

            if (managerRoleModel == null)
            {
                return false;
            }
            return true;
        }

        #endregion
    }
}
