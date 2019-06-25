using DapperAdminApi.App_Start;
using DapperAdminApi.Common.Help;
using DapperAdminApi.Common.Method;
using DapperBLL.Sys_BLL;
using DapperCommonMethod.CommonEnum;
using DapperCommonMethod.CommonMethod;
using DapperCommonMethod.CommonModel;
using DapperModel;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace DapperAdminApi.Controllers.Common
{
    /// <summary>
    /// Author：Geek Dog  Content：公共控制器 AddTime：2019-5-29 16:17:43  
    /// </summary>
    [RoutePrefix("v1/api/common")]
    public class CommonController : BaseController
    {
        private MenuBLL menuBLL = new MenuBLL();
        
        /// <summary>
        /// Author：Geek Dog  Content：获取菜单列表 AddTime：2019-5-29 9:21:15  
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ApiAuthorize]
        [Route("getmenulist")]
        public IHttpActionResult GetMenuList()
        {
            try
            {
                whereStr.Clear();
                orderByStr.Clear();
                List<Sys_Menu> menuList = menuBLL.GetList<Sys_Menu>(whereStr, orderByStr);
                List<Sys_Menu> orderlist = new List<Sys_Menu>();
                orderlist = ApiCommonMethod.GetMenuListNew(menuList, orderlist,null);

                return Ok(ReturnHelp.ReturnSuccess((int)HttpCodeEnum.Http_200, new { data = orderlist }));
            }
            catch (Exception ex)
            {
                WriteLogMethod.WriteLogs(ex);
                return Ok(ReturnHelp.ReturnError((int)HttpCodeEnum.Http_500));
            }
        }

        /// <summary>
        /// Author：Geek Dog
        /// Content：获取单个菜单信息
        /// AddTime：2019-6-20 13:51:55
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ApiAuthorize]
        [Route("getmenumodel")]
        public IHttpActionResult GetMenuModel(string Id)
        {
            try
            {
                //whereStr.Clear();
                //orderByStr.Clear();
                //whereStr.Add("Id", new WhereModel { InquireManner = (int)SqlTypeEnum.Equal, Content = Id });
                //Sys_Menu menuModel = menuBLL.GetModel<Sys_Menu>(whereStr, orderByStr);
                Sys_Menu menuModel = menuBLL.GetModelById<Sys_Menu>(Id);

                return Ok(ReturnHelp.ReturnSuccess((int)HttpCodeEnum.Http_200, new { data = menuModel }));
            }
            catch (Exception ex)
            {
                WriteLogMethod.WriteLogs(ex);
                return Ok(ReturnHelp.ReturnError((int)HttpCodeEnum.Http_500));
            }
          
        }
    }
}
