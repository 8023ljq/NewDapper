using DapperCacheHelps.CSRedisHelper;
using DapperCommonMethod.CommonEnum;
using DapperCommonMethod.CommonJson;
using DapperCommonMethod.CommonMethod;
using DapperCommonMethod.CommonModel;
using DapperThirdHelps.RedisHelper;
using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace DapperAdminApi.App_Start
{
    /// <summary>
    /// 操作权限过滤器
    /// </summary>
    public class ApiAuthorizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// 缓存管理员信息
        /// </summary>
        //public static RedisHelpers redis = new RedisHelpers();   
         public static RedisCoreHelper CSRedis = new RedisCoreHelper();

        /// <summary>
        /// 指示指定的控件是否已获得授权
        /// </summary>
        /// <param name="actionContext"></param>
        /// <returns></returns>
        protected override bool IsAuthorized(HttpActionContext actionContext)
         {
            try
            {
                // 验证token
                var ts = actionContext.Request.Headers.Where(c => c.Key.ToLower() == "token").FirstOrDefault().Value;
                var token = string.Empty;
                if (ts != null && ts.Count() > 0)
                {
                    token = ts.First<string>();
                    HttpContext.Current.Session["token"] = token;
                    // 验证token
                    if (string.IsNullOrEmpty(token))
                    {
                        return false;
                    }
                    if (!CSRedis.KeyExists((int)CSRedisEnum.Administrator,token))
                    {
                        return false;
                    }
                }

                ////检查用户是否停用
                //var FreighterEntityModel = redis.StringGet<FreighterEntity>(token);
                //freighterEntity = freighterApp.GetFreighterModel(FreighterEntityModel.Id);
                ////freighterEntity = null;
                //if (freighterEntity.IsDisable.Value)
                //{
                //    return false;
                //}
                CSRedis.KeyExpire((int)CSRedisEnum.Administrator,token,1800);
            }
            catch (Exception ex)
            {
                WriteLogMethod.WriteLogs(ex);
                throw;
            }
            return true;
        }

        /// <summary>
        /// 处理授权失败的请求
        /// </summary>
        /// <param name="actionContext"></param>
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            ResultMsg resultMsg = new ResultMsg();
            var response = actionContext.Response = actionContext.Response ?? new HttpResponseMessage();
            string token = HttpContext.Current.Session["token"].ToString();

            if (string.IsNullOrEmpty(token))
            {
                resultMsg.ResultCode = (int)HttpCodeEnum.Http_700;
                resultMsg.ResultMsgs = "您尚未登录,请先登录！";
                response.Content = new StringContent(JosnHelp.ToJson(resultMsg), Encoding.UTF8, "application/json");
            }
            else if (!CSRedis.KeyExists((int)CSRedisEnum.Administrator,token))
            {
                resultMsg.ResultCode = (int)HttpCodeEnum.Http_700;
                resultMsg.ResultMsgs = "当前账号已掉线或在另一端登录！";
                response.Content = new StringContent(JosnHelp.ToJson(resultMsg), Encoding.UTF8, "application/json");
            }
        }
    }
}