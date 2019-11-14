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
        /// 是否开启短信验证码
        /// </summary>
        public static bool IsOpenSMS = ConfigurationManager.AppSettings["IsOpenSMS"] == "false" ? false : true;

        /// <summary>
        /// 开放验证码
        /// </summary>
        public static string OpenSMS = ConfigurationManager.AppSettings["OpenSMS"];

        /// <summary>
        /// 系统默认密码
        /// </summary>
        public static string PublicPwd = ConfigurationManager.AppSettings["PublicPwd"];

        /// <summary>
        /// 中文josn地址
        /// </summary>
        public static string CNJsonAddress = ConfigurationManager.AppSettings["CNJsonAddress"];

        /// <summary>
        /// 英文文josn地址
        /// </summary>
        public static string ENJsonAddress = ConfigurationManager.AppSettings["ENJsonAddress"];

        /// <summary>
        /// Redis默认前缀
        /// </summary>
        public static string redisKey = ConfigurationManager.AppSettings["redisKey"];

        /// <summary>
        /// Redis保存用户信息
        /// </summary>
        public static int RedisUserDB = int.Parse(ConfigurationManager.AppSettings["RedisUserDB"]);

        /// <summary>
        /// Redis保存公共信息
        /// </summary>
        public static int RedisCommonDB = int.Parse(ConfigurationManager.AppSettings["RedisCommonDB"]);

        /// <summary>
        /// 数据对比过滤字段
        /// </summary>
        public static string Filtration = ConfigurationManager.AppSettings["Filtration"];

        /// <summary>
        /// 银行归属地JSON文件
        /// </summary>
        public static string BankJsonName = ConfigurationManager.AppSettings["BankJsonName"];

        /// <summary>
        /// 获取银行归属地接口地址
        /// </summary>
        public static string BankNameApi = ConfigurationManager.AppSettings["BankNameApi"];


        #endregion

        #region ConnectionStrings

        /// <summary>
        /// Redis配置文件
        /// </summary>
        public static string RedisExchangeHosts = ConfigurationManager.ConnectionStrings["RedisExchangeHosts"].ConnectionString;

        #endregion
    }
}
