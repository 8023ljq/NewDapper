using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DapperAdminApi.Controllers.SysControllers
{
    /// <summary>
    /// Author：Geek Dog  Content：登录接口 AddTime：2019-5-22 15:33:13  
    /// </summary>
    [RoutePrefix("v1/api/login")]
    public class LoginController : ApiController
    {
        /// <summary>
        /// Author：Geek Dog  Content：后台登录 AddTime：2019-5-22 15:32:55  
        /// </summary>
        /// <param name="dynamic"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("loginact")]
        public IHttpActionResult LoginAct()
        {
            return Ok();
        }
    }
}
