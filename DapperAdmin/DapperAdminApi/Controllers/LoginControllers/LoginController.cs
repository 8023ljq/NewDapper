using DapperAdminApi.App_Start;
using DapperBLL;
using DapperCommonMethod.CommonMethod;
using DapperModel.ViewModel;
using System.Web.Http;
using System.Web.Http.Cors;

namespace DapperAdminApi.Controllers.LoginControllers
{
    /// <summary>
    /// Author：Geek Dog  Content：登录接口 AddTime：2019-5-22 15:33:13  
    /// </summary>
    [RoutePrefix("api/login")]
    public class LoginController : BaseController
    {
        private LoginBLL loginBLL = new LoginBLL();

        /// <summary>
        /// 后台管理员登录 
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

            return Ok(loginBLL.LoginAct(Model, GetIPAddress));
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
            return Ok(loginBLL.LogOut(GetToken));
        }
    }
}
