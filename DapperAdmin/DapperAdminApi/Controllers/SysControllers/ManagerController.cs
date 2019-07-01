using DapperAdminApi.App_Start;
using DapperAdminApi.Common.Help;
using DapperBLL.Sys_BLL;
using DapperCommonMethod.CommonEnum;
using DapperCommonMethod.CommonMethod;
using DapperModel;
using DapperModel.CommonModel;
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
        [HttpGet]
        [AllowAnonymous]
        [Route("getmanagerlist")]
        public IHttpActionResult GetManagerList()
        {
            try
            {
                PageModel pageModel = new PageModel
                {
                    pageSize = 5,
                    curPage = 1,
                };
                List<Sys_Manager> managersList = managerdBLL.GetPageList<Sys_Manager>("IsDelete=0", pageModel);
                return Ok(ReturnHelp.ReturnSuccess((int)HttpCodeEnum.Http_200, new { data = managersList , pageModel = pageModel }));
            }
            catch (Exception ex)
            {
                WriteLogMethod.WriteLogs(ex);
                return Ok(ReturnHelp.ReturnError((int)HttpCodeEnum.Http_500));
            }
        }
    }
}
