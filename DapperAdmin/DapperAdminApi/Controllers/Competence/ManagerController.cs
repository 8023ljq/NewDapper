﻿using DapperAdminApi.App_Start;
using DapperAdminApi.Common.Help;
using DapperBLL.Sys_BLL;
using DapperCommonMethod.CommonConfig;
using DapperCommonMethod.CommonEnum;
using DapperCommonMethod.CommonMethod;
using DapperHelp.Dapper;
using DapperModel;
using DapperModel.CommonModel;
using DapperModel.ViewModel.DBViewModel;
using DapperSql.Sys_Sql;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace DapperAdminApi.Controllers.Competence
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
            return Ok(managerdBLL.GetManagerList(pageModel));
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
            return Ok(managerdBLL.GetManagerModel(mangaerId));
        }

        /// <summary>
        /// 修改管理员信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("updatemanagerinfo")]
        public IHttpActionResult UpdateManagerInfo(Sys_Manager managerModel)
        {
            //检查主键
            if (String.IsNullOrEmpty(managerModel.Id))
            {
                return Ok(ReturnHelpMethod.ReturnError((int)HttpCodeEnum.Http_400));
            }

            //数据格式验证
            var IsValidStr = ValidatetionMethod.IsValid(managerModel);
            if (!IsValidStr.IsVaild)
            {
                return Ok(ReturnHelpMethod.ReturnError(int.Parse(IsValidStr.ErrorMembers)));
            }

            managerModel.UpdateUserId = GetUserId;

            return Ok(managerdBLL.UpdateManagerInfo(managerModel));
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
            //数据格式验证
            managerModel.Id = Guid.NewGuid().ToString();
            var IsValidStr = ValidatetionMethod.IsValid(managerModel);
            if (!IsValidStr.IsVaild)
            {
                return Ok(ReturnHelpMethod.ReturnError(int.Parse(IsValidStr.ErrorMembers)));
            }

            //补充参数
            managerModel.AddUserId = GetUserId;

            return Ok(managerdBLL.AddManagerInfo(managerModel));
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
                return Ok(ReturnHelpMethod.ReturnError((int)HttpCodeEnum.Http_400));
            }

            return Ok(managerdBLL.DisOrEnaManager(mangaerId));
        }
    }
}