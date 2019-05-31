using DapperCommonMethod.CommonEnum;
using DapperCommonMethod.CommonJson;
using DapperCommonMethod.CommonModel;
using System.Web.Mvc;

namespace DapperAdmin.Controllers
{
    public class BaseController : Controller
    {
        
        /// <summary>
        /// 普通返回成功
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        protected virtual ActionResult Success(string message)
        {
            return Content(new AjaxResult { ResultCode = (int)ResultTypeEnum.success, ResultMsg = message }.ToJson());
        }

        /// <summary>
        /// 带参数的返回成功
        /// </summary>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        protected virtual ActionResult Success(string message, object data)
        {
            return Content(new AjaxResult { ResultCode = (int)ResultTypeEnum.success, ResultMsg = message, ResultData = data }.ToJson());
        }
        
        /// <summary>
        /// 返回失败
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        protected virtual ActionResult Error(string message)
        {
            return Content(new AjaxResult { ResultCode = (int)ResultTypeEnum.error, ResultMsg = message }.ToJson());
        }

        /// <summary>
        /// 返回警告
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        protected virtual ActionResult Warning(string message)
        {
            return Content(new AjaxResult { ResultCode = (int)ResultTypeEnum.warning, ResultMsg = message }.ToJson());
        }
    }
}