using System.Configuration;

namespace DapperCommonMethod.CommonConfig
{
    /// <summary>
    /// 公共静态配置文件
    /// </summary>
    public class CommonConfigs
    {
        #region 获取银行卡归属地配置文件

        /// <summary>
        /// 银行归属地JSON文件
        /// </summary>
        public static string BankJsonName = "BankCode.json";

        /// <summary>
        /// 获取银行归属地接口地址
        /// </summary>
        public static string BankNameApi = "https://ccdcapi.alipay.com/validateAndCacheCardInfo.json?_input_charset=utf-8&amp;cardNo=##&amp;cardBinCheck=true";

        #endregion

        public static string PublicPwd = "a123456";
    }
}
