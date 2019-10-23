using DapperBLL.Sys_BLL;
using DapperCacheHelps.RedisHelper;
using DapperCommonMethod.CommonEnum;
using DapperCommonMethod.CommonMethod;
using DapperHelp.Dapper;
using DapperModel;
using DapperSql.MySql_SQL;
using System;
using System.Web.Http;

namespace DapperAdminApi.Controllers.Text
{
    /// <summary>
    /// 测试控制器
    /// </summary>
    [RoutePrefix("v1/api/text")]
    public class TextController : ApiController
    {
        /// <summary>
        /// 缓存管理员信息
        /// </summary>
        private RedisHelper redis = new RedisHelper();
        private LinkMySqlDapperHelps linkMySqlDapper = new LinkMySqlDapperHelps();
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
            string Code = "666888";
            bool Sendbo = SMSHelpMethod.Send("15072137573", "SMS_97040028", "{'code':'" + Code + "'}");
            return Ok(Sendbo ? ReturnHelpMethod.ReturnSuccess((int)HttpCodeEnum.Http_200) : ReturnHelpMethod.ReturnSuccess((int)HttpCodeEnum.Http_200));
        }
    }
}
