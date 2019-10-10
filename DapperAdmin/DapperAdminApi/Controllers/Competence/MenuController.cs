using DapperAdminApi.App_Start;
using DapperBLL.Sys_BLL;
using DapperCommonMethod.CommonEnum;
using DapperCommonMethod.CommonMethod;
using DapperModel;
using DapperModel.ViewModel.RequestModel;
using System;
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
        /// Author：Geek Dog  Content：获取单个菜单信息 AddTime：2019-8-21 9:23:54  
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getmenumodel")]
        public IHttpActionResult GetMenuModel(string guid)
        {
            return Ok(menuBLL.GetMenuModel(guid));
        }

        /// <summary>
        /// Author：Geek Dog  Content：获取菜单列表 AddTime：2019-5-29 9:21:15  
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getmenulist")]
        public IHttpActionResult GetMenuList()
        {
            string NowUserRoleId = GetUserInfo().Id;
            return Ok(menuBLL.GetMenuList());
        }

        /// <summary>
        /// Author：Geek Dog  Content：添加菜单里按钮权限数据 AddTime：2019-10-10 11:25:22  
        /// </summary>
        /// <param name="addMenuPower"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("addmenupower")]

        public IHttpActionResult AddMenuPower(AddMenuPowerRequest addMenuPower)
        {
            //检查主键
            if (String.IsNullOrEmpty(addMenuPower.MenuId))
            {
                return Ok(ReturnHelpMethod.ReturnWarning((int)HttpCodeEnum.Http_400));
            }

            //数据格式验证
            var IsValidStr = ValidatetionMethod.IsValid(addMenuPower);
            if (!IsValidStr.IsVaild)
            {
                return Ok(ReturnHelpMethod.ReturnWarning(int.Parse(IsValidStr.ErrorMembers)));
            }

            Sys_Manager userModel = GetUserInfo();

            return Ok(menuBLL.AddMenuPower(addMenuPower, userModel));
        }
    }
}
