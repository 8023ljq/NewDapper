using DapperBLL;
using DapperCommonMethod.CommonEnum;
using DapperCommonMethod.CommonMethod;
using DapperHelp.Dapper;
using DapperModel.CommonModel;
using DapperModel.DataModel;
using DapperSql.MySql_SQL;
using System;
using System.Web.Http;

namespace DapperAdminApi.Controllers.Test
{
    /// <summary>
    /// 测试控制器
    /// </summary>
    [RoutePrefix("api/test")]
    public class TextController : ApiController
    {
        /// <summary>
        /// 缓存管理员信息
        /// </summary>
        private LinkMySqlDapperHelps linkMySqlDapper = new LinkMySqlDapperHelps();

        private ManagerdBLL managerdBLL = new ManagerdBLL();

        private TestBLL testBll = new TestBLL();

        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("addmenu")]
        public IHttpActionResult AddMenu()
        {
            Sys_ManagerGroup managerGroup = new Sys_ManagerGroup()
            {
                Id = Guid.NewGuid().ToString(),
                GroupName = "administrator123",
                AddUserId = "524eed52-1a33-40ca-9a70-1c621c8d2640",
                AddTime = DateTime.Now,
                UpdateUserId = "524eed52-1a33-40ca-9a70-1c621c8d2640",
                UpdateTime = DateTime.Now,
                IsLocking = false,
                IsDelete = false,
                Remarks = "备注信息"
            };

            bool bo = linkMySqlDapper.ExecuteInsertGuid<Sys_ManagerGroup>(managerGroup, Sys_ManagerGroupSql.InsertAllSqlStr);

            return Ok(bo ? ReturnHelpMethod.ReturnSuccess((int)HttpCodeEnum.Http_200) : ReturnHelpMethod.ReturnError((int)HttpCodeEnum.Http_400));
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("selectmenu")]
        public IHttpActionResult SelectMenu(string groupname)
        {
            string sql = Sys_ManagerGroupSql.SelectSqlStr;
            if (!String.IsNullOrEmpty(groupname))
            {
                sql += "where groupname=@groupname";
            }

            var Model = linkMySqlDapper.ExecuteReaderReturnT<Sys_ManagerGroup>(sql, new { groupname = groupname });

            return Ok(ReturnHelpMethod.ReturnSuccess((int)HttpCodeEnum.Http_200, new { data = Model }));
        }

        /// <summary>
        /// 测试发送短信
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("sendsms")]
        public IHttpActionResult SendSMS()
        {
            string NumberStr = ((int)NumberPrefixEnum.Apply).GetNumber();
            string Code = "666888";
            bool Sendbo = SMSHelpMethod.Send("15072137573", "SMS_97040028", "{'code':'" + Code + "'}");
            return Ok(Sendbo ? ReturnHelpMethod.ReturnSuccess((int)HttpCodeEnum.Http_200) : ReturnHelpMethod.ReturnSuccess((int)HttpCodeEnum.Http_200));
        }

        //[HttpPost]
        //[AllowAnonymous]
        //[Route("exportdata")]
        //public IHttpActionResult ExportData(SelectModel selectModel)
        //{
        //    byte[] byteData = (managerdBLL.ExportData(selectModel).ResultDataTable).DataTable2Excel("测试导出");

        //    return ResponseMessage(GetHttpResponseMessage(byteData, "测试导出"));
        //}

        /// <summary>
        /// 添加用户(事物处理)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("adduser")]
        public IHttpActionResult AddUser()
        {
            //return Ok(testBll.AddModel());
            return Ok(testBll.GetDataBases());
        }
    }
}
