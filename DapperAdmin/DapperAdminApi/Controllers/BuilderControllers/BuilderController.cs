using DapperBLL;
using DapperModel.BuilderModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DapperAdminApi.Controllers.BuilderControllers
{
    /// <summary>
    /// Author：Geek Dog  Content：代码生成接口 AddTime：2020-5-8 13:46:05  
    /// </summary>
    [RoutePrefix("api/builder")]
    public class BuilderController : ApiController
    {
        private BuilderBLL builderBL = new BuilderBLL();

        /// <summary>
        /// 连接服务器
        /// </summary>
        /// <param name="connectionModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("connectionact")]
        public IHttpActionResult ConnectionAct(ConnectionModel connectionModel)
        {
            return Ok(builderBL.ConnectionAct(connectionModel));
        }
    }
}
