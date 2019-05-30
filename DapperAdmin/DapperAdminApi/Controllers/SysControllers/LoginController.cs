using DapperAdminApi.Common.Help;
using DapperAdminApi.Models.RequestModel;
using DapperAdminApi.Models.ReturnModel;
using DapperBLL.Sys_BLL;
using DapperCommonMethod.CommonEnum;
using DapperCommonMethod.CommonMethod;
using DapperCommonMethod.CommonModel;
using DapperCommonMethod.DBModel.Sys_Model;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace DapperAdminApi.Controllers.SysControllers
{
    /// <summary>
    /// Author：Geek Dog  Content：登录接口 AddTime：2019-5-22 15:33:13  
    /// </summary>
    [RoutePrefix("v1/api/login")]
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
        public IHttpActionResult LoginAct(LoginModel Model)
        {
            try
            {
                //数据验证
                var IsValidStr = ValidatetionMethod.IsValid(Model);
                if (!IsValidStr.IsVaild)
                {
                    return Ok(ReturnHelp.ReturnError(IsValidStr.ErrorMembers));
                }

                whereStr.Clear();
                orderByStr.Clear();
                whereStr.Add("Name", new WhereModel { InquireManner = (int)SqlTypeEnum.Equal, Content = Model.UserName });
                Sys_Manager managerModel = managerdBLL.GetModel<Sys_Manager>("Sys_Manager", whereStr, orderByStr);

                //检查用户是否存在
                if (managerModel == null)
                {
                    return Ok(ReturnHelp.ReturnError((int)HttpCodeEnum.Http_1002));
                }

                //检查密码
                string PassWord = DESEncryptMethod.Encrypt(Model.PassWord, managerModel.RandomCode);
                if (PassWord != managerModel.Password)
                {
                    return Ok(ReturnHelp.ReturnError((int)HttpCodeEnum.Http_1002));
                }

                //查询用户角色
                whereStr.Clear();
                orderByStr.Clear();
                whereStr.Add("id", new WhereModel { InquireManner = (int)SqlTypeEnum.Equal, Content = managerModel.RoleId });
                Sys_ManagerRole managerroleModel = managerRoledBLL.GetModel<Sys_ManagerRole>("Sys_ManagerRole", whereStr, orderByStr);

                //返回管理员信息
                AdminModel adminModel = new AdminModel()
                {
                    AdminName = String.IsNullOrEmpty(managerModel.Nickname) ? managerModel.Name : managerModel.Nickname,
                    Avatar = managerModel.Avatar,
                    RoleName = managerroleModel.RoleName
                };

                //登录成功报存管理员信息
                string Token = DESEncryptMethod.Encrypt(managerModel.Id.ToString(), ExpandMethod.GetTimeStamp());
                redis.StringSet(Token, managerModel, TimeSpan.FromMinutes(30));

                return Ok(ReturnHelp.ReturnSuccess((int)HttpCodeEnum.Http_1001, new { Data = adminModel, Token = Token }));
            }
            catch (Exception ex)
            {
                WriteLogMethod.WriteLogs(ex);
                return Ok(ReturnHelp.ReturnError((int)HttpCodeEnum.Http_500));
            }

        }
    }
}
