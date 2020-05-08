using DapperCacheHelps.CSRedisHelper;
using DapperCommonMethod.CommonConfig;
using DapperCommonMethod.CommonEnum;
using DapperCommonMethod.CommonMethod;
using DapperCommonMethod.CommonModel;
using DapperDAL;
using DapperModel.BuilderModel;

namespace DapperBLL
{
    /// <summary>
    /// 代码生成业务逻辑层
    /// </summary>
    public class BuilderBLL
    {
        /// <summary>
        /// 缓存管理员信息
        /// </summary>
        private RedisCoreHelper redisCoreHelper = new RedisCoreHelper();
      
        /// <summary>
        /// 连接服务器
        /// </summary>
        /// <param name="connectionModel"></param>
        /// <returns></returns>
        public ResultMsg ConnectionAct(ConnectionModel connectionModel)
        {
            //数据检查
            var IsValidStr = ValidatetionMethod.IsValid(connectionModel);
            if (!IsValidStr.IsVaild)
            {
                return ReturnHelpMethod.ReturnWarning(int.Parse(IsValidStr.ErrorMembers));
            }

            string connstring = $@"Server={ connectionModel.Ip};DataBase=master;Uid={connectionModel.Account};Pwd={connectionModel.Pwd};";
            BuilderDAL builderDAL = new BuilderDAL(connstring);

            bool IsRedis = redisCoreHelper.StringSet((int)CSRedisEnum.Common, CommonConfigs.BuilderRedis, connstring, 86400);

            if (IsRedis)
            {
                bool IsOpen = builderDAL.IsOpen();
                if (IsOpen)
                {
                    return ReturnHelpMethod.ReturnSuccess((int)HttpCodeEnum.Http_Connection_610);
                }
                else
                {
                    return ReturnHelpMethod.ReturnWarning((int)HttpCodeEnum.Http_Connection_609);
                }
            }
            else
            {
                return ReturnHelpMethod.ReturnWarning((int)HttpCodeEnum.Http_Connection_609);
            }
        }

    }
}
