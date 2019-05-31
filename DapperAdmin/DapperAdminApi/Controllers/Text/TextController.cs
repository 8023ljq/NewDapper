using DapperBLL.Sys_BLL;
using DapperCommonMethod.DBModel.Sys_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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

            menusList.Add(new Sys_Menu()
            {
                Id = Guid.NewGuid().ToString(),
                ParentId = "08ab32f7-c734-4d2f-be2e-ac37db9e2a42",
                FullName = "角色管理",
                Layers = 2,
                IconUrl = String.Empty,
                AddressUrl = "",
                Sort = 1,
                IsShow = true,
                IsDefault = true,
                IsDelete = false,
                AddUserId = Guid.NewGuid().ToString(),
                AddTime = DateTime.Now,
                UpdateTime=DateTime.Now,
                Remarks = ""
            });

            menusList.Add(new Sys_Menu()
            {
                Id = Guid.NewGuid().ToString(),
                ParentId = "08ab32f7-c734-4d2f-be2e-ac37db9e2a42",
                FullName = "用户管理",
                Layers = 2,
                IconUrl = String.Empty,
                AddressUrl = "",
                Sort = 1,
                IsShow = true,
                IsDefault = true,
                IsDelete = false,
                AddUserId = Guid.NewGuid().ToString(),
                AddTime = DateTime.Now,
                UpdateTime = DateTime.Now,
                Remarks = ""
            });

            menusList.Add(new Sys_Menu()
            {
                Id = Guid.NewGuid().ToString(),
                ParentId = "08ab32f7-c734-4d2f-be2e-ac37db9e2a42",
                FullName = "权限管理",
                Layers = 2,
                IconUrl = String.Empty,
                AddressUrl = "",
                Sort = 1,
                IsShow = true,
                IsDefault = true,
                IsDelete = false,
                AddUserId = Guid.NewGuid().ToString(),
                AddTime = DateTime.Now,
                UpdateTime = DateTime.Now,
                Remarks = ""
            });

            menuBLL.InsertList(menusList);

            return Ok();
        }

    }
}
