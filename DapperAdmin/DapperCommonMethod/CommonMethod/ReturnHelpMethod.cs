using DapperCommonMethod.CommonConfig;
using DapperCommonMethod.CommonEnum;
using DapperCommonMethod.CommonJson;
using DapperCommonMethod.CommonModel;
using System.Data;

namespace DapperCommonMethod.CommonMethod
{
    /// <summary>
    /// 处理返回参数
    /// </summary>
    public class ReturnHelpMethod
    {
        /// <summary>
        /// Author：Geek Dog  Content：普通请求成功 AddTime：2019-1-8 14:25:30  
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <param name="Model">返回参数</param>
        /// <returns></returns>
        public static ResultMsg ReturnSuccess(object ResultCode, object Model = null)
        {
            ResultMsg msg = new ResultMsg();
            msg.ResultCode = 200;
            msg.ResultType = "success";
            msg.ResultMsgs = JosnHelp.Readjson(ResultCode.ToString(), LanguageConfig.CN);
            msg.ResultData = Model;
            return msg;
        }

        /// <summary>
        /// Author：Geek Dog  Content：普通请求失败(错误提示) AddTime：2019-1-8 14:29:48  
        /// </summary>
        /// <param name="enumType"></param>
        /// <param name="Model"></param>
        /// <param name="Errormsg"></param>
        /// <returns></returns>
        public static ResultMsg ReturnError(object ResultCode, dynamic Model = null)
        {
            ResultMsg msg = new ResultMsg();
            msg.ResultCode = ResultCode;
            msg.ResultType = "error";
            msg.ResultMsgs = JosnHelp.Readjson(ResultCode.ToString(), LanguageConfig.CN);
            msg.ResultData = Model;
            return msg;
        }

        /// <summary>
        /// Author：Geek Dog  Content：普通请求失败(警告提示) AddTime：2019-9-19 16:52:39  
        /// </summary>
        /// <param name="ResultCode"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static ResultMsg ReturnWarning(object ResultCode, dynamic Model = null)
        {
            ResultMsg msg = new ResultMsg();
            msg.ResultCode = ResultCode;
            msg.ResultType = "warning";
            msg.ResultMsgs = JosnHelp.Readjson(ResultCode.ToString(), LanguageConfig.CN);
            msg.ResultData = Model;
            return msg;
        }

        /// <summary>
        /// Author：Geek Dog  Content：普通请求失败返回DataTable(警告提示) AddTime：2019-9-19 16:52:39  
        /// </summary>
        /// <param name="ResultCode"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        public static ResultMsg ReturnDataTable(int ResultCode, DataTable Model = null)
        {
            ResultMsg msg = new ResultMsg();
            msg.ResultCode = ResultCode;
            msg.ResultType = "warning";
            msg.ResultMsgs = EnumMethod.GetDesString<HttpCodeEnum>(ResultCode);
            msg.ResultDataTable = Model;
            return msg;
        }
    }
}

