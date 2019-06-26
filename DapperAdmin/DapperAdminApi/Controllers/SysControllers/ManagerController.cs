using DapperAdminApi.App_Start;
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
    [RoutePrefix("api/login")]
    public class ManagerController : BaseController
    {
        /// <summary>
        /// 获取管理员列表信息
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("getmanagerlist")]
        public IHttpActionResult GetManagerList()
        {
            return Ok();
        }
    }
}
