using DapperAdminApi.App_Start;
using DapperBLL.Sys_BLL;
using DapperCommonMethod.CommonEnum;
using DapperCommonMethod.CommonMethod;
using DapperModel.ViewModel.RequestModel;
using System;
using System.Web.Http;

namespace DapperAdminApi.Controllers.LoginControllers
{
    /// <summary>
    /// Author：Geek Dog  Content：登录接口 AddTime：2019-5-22 15:33:13  
    /// </summary>
    [RoutePrefix("api/login")]
    public class LoginController : BaseController
    {
        private ManagerdBLL managerdBLL = new ManagerdBLL();
        private ManagerRoledBLL managerRoledBLL = new ManagerRoledBLL();

        /// <summary>
        /// Author：Geek Dog  Content：后台管理员登录 AddTime：2019-5-22 15:32:55  
        /// </summary>
        /// <param name="dynamic"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("loginact")]
        public IHttpActionResult LoginAct(LoginModelRequest Model)
        {
            //数据验证
            var IsValidStr = ValidatetionMethod.IsValid(Model);
            if (!IsValidStr.IsVaild)
            {
                return Ok(ReturnHelpMethod.ReturnError(IsValidStr.ErrorMembers));
            }

            ManagerdBLL managerdBLL = new ManagerdBLL();

            return Ok(managerdBLL.ManagerLogin(Model, GetIPAddress));
        }

        /// <summary>
        /// Author：Geek Dog  Content：用户退出 AddTime：2019-6-11 11:44:09  
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ApiAuthorize]
        [Route("logout")]
        public IHttpActionResult LogOut()
        {
            if (String.IsNullOrEmpty(GetToken))
            {
                return Ok(ReturnHelpMethod.ReturnError((int)HttpCodeEnum.Http_1005));
            }
            if (!redis.KeyExists(GetToken))
            {
                return Ok(ReturnHelpMethod.ReturnError((int)HttpCodeEnum.Http_1005));
            }
            if (!redis.KeyDelete(GetToken))
            {
                return Ok(ReturnHelpMethod.ReturnError((int)HttpCodeEnum.Http_1007));
            }
            return Ok(ReturnHelpMethod.ReturnSuccess((int)HttpCodeEnum.Http_1006));
        }
    }
}
