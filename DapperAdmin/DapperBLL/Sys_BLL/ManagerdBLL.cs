using DapperCommonMethod.CommonConfig;
using DapperCommonMethod.CommonEnum;
using DapperCommonMethod.CommonMethod;
using DapperCommonMethod.CommonModel;
using DapperModel.CommonModel;
using DapperModel.ViewModel.DBViewModel;
using System;
using System.Collections.Generic;
using DapperModel.DataModel;
using DapperDAL;
using DapperModel.ViewModel;

namespace DapperBLL
{
    /// <summary>
    /// 管理员业务处理层
    /// </summary>
    public class ManagerdBLL 
    {
        private ManagerdDAL managerdDAL = new ManagerdDAL();

        /// <summary>
        /// 获取管理员列表信息
        /// </summary>
        /// <param name="pageModel"></param>
        /// <returns></returns>
        public ResultMsg GetManagerList(SelectModel selectModel)
        {
            List<Sys_Manager> managersList = managerdDAL.GetManagerList(selectModel);

            return ReturnHelpMethod.ReturnSuccess((int)HttpCodeEnum.Http_200, new { data = managersList, pageModel = selectModel }); ;
        }

        /// <summary>
        /// 获取导出数据
        /// </summary>
        /// <param name="selectModel"></param>
        /// <returns></returns>
        public ResultMsg ExportData(SelectModel selectModel)
        {
            var CustomerViewList = managerdDAL.GetManagerList(selectModel);

            return ReturnHelpMethod.ReturnDataTable((int)HttpCodeEnum.Http_200, CustomerViewList.ToDataTable());
        }

        /// <summary>
        /// 获取当前管理员信息
        /// </summary>
        /// <param name="mangaerId"></param>
        /// <returns></returns>
        public ResultMsg GetManagerModel(string mangaerId)
        {
            ManagerRoleReturnModel roleReturnModel = new ManagerRoleReturnModel();

            Sys_Manager managerModel = new Sys_Manager();

            if (!DataCheck(mangaerId, out managerModel))
            {
                return ReturnHelpMethod.ReturnError((int)HttpCodeEnum.Http_400);
            }

            ManagerRoleDAL managerRoledDAL = new ManagerRoleDAL();

            roleReturnModel.ManagerModel = managerModel;
            roleReturnModel.RoleSelectViewList = managerRoledDAL.GetSelectRoleList(managerModel);

            return ReturnHelpMethod.ReturnSuccess((int)HttpCodeEnum.Http_200, new { data = roleReturnModel });
        }

        /// <summary>
        /// 修改管理员信息
        /// </summary>
        /// <param name="managerModel"></param>
        /// <returns></returns>
        public ResultMsg UpdateManagerInfo(Sys_Manager managerModel, Sys_Manager LoginUser)
        {
            Sys_Manager AddModel = new Sys_Manager();

            if (!DataCheck(managerModel.Id, out AddModel))
            {
                return ReturnHelpMethod.ReturnWarning((int)HttpCodeEnum.Http_400);
            }

            if (AddModel.IsDefault && AddModel.RelationId != LoginUser.RelationId)
            {
                return ReturnHelpMethod.ReturnWarning((int)HttpCodeEnum.Http_1021);
            }

            AddModel.RelationId = managerModel.RelationId;
            AddModel.Name = managerModel.Name;
            AddModel.Avatar = managerModel.Avatar;
            AddModel.Nickname = managerModel.Nickname;
            AddModel.Phone = managerModel.Phone;
            AddModel.Email = managerModel.Email;
            AddModel.Remarks = managerModel.Remarks;
            AddModel.UpdateTime = DateTime.Now;

            bool bo = managerdDAL.UpdateModel<Sys_Manager>(AddModel);

            return bo ? ReturnHelpMethod.ReturnSuccess((int)HttpCodeEnum.Http_Update_602) : ReturnHelpMethod.ReturnError((int)HttpCodeEnum.Http_Update_603);
        }

        /// <summary>
        /// 添加管理员
        /// </summary>
        /// <param name="managerModel"></param>
        /// <returns></returns>
        public ResultMsg AddManagerInfo(Sys_Manager managerModel)
        {
            List<Sys_Manager> ManagerList = managerdDAL.GetManagerList(managerModel);

            if (ManagerList.Find(p => p.Name == managerModel.Name) != null)
            {
                return ReturnHelpMethod.ReturnWarning((int)HttpCodeEnum.Http_1009);
            }

            if (ManagerList.Find(p => p.Nickname == managerModel.Nickname) != null)
            {
                return ReturnHelpMethod.ReturnWarning((int)HttpCodeEnum.Http_1010);
            }

            if (ManagerList.Find(p => p.Phone == managerModel.Phone) != null)
            {
                return ReturnHelpMethod.ReturnWarning((int)HttpCodeEnum.Http_1011);
            }

            if (ManagerList.Find(p => p.Email == managerModel.Email) != null)
            {
                return ReturnHelpMethod.ReturnWarning((int)HttpCodeEnum.Http_1012);
            }

            managerModel.RandomCode = ((int)RandNumEnum.NumberAndLetter).GetRandNum(6, true);
            managerModel.Password = DESEncryptMethod.Encrypt(AppSettingsConfig.PublicPwd, managerModel.RandomCode);
            managerModel.AddTime = DateTime.Now;
            managerModel.IsLocking = false;
            managerModel.IsDelete = false;

            if (!String.IsNullOrEmpty(managerdDAL.InsertModelGuid<Sys_Manager>(managerModel)))
            {
                return ReturnHelpMethod.ReturnSuccess((int)HttpCodeEnum.Http_Add_600);
            }
            else
            {
                return ReturnHelpMethod.ReturnError((int)HttpCodeEnum.Http_Add_601);
            }
        }

        /// <summary>
        /// 启用或停用管理员
        /// </summary>
        /// <param name="mangaerId"></param>
        /// <returns></returns>
        public ResultMsg DisOrEnaManager(string mangaerId)
        {
            Sys_Manager managerModel = new Sys_Manager();

            if (!DataCheck(mangaerId, out managerModel))
            {
                return ReturnHelpMethod.ReturnWarning((int)HttpCodeEnum.Http_400);
            }

            managerModel.IsLocking = !managerModel.IsLocking;

            bool bo = managerdDAL.UpdateModel<Sys_Manager>(managerModel);

            return bo ? ReturnHelpMethod.ReturnSuccess((int)HttpCodeEnum.Http_Update_602) : ReturnHelpMethod.ReturnError((int)HttpCodeEnum.Http_Update_603);
        }

        /// <summary>
        /// 删除管理员
        /// </summary>
        /// <param name="mangaerId"></param>
        /// <returns></returns>
        public ResultMsg DeleteManager(string mangaerId)
        {
            Sys_Manager AddModel = new Sys_Manager();

            if (!DataCheck(mangaerId, out AddModel))
            {
                return ReturnHelpMethod.ReturnWarning((int)HttpCodeEnum.Http_400);
            }

            if (AddModel.IsDefault)
            {
                return ReturnHelpMethod.ReturnWarning((int)HttpCodeEnum.Http_1020);
            }

            View_ManagerRoleDetails view_ManagerRoleModel = managerdDAL.GetModelById<View_ManagerRoleDetails>(mangaerId);

            if (!view_ManagerRoleModel.IsDefault)
            {
                return ReturnHelpMethod.ReturnWarning((int)HttpCodeEnum.Http_1017);
            }

            bool bo = managerdDAL.DeleteStringId<Sys_Manager>(mangaerId);

            return bo ? ReturnHelpMethod.ReturnSuccess((int)HttpCodeEnum.Http_Delete_604) : ReturnHelpMethod.ReturnError((int)HttpCodeEnum.Http_Delete_605);
        }

        #region 提公方法

        /// <summary>
        /// 数据检查(参数是否为空,是否查询到数据)
        /// </summary>
        /// <param name="mangaerId"></param>
        /// <param name="managerModel"></param>
        /// <returns></returns>
        public bool DataCheck(string mangaerId, out Sys_Manager managerModel)
        {
            managerModel = managerdDAL.GetModelById<Sys_Manager>(mangaerId);

            if (managerModel == null)
            {
                return false;
            }
            return true;
        }

        #endregion
    }
}
