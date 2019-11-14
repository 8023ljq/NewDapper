using DapperCommonMethod.CommonConfig;
using DapperDAL;
using DapperThirdHelps.RedisHelper;

namespace DapperBLL.BaseBLL
{
    /// <summary>
    /// 公共业务处理层
    /// </summary>
    public class BaseBLLS
    {
        public BaseDALS baseDALS = new BaseDALS();

        /// <summary>
        /// 缓存管理员信息
        /// </summary>
        public static RedisHelper redis = new RedisHelper(AppSettingsConfig.RedisUserDB);

        /// <summary>
        /// 缓存管理员信息
        /// </summary>
        public static RedisHelper Commonredis = new RedisHelper(AppSettingsConfig.RedisCommonDB);
    }
}
