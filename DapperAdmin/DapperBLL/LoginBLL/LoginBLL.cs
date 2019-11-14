using DapperBLL.BaseBLL;
using DapperCommonMethod.CommonEnum;
using DapperCommonMethod.CommonMethod;
using DapperCommonMethod.CommonModel;
using DapperDAL;
using DapperModel.CommonModel;
using DapperModel.DataModel;
using DapperModel.ViewModel;
using System;

namespace DapperBLL
{
    /// <summary>
    /// 后台登录业务处理层
    /// </summary>
    public class LoginBLL : BaseBLLS
    {
        private LoginDAL loginDAL = new LoginDAL();
        private ManagerRoleDAL managerRoleDAL = new ManagerRoleDAL();
        private ManagerdDAL managerdDAL = new ManagerdDAL();

        /// <summary>
        /// 后台管理员登录
        /// </summary>
        /// <param name="Model"></param>
        /// <param name="IPAddress"></param>
        /// <returns></returns>
        public ResultMsg LoginAct(LoginModelRequest Model, string IPAddress)
        {
            //检查用户是否存在
            Sys_Manager managerModel = loginDAL.GetModelByName(Model.UserName);

            if (managerModel == null)
            {
                return ReturnHelpMethod.ReturnWarning((int)HttpCodeEnum.Http_1002);
            }

            //检查密码
            string PassWord = DESEncryptMethod.Encrypt(Model.PassWord, managerModel.RandomCode);
            if (PassWord != managerModel.Password)
            {
                return ReturnHelpMethod.ReturnWarning((int)HttpCodeEnum.Http_1002);
            }

            //查询用户角色
            Sys_ManagerRole managerroleModel = managerRoleDAL.GetModelById<Sys_ManagerRole>(managerModel.RelationId);

            ManagerReturnModel adminModel = new ManagerReturnModel()
            {
                UserId = managerModel.Id,
                AdminName = String.IsNullOrEmpty(managerModel.Nickname) ? managerModel.Name : managerModel.Nickname,
                Avatar = managerModel.Avatar,
                RoleName = managerroleModel.RoleName,
                RegisteTime = managerroleModel.AddTime.Value,
            };

            //登录成功报存管理员信息
            string Token = DESEncryptMethod.Encrypt(managerModel.Id.ToString(), ExpandMethod.GetTimeStamp());

            //处理单点登录问题
            if (!String.IsNullOrEmpty(managerModel.TokenId))
            {
                redis.KeyDelete(managerModel.TokenId);
            }

            managerModel.TokenId = Token;
            managerModel.LoginTimes = managerModel.LoginTimes + 1;
            managerModel.LastLoginIP = IPAddress;
            managerModel.LastLoginTime = DateTime.Now;

            RedisManagerModel redisManagerModel = new RedisManagerModel()
            {
                Id = managerModel.Id,
                RelationId = managerModel.RelationId,
                Name = managerModel.Name,
                Avatar = managerModel.Avatar,
                Nickname = managerModel.Nickname,
                Phone = managerModel.Phone,
                Email = managerModel.Email,
                LoginTimes = managerModel.LoginTimes,
                LastLoginIP = managerModel.LastLoginIP,
                LastLoginTime = managerModel.LastLoginTime,
                IsDefault = managerModel.IsDefault,
                Remarks = managerModel.Remarks,
            };

            redis.StringSet(Token, redisManagerModel, TimeSpan.FromMinutes(30));

            //修改的成功与否不与登录成功有关系
            managerdDAL.UpdateModel<Sys_Manager>(managerModel);

            return ReturnHelpMethod.ReturnSuccess((int)HttpCodeEnum.Http_1001, new { Data = adminModel, Token = Token });
        }

        /// <summary>
        /// 后台管理员退出
        /// </summary>
        /// <param name="GetToken"></param>
        /// <returns></returns>
        public ResultMsg LogOut(string GetToken)
        {
            if (String.IsNullOrEmpty(GetToken))
            {
                return ReturnHelpMethod.ReturnError((int)HttpCodeEnum.Http_1005);
            }
            if (!redis.KeyExists(GetToken))
            {
                return ReturnHelpMethod.ReturnError((int)HttpCodeEnum.Http_1005);
            }
            if (!redis.KeyDelete(GetToken))
            {
                return ReturnHelpMethod.ReturnError((int)HttpCodeEnum.Http_1007);
            }
            return ReturnHelpMethod.ReturnSuccess((int)HttpCodeEnum.Http_1006);
        }
    }
}
