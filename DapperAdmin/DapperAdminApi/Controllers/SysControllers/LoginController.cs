using DapperAdminApi.App_Start;
using DapperAdminApi.Common.Help;
using DapperAdminApi.Models.RequestModel;
using DapperAdminApi.Models.ReturnModel;
using DapperBLL.Sys_BLL;
using DapperCommonMethod.CommonEnum;
using DapperCommonMethod.CommonMethod;
using DapperCommonMethod.CommonModel;
using DapperModel;
using System;
using System.Web.Http;

namespace DapperAdminApi.Controllers.SysControllers
{
    /// <summary>
    /// Author：Geek Dog  Content：登录接口 AddTime：2019-5-22 15:33:13  
    /// </summary>
    [RoutePrefix("api/login")]
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
                Sys_Manager managerModel = managerdBLL.GetModel<Sys_Manager>(whereStr, orderByStr);

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
                Sys_ManagerRole managerroleModel = managerRoledBLL.GetModelById<Sys_ManagerRole>(managerModel.RoleId);

                //返回管理员信息
                AdminModel adminModel = new AdminModel()
                {
                    AdminName = String.IsNullOrEmpty(managerModel.Nickname) ? managerModel.Name : managerModel.Nickname,
                    Avatar = managerModel.Avatar,
                    RoleName = managerroleModel.RoleName,
                    RegisteTime= managerroleModel.AddTime
                };

                //登录成功报存管理员信息
                string Token = DESEncryptMethod.Encrypt(managerModel.Id.ToString(), ExpandMethod.GetTimeStamp());

                //处理单点登录问题
                if (!String.IsNullOrEmpty(managerModel.TokenId))
                {
                    redis.KeyDelete(managerModel.TokenId);
                }

                managerModel.TokenId = Token;
                managerModel.LastLoginTime = DateTime.Now;
                redis.StringSet(Token, managerModel, TimeSpan.FromMinutes(30));
                managerdBLL.UpdateModel<Sys_Manager>(managerModel);

                return Ok(ReturnHelp.ReturnSuccess((int)HttpCodeEnum.Http_1001, new { Data = adminModel, Token = Token }));
            }
            catch (Exception ex)
            {
                WriteLogMethod.WriteLogs(ex);
                return Ok(ReturnHelp.ReturnError((int)HttpCodeEnum.Http_500));
            }

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
            try
            {
                if (String.IsNullOrEmpty(GetToken))
                {
                    return Ok(ReturnHelp.ReturnError((int)HttpCodeEnum.Http_1005));
                }
                if (!redis.KeyExists(GetToken))
                {
                    return Ok(ReturnHelp.ReturnError((int)HttpCodeEnum.Http_1005));
                }
                if (!redis.KeyDelete(GetToken))
                {
                    return Ok(ReturnHelp.ReturnError((int)HttpCodeEnum.Http_1007));
                }
                return Ok(ReturnHelp.ReturnSuccess((int)HttpCodeEnum.Http_1006));
            }
            catch (Exception ex)
            {
                WriteLogMethod.WriteLogs(ex);
                return Ok(ReturnHelp.ReturnError((int)HttpCodeEnum.Http_500));
            }
        }
    }
}
