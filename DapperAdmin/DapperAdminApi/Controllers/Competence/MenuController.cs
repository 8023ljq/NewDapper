using DapperAdminApi.App_Start;
using DapperAdminApi.Common.Help;
using DapperAdminApi.Common.Method;
using DapperBLL.Sys_BLL;
using DapperCommonMethod.CommonEnum;
using DapperModel;
using System.Collections.Generic;
using System.Web.Http;

namespace DapperAdminApi.Controllers.Competence
{
    /// <summary>
    /// Author：Geek Dog  Content：菜单控制器 AddTime：2019-8-20 16:42:30  
    /// </summary>
    [ApiAuthorize]
    [RoutePrefix("api/menu")]
    public class MenuController : BaseController
    {
        private MenuBLL menuBLL = new MenuBLL();

        /// <summary>
        /// Author：Geek Dog
        /// Content：获取单个菜单信息
        /// AddTime：2019-6-20 13:51:55
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getmenumodel")]
        public IHttpActionResult GetMenuModel(string menuId)
        {
            Sys_Menu menuModel = menuBLL.GetModelById<Sys_Menu>(menuId);

            return Ok(ReturnHelp.ReturnSuccess((int)HttpCodeEnum.Http_200, new { data = menuModel }));
        }

        /// <summary>
        /// Author：Geek Dog  Content：获取菜单列表 AddTime：2019-5-29 9:21:15  
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getmenulist")]
        public IHttpActionResult GetMenuList()
        {
            List<Sys_Menu> menuList = menuBLL.GetListAll<Sys_Menu>("IsDelete=@IsDelete", null, new { IsDelete = 0 });
            List<Sys_Menu> orderlist = new List<Sys_Menu>();
            orderlist = ApiCommonMethod.GetMenuListNew(menuList, orderlist, null);

            return Ok(ReturnHelp.ReturnSuccess((int)HttpCodeEnum.Http_200, new { data = orderlist }));
        }

      

      
    }
}
