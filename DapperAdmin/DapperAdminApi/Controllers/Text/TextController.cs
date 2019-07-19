using DapperBLL.Sys_BLL;
using DapperModel;
using System;
using System.Collections.Generic;
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
        /// 添加菜单
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("addmenu")]
        public IHttpActionResult AddMenu()
        {
            MenuBLL menuBLL = new MenuBLL();
            List<Sys_Menu> menusList = new List<Sys_Menu>();

            string Id = "08ab32f7-c734-4d2f-be2e-ac37db9e2a42";
            Sys_Menu menuModel = menuBLL.GetModelById<Sys_Menu>(Id);
            menuModel.Remarks = "测试修改";


           bool bo= menuBLL.UpdateModel(menuModel);

            return Ok();
        }

    }
}
