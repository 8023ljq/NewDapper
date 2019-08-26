using DapperBLL.BaseBLL;
using DapperCommonMethod.CommonConfig;
using DapperCommonMethod.CommonEnum;
using DapperCommonMethod.CommonMethod;
using DapperCommonMethod.CommonModel;
using DapperHelp.Dapper;
using DapperModel;
using DapperModel.CommonModel;
using DapperModel.ViewModel.DBViewModel;
using DapperSql.Sys_Sql;
using System;
using System.Collections.Generic;

namespace DapperBLL.Sys_BLL
{
    /// <summary>
    /// 管理员业务处理层
    /// </summary>
    public class ManagerdBLL : BaseBLLS
    {
        private ResultMsg resultMsg = new ResultMsg();

        /// <summary>
        /// 获取管理员列表信息
        /// </summary>
        /// <param name="pageModel"></param>
        /// <returns></returns>
        public ResultMsg GetManagerList(SelectModel selectModel)
        {
            List<Sys_ManagerViewModel> managersList = baseDALS.GetPageJoinList<Sys_ManagerViewModel>(Sys_ManagerSql.getPageList, selectModel);

            return ReturnHelpMethod.ReturnSuccess((int)HttpCodeEnum.Http_200, new { data = managersList, pageModel = selectModel }); ;
        }

        /// <summary>
        /// 获取当前管理员信息
        /// </summary>
        /// <param name="mangaerId"></param>
        /// <returns></returns>
        public ResultMsg GetManagerModel(string mangaerId)
        {
            Sys_Manager managerModel = baseDALS.GetModelById<Sys_Manager>(mangaerId);

            if (managerModel == null)
            {
                return ReturnHelpMethod.ReturnError((int)HttpCodeEnum.Http_400);
            }

            return ReturnHelpMethod.ReturnSuccess((int)HttpCodeEnum.Http_200, new { data = managerModel });
        }

        /// <summary>
        /// 修改管理员信息
        /// </summary>
        /// <param name="managerModel"></param>
        /// <returns></returns>
        public ResultMsg UpdateManagerInfo(Sys_Manager managerModel)
        {
            Sys_Manager manager = baseDALS.GetModelById<Sys_Manager>(managerModel.Id);
            if (manager == null)
            {
                return ReturnHelpMethod.ReturnSuccess((int)HttpCodeEnum.Http_400);
            }
            manager.RoleId = managerModel.RoleId;
            manager.Name = managerModel.Name;
            manager.Avatar = managerModel.Avatar;
            manager.Nickname = managerModel.Nickname;
            manager.Phone = managerModel.Phone;
            manager.Email = managerModel.Email;
            manager.Remarks = managerModel.Remarks;
            manager.UpdateTime = DateTime.Now;

            bool bo = baseDALS.UpdateModel<Sys_Manager>(manager);

            return bo ? ReturnHelpMethod.ReturnSuccess((int)HttpCodeEnum.Http_200) : ReturnHelpMethod.ReturnError((int)HttpCodeEnum.Http_300);
        }

        /// <summary>
        /// 添加管理员
        /// </summary>
        /// <param name="managerModel"></param>
        /// <returns></returns>
        public ResultMsg AddManagerInfo(Sys_Manager managerModel)
        {
            List<Sys_Manager> ManagerList = baseDALS.GetListAll<Sys_Manager>("(Name=@Name or Nickname=@Nickname or Phone=@Phone or Email=@Email)", null, managerModel);

            if (ManagerList.Find(p => p.Name == managerModel.Name) != null)
            {
                return ReturnHelpMethod.ReturnError((int)HttpCodeEnum.Http_1009);
            }

            if (ManagerList.Find(p => p.Nickname == managerModel.Nickname) != null)
            {
                return ReturnHelpMethod.ReturnError((int)HttpCodeEnum.Http_1010);
            }

            if (ManagerList.Find(p => p.Phone == managerModel.Phone) != null)
            {
                return ReturnHelpMethod.ReturnError((int)HttpCodeEnum.Http_1011);
            }

            if (ManagerList.Find(p => p.Email == managerModel.Email) != null)
            {
                return ReturnHelpMethod.ReturnError((int)HttpCodeEnum.Http_1012);
            }

            managerModel.RandomCode = ExpandMethod.GetRandNum(6, true, (int)RandNumEnum.NumberAndLetter);
            managerModel.Password = DESEncryptMethod.Encrypt(CommonConfigs.PublicPwd, managerModel.RandomCode);
            managerModel.AddTime = DateTime.Now;
            managerModel.IsLocking = false;
            managerModel.IsDelete = false;

            DapperHelps dapperHelps = new DapperHelps();

            using (var tran = dapperHelps.GetOpenConnection().BeginTransaction())
            {
                //添加管理员信息
                dapperHelps.ExecuteInsertGuid(managerModel, tran);

                dapperHelps.ExecuteInsertGuid(new Sys_Article()
                {
                    Id = Guid.NewGuid().ToString(),
                    CategoryId = Guid.NewGuid().ToString(),
                    Title = "测试数据",
                    ViewCount = 10,
                    Sort = 1,
                    AddUserId = managerModel.AddUserId,
                    AddTime = DateTime.Now,
                    IsTop = false,
                    IsRed = false,
                    IsPublish = false,
                    IsDeleted = false
                }, tran);

                tran.Commit();
            }

            return ReturnHelpMethod.ReturnSuccess((int)HttpCodeEnum.Http_200);
        }

        /// <summary>
        /// 启用或停用管理员
        /// </summary>
        /// <param name="mangaerId"></param>
        /// <returns></returns>
        public ResultMsg DisOrEnaManager(string mangaerId)
        {
            Sys_Manager manager = baseDALS.GetModelById<Sys_Manager>(mangaerId);
            if (manager == null)
            {
                return ReturnHelpMethod.ReturnSuccess((int)HttpCodeEnum.Http_400);
            }
            manager.IsLocking = !manager.IsLocking;

            bool bo = baseDALS.UpdateModel<Sys_Manager>(manager);

            return bo ? ReturnHelpMethod.ReturnSuccess((int)HttpCodeEnum.Http_200) : ReturnHelpMethod.ReturnError((int)HttpCodeEnum.Http_300);
        }
    }
}
