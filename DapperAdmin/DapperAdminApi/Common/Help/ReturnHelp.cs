using DapperCommonMethod.CommonConfig;
using DapperCommonMethod.CommonJson;
using DapperCommonMethod.CommonMethod;
using DapperCommonMethod.CommonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DapperAdminApi.Common.Help
{
    public class ReturnHelp
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
            msg.ResultMsgs = JosnHelp.Readjson(ResultCode.ToString(), LanguageConfig.CN);
            msg.ResultData = Model;
            return msg;
        }

        /// <summary>
        /// Author：Geek Dog  Content：普通请求失败 AddTime：2019-1-8 14:29:48  
        /// </summary>
        /// <param name="enumType"></param>
        /// <param name="Model"></param>
        /// <param name="Errormsg"></param>
        /// <returns></returns>
        public static ResultMsg ReturnError(object ResultCode, dynamic Model = null)
        {
            ResultMsg msg = new ResultMsg();
            msg.ResultCode = ResultCode;
            msg.ResultMsgs = JosnHelp.Readjson(ResultCode.ToString(), LanguageConfig.CN);
            msg.ResultData = Model;
            return msg;
        }

    }
}