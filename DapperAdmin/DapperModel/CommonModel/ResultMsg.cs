using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperCommonMethod.CommonModel
{
    /// <summary>
    /// 普通请求返回参数
    /// </summary>
    public class ResultMsg
    {
        public ResultMsg()
        {
            ResultData = new { };
            ResultMsgs = string.Empty;
        }

        /// <summary>
        /// 状态码
        /// </summary>
        public object ResultCode { get; set; }
        /// <summary>
        /// 获取 消息内容
        /// </summary>
        public string ResultMsgs { get; set; }
        /// <summary>
        /// 获取 返回数据
        /// </summary>
        public object ResultData { get; set; }
    }

    /// <summary>
    /// 列表请求返回参数
    /// </summary>
    public class ResultListMsg
    {
        public ResultListMsg()
        {
            data = new { };
            msg = string.Empty;
        }

        /// <summary>
        /// 状态码
        /// </summary>
        public int code { get; set; }

        /// <summary>
        /// 返回总行数
        /// </summary>
        public int count { get; set; }

        /// <summary>
        /// 操作信息
        /// </summary>
        public string msg { get; set; }

        /// <summary>
        /// 返回数据
        /// </summary>
        public object data { get; set; }
    }

    /// <summary>
    /// 第三方获取银行卡类别返回参数
    /// </summary>
    public class ResultBankApi
    {
        /// <summary>
        /// 银行卡种类
        /// </summary>
        public string cardType { get; set; }

        /// <summary>
        /// 银行卡类别
        /// </summary>
        public string bank { get; set; }

        /// <summary>
        /// 银行卡号
        /// </summary>
        public string key { get; set; }

        /// <summary>
        /// 返回信息
        /// </summary>
        //public Array[] messages { get; set; }

        /// <summary>
        /// 是否生效
        /// </summary>
        public bool Vaildated { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public string stat { get; set; }

    }

        /// <summary>
    /// 发送短信请求返回参数
    /// </summary>
    public class ResultSMSMsg
    {
        public ResultSMSMsg()
        {
            data = new { };
            msg = string.Empty;
        }

        /// <summary>
        /// 状态码
        /// </summary>
        public String code { get; set; }

        /// <summary>
        /// 操作信息
        /// </summary>
        public string msg { get; set; }

        /// <summary>
        /// 返回数据
        /// </summary>
        public object data { get; set; }
    }

    /// <summary>
    /// 异步请求返回参数
    /// </summary>
    public class AjaxResult {
        /// <summary>
        /// 操作结果类型
        /// </summary>
        public object ResultCode { get; set; }
        /// <summary>
        /// 获取 消息内容
        /// </summary>
        public string ResultMsg { get; set; }
        /// <summary>
        /// 获取 返回数据
        /// </summary>
        public object ResultData { get; set; }
    }
}
