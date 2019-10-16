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
        /// Author：Geek Dog  Content：添加菜单信息 AddTime：2019-10-11 16:36:06  
        /// </summary>
        /// <param name="menuModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("addmenuinfo")]
        public IHttpActionResult AddMenuInfo(Sys_Menu menuModel)
        {
            menuModel.GuId = Guid.NewGuid().ToString();
            return Ok(menuBLL.AddMenuInfo(menuModel, GetUserInfo()));
        }

        /// <summary>
        /// Author：Geek Dog  Content：删除菜单 AddTime：2019-10-12 9:17:09  
        /// </summary>
        /// <param name="guId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("deletemenu")]
        public IHttpActionResult DeleteMenu(string guId)
        {
            if (!RegexUtilsMethod.CheckGuID(guId))
            {
                return Ok(ReturnHelpMethod.ReturnWarning((int)HttpCodeEnum.Http_606));
            }

            return Ok(menuBLL.DeleteMenu(guId, GetUserInfo()));
        }

        /// <summary>
        /// Author：Geek Dog  Content：修改菜单信息 AddTime：2019-10-11 16:33:55  
        /// </summary>
        /// <param name="menuModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("updatemenu")]
        public IHttpActionResult UpdateMenu(Sys_Menu menuModel)
        {
            return Ok(menuBLL.UpdateMenu(menuModel, GetUserInfo()));
        }

        /// <summary>
        /// Author：Geek Dog  Content：获取单个菜单信息 AddTime：2019-8-21 9:23:54  
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getmenumodel")]
        public IHttpActionResult GetMenuModel(string guid)
        {
            if (!RegexUtilsMethod.CheckGuID(guid))
            {
                return Ok(ReturnHelpMethod.ReturnWarning((int)HttpCodeEnum.Http_606));
            }

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
            return Ok(menuBLL.GetMenuList(GetUserInfo()));
        }

        /// <summary>
        /// Author：Geek Dog  Content：获取所有菜单集合 AddTime：2019-10-15 15:53:16  
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getallmenulist")]
        public IHttpActionResult GetAllMenuList()
        {
            return Ok(menuBLL.GetAllMenuList());
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
            if (String.IsNullOrEmpty(addMenuPower.ParentId))
            {
                return Ok(ReturnHelpMethod.ReturnWarning((int)HttpCodeEnum.Http_400));
            }

            //数据格式验证
            var IsValidStr = ValidatetionMethod.IsValid(addMenuPower);
            if (!IsValidStr.IsVaild)
            {
                return Ok(ReturnHelpMethod.ReturnWarning(int.Parse(IsValidStr.ErrorMembers)));
            }

            return Ok(menuBLL.AddMenuPower(addMenuPower, GetUserInfo()));
        }

        /// <summary>
        /// Author：Geek Dog  Content：删除按钮权限 AddTime：2019-10-12 16:05:09  
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("deletemenupower")]
        public IHttpActionResult DeleteMenuPower(string guid)
        {
            if (!RegexUtilsMethod.CheckGuID(guid))
            {
                return Ok(ReturnHelpMethod.ReturnWarning((int)HttpCodeEnum.Http_606));
            }
            return Ok(menuBLL.DeleteMenuPower(guid,GetUserInfo()));
        }

        /// <summary>
        /// Author：Geek Dog  Content：修改按钮数据 AddTime：2019-10-16 11:14:55  
        /// </summary>
        /// <param name="addMenuPower"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("updatemenupower")]
        public IHttpActionResult UpdateMenuPower(AddMenuPowerRequest addMenuPower)
        {
            //检查主键
            if (String.IsNullOrEmpty(addMenuPower.GuId))
            {
                return Ok(ReturnHelpMethod.ReturnWarning((int)HttpCodeEnum.Http_400));
            }

            //数据格式验证
            var IsValidStr = ValidatetionMethod.IsValid(addMenuPower);
            if (!IsValidStr.IsVaild)
            {
                return Ok(ReturnHelpMethod.ReturnWarning(int.Parse(IsValidStr.ErrorMembers)));
            }

            return Ok(menuBLL.UpdateMenuPower(addMenuPower, GetUserInfo()));
        }

        /// <summary>
        /// Author：Geek Dog  Content：获取单个菜单按钮数据 AddTime：2019-10-16 10:15:57  
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("getmenupower")]
        public IHttpActionResult GetMenuPower(string guid)
        {
            if (!RegexUtilsMethod.CheckGuID(guid))
            {
                return Ok(ReturnHelpMethod.ReturnWarning((int)HttpCodeEnum.Http_606));
            }
            return Ok(menuBLL.GetMenuPower(guid));
        }
    }
}
