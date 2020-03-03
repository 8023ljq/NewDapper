using DapperAdminApi.App_Start;
using DapperBLL;
using System.Web.Http;

namespace DapperAdminApi.Controllers.Common
{
    /// <summary>
    /// Author：Geek Dog  Content：公共控制器 AddTime：2019-5-29 16:17:43  
    /// </summary>
    [ApiAuthorize]
    [RoutePrefix("api/common")]
    public class CommonController : BaseController
    {
        private MenuBLL menuBLL = new MenuBLL();

        [HttpGet]
        [Route("apia")]
        public IHttpActionResult ApiA()
        {
            return Ok(menuBLL.GetMenuList(GetUserInfo()));
        }

        [HttpGet]
        [Route("apib")]
        public IHttpActionResult ApiB()
        {
            return Ok(menuBLL.GetMenuList(GetUserInfo()));
        }

        [HttpGet]
        [Route("apic")]
        public IHttpActionResult ApiC()
        {
            return Ok(menuBLL.GetMenuList(GetUserInfo()));
        }
    }
}
