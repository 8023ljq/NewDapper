using DapperCommonMethod.CommonLog;
using DapperCommonMethod.CommonModel;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Filters;

namespace DapperAdminApi.App_Start
{
    /// <summary>
    /// 捕获异常过滤器
    /// </summary>
    public class WebApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {

            string ControllerName = actionExecutedContext.ActionContext.ControllerContext.ControllerDescriptor.ControllerName;
            string ActionName = actionExecutedContext.ActionContext.ActionDescriptor.ActionName;

            string message = string.Format("控制器：{0} 下执行：{1} 引发异常,异常信息：{2} 异常的对象：{3}"
                , ControllerName
                , ActionName
                , actionExecutedContext.Exception.Message
                , actionExecutedContext.Exception.Source);

            LogHelper.Log("logsys").WriteError(message);

            ResultMsg resultMsg = new ResultMsg();
            resultMsg.ResultCode= HttpStatusCode.InternalServerError;
            resultMsg.ResultMsgs = "服务器被外星人绑架了,开发人员正在解救中！";

            var resp = new HttpResponseMessage(HttpStatusCode.InternalServerError)
            {
                //Content = new StringContent(JosnHelp.ToJson(resultMsg), Encoding.UTF8, "application/json"),
                Content = new StringContent("服务器被外星人绑架了,开发人员正在解救中！"),
            };
            throw new HttpResponseException(resp);
        }
    }
}