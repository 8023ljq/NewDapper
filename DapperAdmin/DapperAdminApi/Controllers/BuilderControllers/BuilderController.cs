using DapperBLL;
using DapperModel.BuilderModel;
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

        /// <summary>
        /// 获取数据库数据树结构
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getdatelist")]
        public IHttpActionResult GetDateList()
        {
            return Ok(builderBL.GetDateList());
        }


        [HttpPost]
        [Route("text")]
        public IHttpActionResult Text(ConnectionModel connectionModel)
        {
            return Ok(builderBL.Text(connectionModel));
        }
    }
}
