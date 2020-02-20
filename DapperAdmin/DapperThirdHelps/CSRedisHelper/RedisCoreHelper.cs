using CSRedis;
using DapperCommonMethod.CommonLog;
using System;
using System.Configuration;
using System.Threading.Tasks;

namespace DapperCacheHelps.CSRedisHelper
{
    public class RedisCoreHelper : RedisHelper
    {
        //static string redislink = "139.9.167.151:6379,password=admin@2019,defaultDatabase=13,prefix=Text";
        static readonly string redislink = ConfigurationManager.ConnectionStrings["CSRedisExchangeHosts"].ConnectionString;
        static CSRedisClient redisManger = null;

        /// <summary>
        /// 构造函数
        /// </summary>
        static RedisCoreHelper()
        {
            try
            {
                redisManger = new CSRedisClient(redislink);      //Redis的连接字符串
                RedisHelper.Initialization(redisManger);
            }
            catch (Exception ex)
            {
                LogHelper.Log("logsys").WriteError(ex.Message);
            }

        }

        #region T,String类

        #region 同步方法
        /// <summary>
        /// 保存单个key value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">key值</param>
        /// <param name="obj">value</param>
        /// <param name="expiry">过期时间(单位秒-1永不过期)</param>
        public bool StringSet(string key, string obj, int expiry = -1)
        {
            try
            {
                bool bo = Set(key, obj, expiry);
                return bo;
            }
            catch (Exception)
            {

                return false;
            }
        }

        /// <summary>
        /// 获取单个key value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">key值</param>
        /// <returns></returns>
        public string StringGet(string key)
        {
            try
            {
                return Get(key);
            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>
        /// 保存单个实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">key值</param>
        /// <param name="obj">实体类</param>
        /// <param name="expiry">过期时间(单位秒-1永不过期)</param>
        public bool StringSet<T>(string key, T obj, int expiry = -1)
        {
            try
            {
                return Set(key, obj, expiry);
            }
            catch (Exception)
            {

                return false;
            }
        }

        /// <summary>
        /// 获取单个实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">key值</param>
        /// <returns></returns>
        public T StringGet<T>(string key)
        {
            try
            {
                return Get<T>(key);
            }
            catch (Exception)
            {
                return default(T);
            }
        }
        #endregion


        #region 异步方法
        /// <summary>
        /// 保存单个key value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">key值</param>
        /// <param name="obj">value</param>
        /// <param name="expiry">过期时间(单位秒-1永不过期)</param>
        public async Task<bool> StringSetAsync(string key, string obj, int expiry = -1)
        {
            try
            {
                return await SetAsync(key, obj, expiry);
            }
            catch (Exception)
            {

                return false;
            }
        }

        /// <summary>
        /// 获取单个key value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">key值</param>
        /// <returns></returns>
        public async Task<string> StringGetAsync(string key)
        {
            try
            {
                return await GetAsync(key);
            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>
        /// 保存单个实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">key值</param>
        /// <param name="obj">实体类</param>
        /// <param name="expiry">过期时间(单位秒-1永不过期)</param>
        public async Task<bool> StringSetAsync<T>(string key, T obj, int expiry = -1)
        {
            try
            {
                return await SetAsync(key, obj, expiry);
            }
            catch (Exception)
            {

                return false;
            }
        }

        /// <summary>
        /// 获取单个实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">key值</param>
        /// <returns></returns>
        public async Task<T> StringGetAsync<T>(string key)
        {
            try
            {
                return await GetAsync<T>(key);
            }
            catch (Exception)
            {
                return default(T);
            }
        }
        #endregion

        #endregion

        #region list集合
        /// <summary>
        /// 保存单个实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key">key值</param>
        /// <param name="obj">实体类</param>
        /// <param name="expiry">过期时间(单位秒-1永不过期)</param>
        public bool LPushX<T>(string key, T obj, int expiry = -1)
        {
            try
            {
                var log = LPush(key, obj);
                return log > 0L ? true : false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        #region Key操作

        #region 同步方法
        /// <summary>
        /// 检查单个Key值是否存在
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public bool KeyExists(string Key)
        {
            try
            {
                return Exists(Key);
            }
            catch (Exception)
            {

                return false;
            }
        }

        /// <summary>
        /// 删除单个Key值
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public bool KeyDelete(string Key)
        {
            try
            {
                var logbool = Del(Key);
                return logbool > 0L ? true : false;
            }
            catch (Exception)
            {

                return false;
            }
        }

        /// <summary>
        /// 删除单个Key值
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public bool KeyDelete(string[] Key)
        {
            try
            {
                var logbool = Del(Key);
                return logbool > 0L ? true : false;
            }
            catch (Exception)
            {

                return false;
            }
        }

        /// <summary>
        /// 重新命名key
        /// </summary>
        /// <param name="Key">旧key值</param>
        /// <param name="newKey">新的key值</param>
        /// <returns></returns>
        public bool KeyRename(string Key, string newKey)
        {
            try
            {
                return Rename(Key, newKey);
            }
            catch (Exception)
            {

                return false;
            }
        }

        /// <summary>
        /// 设置过期时间
        /// </summary>
        /// <param name="Key">Key值</param>
        /// <param name="Expiry">过期时间(单位秒-1永不过期)</param>
        /// <returns></returns>
        public bool KeyExpire(string Key, int Expiry)
        {
            try
            {
                return Expire(Key, Expiry);
            }
            catch (Exception)
            {

                return false;
            }
        }
        #endregion

        #region 异步方法
        /// <summary>
        /// 检查单个Key值是否存在
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public async Task<bool> KeyExistsAsync(string Key)
        {
            try
            {
                return await ExistsAsync(Key);
            }
            catch (Exception)
            {

                return false;
            }
        }

        /// <summary>
        /// 删除单个Key值
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public async Task<bool> KeyDeleteAsync(string Key)
        {
            try
            {
                var logbool = await DelAsync(Key);
                return logbool > 0L ? true : false;
            }
            catch (Exception)
            {

                return false;
            }
        }

        /// <summary>
        /// 重新命名key
        /// </summary>
        /// <param name="Key">旧key值</param>
        /// <param name="newKey">新的key值</param>
        /// <returns></returns>
        public async Task<bool> KeyRenameAsync(string Key, string newKey)
        {
            try
            {
                return await RenameAsync(Key, newKey);
            }
            catch (Exception)
            {

                return false;
            }
        }
        #endregion

        #endregion
    }
}
