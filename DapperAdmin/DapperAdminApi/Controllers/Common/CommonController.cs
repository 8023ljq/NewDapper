using DapperAdminApi.Common.Help;
using DapperAdminApi.Common.Method;
using DapperBLL.Sys_BLL;
using DapperCommonMethod.CommonEnum;
using DapperCommonMethod.CommonMethod;
using DapperCommonMethod.CommonModel;
using DapperCommonMethod.DBModel.Sys_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DapperAdminApi.Controllers.Common
{
    /// <summary>
    /// Author：Geek Dog  Content：公共控制器 AddTime：2019-5-29 16:17:43  
    /// </summary>
    [RoutePrefix("v1/api/common")]
    public class CommonController : BaseController
    {
        /// <summary>
        /// Author：Geek Dog  Content：获取菜单列表 AddTime：2019-5-29 9:21:15  
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("getmenulist")]
        public IHttpActionResult GetMenuList()
        {
            try
            {
                MenuBLL menuBLL = new MenuBLL();

                whereStr.Clear();
                orderByStr.Clear();
                List<Sys_Menu> menuList = menuBLL.GetList<Sys_Menu>("Sys_Menu", whereStr, orderByStr);
                menuList = ApiCommonMethod.GetMenuList(menuList);

                return Ok(ReturnHelp.ReturnSuccess((int)HttpCodeEnum.Http_200, new { data = menuList }));
            }
            catch (Exception ex)
            {
                WriteLogMethod.WriteLogs(ex);
                return Ok(ReturnHelp.ReturnError((int)HttpCodeEnum.Http_500));
            }

        }
    }
}
