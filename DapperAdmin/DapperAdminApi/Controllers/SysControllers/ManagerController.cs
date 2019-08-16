using DapperAdminApi.App_Start;
using DapperAdminApi.Common.Help;
using DapperBLL.Sys_BLL;
using DapperCommonMethod.CommonConfig;
using DapperCommonMethod.CommonEnum;
using DapperCommonMethod.CommonMethod;
using DapperHelp.Dapper;
using DapperModel;
using DapperModel.CommonModel;
using DapperModel.ViewModel.DBViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DapperAdminApi.Controllers.SysControllers
{
    /// <summary>
    /// Author：Geek Dog  Content：管理员数据接口 AddTime：2019-6-24 17:54:55  
    /// </summary>
    [ApiAuthorize]
    [RoutePrefix("api/manager")]
    public class ManagerController : BaseController
    {
        private ManagerdBLL managerdBLL = new ManagerdBLL();

        /// <summary>
        /// 获取管理员列表信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("getmanagerlist")]
        public IHttpActionResult GetManagerList(PageModel pageModel)
        {
            try
            {
                string selectStr = $@"select B.RoleName,A.* from Sys_Manager A left join Sys_ManagerRole B on A.RoleId=B.Id where A.IsDelete=0";
                List<Sys_ManagerViewModel> managersList = managerdBLL.GetPageJoinList<Sys_ManagerViewModel>(selectStr, pageModel);
                //List<Sys_Manager> managersList = managerdBLL.GetPageList<Sys_Manager>("IsDelete=0", pageModel);
                return Ok(ReturnHelp.ReturnSuccess((int)HttpCodeEnum.Http_200, new { data = managersList, pageModel = pageModel }));
            }
            catch (Exception ex)
            {
                WriteLogMethod.WriteLogs(ex);
                return Ok(ReturnHelp.ReturnError((int)HttpCodeEnum.Http_500));
            }
        }

        /// <summary>
        /// 获取单个管理员信息
        /// </summary>
        /// <param name="MangaerId">管理员主键</param>
        /// <returns></returns>
        [HttpGet]
        [Route("getmanagermodel")]
        public IHttpActionResult GetManagerModel(string mangaerId)
        {
            try
            {
                Sys_Manager managerModel = managerdBLL.GetModelById<Sys_Manager>(mangaerId);
                return Ok(ReturnHelp.ReturnSuccess((int)HttpCodeEnum.Http_200, new { data = managerModel }));
            }
            catch (Exception ex)
            {
                WriteLogMethod.WriteLogs(ex);
                return Ok(ReturnHelp.ReturnError((int)HttpCodeEnum.Http_500));
            }
        }

        /// <summary>
        /// 修改管理员信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("updatemanagerinfo")]
        public IHttpActionResult UpdateManagerInfo(Sys_Manager managerModel)
        {
            try
            {
                //数据格式验证
                var IsValidStr = ValidatetionMethod.IsValid(managerModel);
                if (!IsValidStr.IsVaild)
                {
                    return Ok(ReturnHelp.ReturnError(int.Parse(IsValidStr.ErrorMembers)));
                }
                //检查主键
                //if (String.IsNullOrEmpty(managerModel.Id))
                //{
                //    return Ok(ReturnHelp.ReturnSuccess((int)HttpCodeEnum.Http_400));
                //}

                Sys_Manager manager = managerdBLL.GetModelById<Sys_Manager>(managerModel.Id);
                if (manager == null)
                {
                    return Ok(ReturnHelp.ReturnSuccess((int)HttpCodeEnum.Http_400));
                }
                manager.RoleId = managerModel.RoleId;
                manager.Name = managerModel.Name;
                manager.Avatar = managerModel.Avatar;
                manager.Nickname = managerModel.Nickname;
                manager.Phone = managerModel.Phone;
                manager.Email = managerModel.Email;
                manager.Remarks = managerModel.Remarks;
                manager.UpdateUserId = GetUserId;
                manager.UpdateTime = DateTime.Now;

                bool bo = managerdBLL.UpdateModel<Sys_Manager>(manager);
                if (bo)
                {
                    return Ok(ReturnHelp.ReturnSuccess((int)HttpCodeEnum.Http_200));
                }
                else
                {
                    return Ok(ReturnHelp.ReturnError((int)HttpCodeEnum.Http_300));
                }
            }
            catch (Exception ex)
            {
                WriteLogMethod.WriteLogs(ex);
                return Ok(ReturnHelp.ReturnError((int)HttpCodeEnum.Http_500));
            }
        }

        /// <summary>
        /// 添加管理员
        /// </summary>
        /// <param name="managerModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("addmanagerinfo")]
        public IHttpActionResult AddManagerInfo(Sys_Manager managerModel)
        {
            try
            {
                //数据格式验证
                managerModel.Id = Guid.NewGuid().ToString();
                var IsValidStr = ValidatetionMethod.IsValid(managerModel);
                if (!IsValidStr.IsVaild)
                {
                    return Ok(ReturnHelp.ReturnError(int.Parse(IsValidStr.ErrorMembers)));
                }

                List<Sys_Manager> ManagerList = managerdBLL.GetList<Sys_Manager>("(Name=@Name or Nickname=@Nickname or Phone=@Phone or Email=@Email)", managerModel);

                if (ManagerList.Find(p => p.Name == managerModel.Name) != null)
                {
                    return Ok(ReturnHelp.ReturnError((int)HttpCodeEnum.Http_1009));
                }

                if (ManagerList.Find(p => p.Nickname == managerModel.Nickname) != null)
                {
                    return Ok(ReturnHelp.ReturnError((int)HttpCodeEnum.Http_1010));
                }

                if (ManagerList.Find(p => p.Phone == managerModel.Phone) != null)
                {
                    return Ok(ReturnHelp.ReturnError((int)HttpCodeEnum.Http_1011));
                }

                if (ManagerList.Find(p => p.Email == managerModel.Email) != null)
                {
                    return Ok(ReturnHelp.ReturnError((int)HttpCodeEnum.Http_1012));
                }

                managerModel.RandomCode = ExpandMethod.GetRandNum(6, true, (int)RandNumEnum.NumberAndLetter);
                managerModel.Password = DESEncryptMethod.Encrypt(CommonConfigs.PublicPwd, managerModel.RandomCode);
                managerModel.AddUserId = GetUserId;
                managerModel.AddTime = DateTime.Now;
                managerModel.IsLocking = false;
                managerModel.IsDelete = false;

                DapperHelps dapperHelps = new DapperHelps();

                using (var tran = dapperHelps.GetOpenConnection().BeginTransaction())
                {
                    //后台用户申请(续费)记录
                    dapperHelps.ExecuteInsert(new Sys_Manager()
                    {
                        RandomCode = ExpandMethod.GetRandNum(6, true, (int)RandNumEnum.NumberAndLetter),
                        Password = DESEncryptMethod.Encrypt(CommonConfigs.PublicPwd, managerModel.RandomCode),
                        AddUserId = GetUserId,
                        AddTime = DateTime.Now,
                        IsLocking = false,
                        IsDelete = false
                    }, tran);

                    tran.Commit();
                }

                bool bo = managerdBLL.InsertModelGuid<Sys_Manager>(managerModel);
                if (bo)
                {
                    return Ok(ReturnHelp.ReturnSuccess((int)HttpCodeEnum.Http_200));
                }
                else
                {
                    return Ok(ReturnHelp.ReturnError((int)HttpCodeEnum.Http_300));
                }
            }
            catch (Exception ex)
            {
                WriteLogMethod.WriteLogs(ex);
                return Ok(ReturnHelp.ReturnError((int)HttpCodeEnum.Http_500));
            }
        }

        /// <summary>
        /// 启用或停用管理员
        /// </summary>
        /// <param name="ManagerId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("disorenamanager")]
        public IHttpActionResult DisOrEnaManager(string mangaerId)
        {
            if (!RegexUtilsMethod.CheckGuID(mangaerId))
            {
                return Ok(ReturnHelp.ReturnError((int)HttpCodeEnum.Http_400));
            }

            Sys_Manager manager = managerdBLL.GetModelById<Sys_Manager>(mangaerId);
            if (manager == null)
            {
                return Ok(ReturnHelp.ReturnSuccess((int)HttpCodeEnum.Http_400));
            }
            manager.IsLocking = !manager.IsLocking;

            bool bo = managerdBLL.UpdateModel<Sys_Manager>(manager);
            if (bo)
            {
                return Ok(ReturnHelp.ReturnSuccess((int)HttpCodeEnum.Http_200));
            }
            else
            {
                return Ok(ReturnHelp.ReturnError((int)HttpCodeEnum.Http_300));
            }
        }
    }
}
