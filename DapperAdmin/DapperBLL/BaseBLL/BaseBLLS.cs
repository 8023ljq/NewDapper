using DapperCacheHelps.CSRedisHelper;
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
        public static RedisHelpers redis = new RedisHelpers(AppSettingsConfig.RedisUserDB);

        /// <summary>
        /// 缓存管理员信息
        /// </summary>
        public static RedisHelpers Commonredis = new RedisHelpers(AppSettingsConfig.RedisCommonDB);

        /// <summary>
        /// CSRedis缓存
        /// </summary>
        public static RedisCoreHelper CSRedis = new RedisCoreHelper();
    }
}
