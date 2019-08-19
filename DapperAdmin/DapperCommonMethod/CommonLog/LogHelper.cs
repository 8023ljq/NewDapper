using log4net;
using log4net.Config;
using System;
using System.IO;
using System.Web;

namespace DapperCommonMethod.CommonLog
{
    /// <summary>
    /// 日志帮助类
    /// </summary>
    public class LogHelper
    {
        static LogHelper()
        {
            XmlConfigurator.Configure(new FileInfo(HttpContext.Current.Server.MapPath("/Configs/Log4net.config")));
        }

        private static LogHelper _instance = null;
        private static ILog ILog;

        public static LogHelper Log(Type t)
        {
            if (_instance == null)
                _instance = new LogHelper();

            ILog = LogManager.GetLogger(t);

            return _instance;
        }

        public static LogHelper Log(string sType)
        {
            if (_instance == null)
                _instance = new LogHelper();

            ILog = LogManager.GetLogger(sType);

            return _instance;
        }

        /// <summary>
        /// 写调试信息
        /// </summary>
        /// <param name="msg">消息</param>
        public void WriteDebug(string msg)
        {
            ILog.Debug(msg);
        }

        /// <summary>
        /// 写普通信息
        /// </summary>
        /// <param name="msg">消息</param>
        public void WriteInfo(string msg)
        {
            ILog.Info(msg);
        }

        /// <summary>
        /// 写警告信息
        /// </summary>
        /// <param name="msg">消息</param>
        public void WriteWarn(string msg)
        {
            ILog.Warn(msg);
        }

        /// <summary>
        /// 写错误信息
        /// </summary>
        /// <param name="msg">消息</param>
        public void WriteError(string msg)
        {
            ILog.Error(msg);
        }

        /// <summary>
        /// 写错误信息
        /// </summary>
        /// <param name="msg">消息</param>
        /// <param name="ex">错误信息</param>
        public void WriteError(string msg, Exception ex)
        {
            ILog.Error(msg, ex);
        }

        /// <summary>
        /// 写重大错误信息
        /// </summary>
        /// <param name="msg">消息</param>
        public void WriteFatal(string msg)
        {
            ILog.Fatal(msg);
        }
    }
}
