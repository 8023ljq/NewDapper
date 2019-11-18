using DapperBLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DapperAdminApi.Controllers.Common
{
    /// <summary>
    /// 生成数据接口
    /// </summary>
    [AllowAnonymous]
    [RoutePrefix("api/generatedata")]
    public class GenerateDataController : ApiController
    {
        GenerateDataBLL generateDataBLL = new GenerateDataBLL();

        [HttpGet]
        [Route("nowtablename")]
        public IHttpActionResult GetNowTableName()
        {
            return Ok(generateDataBLL.GetNowTableName());
        }
    }
}
