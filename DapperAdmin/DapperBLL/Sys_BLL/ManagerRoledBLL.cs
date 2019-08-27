using DapperBLL.BaseBLL;
using DapperCommonMethod.CommonEnum;
using DapperCommonMethod.CommonMethod;
using DapperCommonMethod.CommonModel;
using DapperModel;
using DapperModel.CommonModel;
using DapperModel.ViewModel;
using DapperModel.ViewModel.DBViewModel;
using DapperSql.Sys_Sql;
using System.Collections.Generic;

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
    }
}
