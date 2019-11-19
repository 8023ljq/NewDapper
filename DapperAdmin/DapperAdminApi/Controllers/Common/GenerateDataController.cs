using DapperBLL;
using DapperModel.CommonModel;
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

        /// <summary>
        /// 获取当前数据库所有表名
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("nowtablename")]
        public IHttpActionResult GetNowTableName()
        {
            return Ok(generateDataBLL.GetNowTableName());
        }

        /// <summary>
        /// 生成数据文件
        /// </summary>
        /// <param name="TableNameList"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("generatefile")]
        public IHttpActionResult GenerateFile(List<SqlTableModel> tableModels)
        {
            //List<SqlTableModel> TableModelList= new List<SqlTableModel>();
            //TableModelList.Add(TableModel);
            return Ok(generateDataBLL.GenerateFile(tableModels));
        }
    }
}
