using System.Configuration;

namespace DapperCommonMethod.CommonConfig
{
    public class AppSettingsConfig
    {
        #region AppSettings

        /// <summary>
        /// 图片上传格式
        /// </summary>
        public static string UploadFormat = ConfigurationManager.AppSettings["UploadFormat"];

        /// <summary>
        /// 图片上传大小
        /// </summary>
        public static string UploadFileSize = ConfigurationManager.AppSettings["UploadFileSize"];

        /// <summary>
        /// 图片上传地址
        /// </summary>
        public static string ServerImgaes = ConfigurationManager.AppSettings["ServerImgaes"];

        /// <summary>
        /// 银行卡归属地查询
        /// </summary>
        public static string bankNameApi = ConfigurationManager.AppSettings["bankNameApi"];

        /// <summary>
        /// Redis默认前缀
        /// </summary>
        public static string redisKey = ConfigurationManager.AppSettings["redisKey"];

        /// <summary>
        /// JSON文件路径
        /// </summary>
        public static string JsonUrl = ConfigurationManager.AppSettings["JsonUrl"];

        /// <summary>
        /// 是否开启短信验证码
        /// </summary>
        public static bool IsOpenSMS = ConfigurationManager.AppSettings["IsOpenSMS"] == "false" ? false : true;

        /// <summary>
        /// 开放验证码
        /// </summary>
        public static string OpenSMS = ConfigurationManager.AppSettings["OpenSMS"];

        /// <summary>
        /// 协议生成PDF路径
        /// </summary>
        public static string PdfAddress = ConfigurationManager.AppSettings["PdfAddress"];

        /// <summary>
        /// 中文josn地址
        /// </summary>
        public static string CNJsonAddress = ConfigurationManager.AppSettings["CNJsonAddress"];

        /// <summary>
        /// 英文文josn地址
        /// </summary>
        public static string ENJsonAddress = ConfigurationManager.AppSettings["ENJsonAddress"];

        #endregion

        #region ConnectionStrings

        /// <summary>
        /// Redis配置文件
        /// </summary>
        public static string RedisExchangeHosts = ConfigurationManager.ConnectionStrings["RedisExchangeHosts"].ConnectionString;

        #endregion
    }
}
