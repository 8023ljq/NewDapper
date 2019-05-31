using System.ComponentModel;

namespace DapperCommonMethod.CommonEnum
{
    /// <summary>
    /// HttpCodeEnum返回状态码
    /// </summary>
    public enum HttpCodeEnum
    {
        /// <summary>
        /// 请求成功
        /// </summary>
        [Description("请求成功")]
        Http_200 = 200,

        /// <summary>
        /// 请求服务失败
        /// </summary>
        [Description("请求服务失败")]
        Http_300 = 300,

        /// <summary>
        /// 请求数据不存在
        /// </summary>
        [Description("请求数据不存在")]
        Http_400 = 400,

        /// <summary>
        /// 服务内部错误
        /// </summary>
        [Description("服务内部错误")]
        Http_500 = 500,

        // ******************************************************************************
        // Author：Geek Dog  
        // Content：1000之后写项目中报错提示  
        // AddTime：2019-1-8 16:32:05  
        //*******************************************************************************

        /// <summary>
        /// 登录成功
        /// </summary>
        [Description("登录成功")]
        Http_1001 = 1001,

        /// <summary>
        /// 用户名或密码错误
        /// </summary>
        [Description("用户名或密码错误")]
        Http_1002 = 1002,

        /// <summary>
        /// 请输入用户名
        /// </summary>
        [Description("请输入用户名")]
        Http_1003 = 1003,

        /// <summary>
        /// 请输入密码
        /// </summary>
        [Description("请输入密码")]
        Http_1004 = 1004,
    }
}
