using Dapper;
using DapperAdminApi.Common.Help;
using DapperAdminApi.Controllers.Base;
using DapperAdminApi.Models.RequestModel;
using DapperBLL.C_BLL;
using DapperCommonMethod.CommonEnum;
using DapperCommonMethod.CommonMethod;
using DapperCommonMethod.CommonModel;
using DapperCommonMethod.DBModel.Sys_Model;
using DapperHelp.Dapper;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace DapperAdminApi.Controllers.SysControllers
{
    /// <summary>
    /// Author：Geek Dog  Content：登录接口 AddTime：2019-5-22 15:33:13  
    /// </summary>
    [RoutePrefix("v1/api/login")]
    public class LoginController : BaseController
    {
        private Dictionary<string, WhereModel> whereStr = new Dictionary<string, WhereModel>();
        private Dictionary<string, OrderByModel> orderByStr = new Dictionary<string, OrderByModel>();

        /// <summary>
        /// Author：Geek Dog  Content：后台登录 AddTime：2019-5-22 15:32:55  
        /// </summary>
        /// <param name="dynamic"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("loginact")]
        public IHttpActionResult LoginAct(LoginModel Model)
        {
            var IsValidStr = ValidatetionMethod.IsValid(Model);
            if (!IsValidStr.IsVaild)
            {
                return Ok(ReturnHelp.ReturnError(IsValidStr.ErrorMembers));
            }

            Sys_Manager ManagerModel = new Sys_Manager()
            {
                Id = Guid.NewGuid().ToString(),
                RoleId = "18EADB40-494E-4AE3-84D4-D38C8867A68F",
                Name = "admin",
                //RandomCode = ExpandMethod.GetRandNum(4, true, (int)RandNumEnum.NumberAndLetter),
                RandomCode = "4D5cs3",
                Password = DESEncryptMethod.Encrypt("a123456", "4D5cs3"),
                AddUserId = Guid.NewGuid().ToString(),
                AddTime = DateTime.Now,
                IsLocking = false,
                IsDelete = false,
                LastLoginTime= DateTime.Now,
                UpdateTime=DateTime.Now,
            };
            string ID = String.Empty;
            ManagerdBLL managerdBLL = new ManagerdBLL();
            bool bo = managerdBLL.AddModelGuid(ManagerModel, out ID);

            //DynamicParameters parameters = new DynamicParameters();
            //string sql = "select * from Sys_Manager";
            //whereStr.Add("name", new WhereModel { InquireManner = (int)SqlTypeEnum.Equal, Content = "ljq8023" });
            //sql = DapperSpliceCondition.GetWhereStr(sql, whereStr, orderByStr, out parameters);
            //List<Sys_Manager> userList = DapperHelps.ExecuteReaderReturnList<Sys_Manager>(sql, parameters);

            return Ok(ReturnHelp.ReturnSuccess("1001"));
        }
    }
}
