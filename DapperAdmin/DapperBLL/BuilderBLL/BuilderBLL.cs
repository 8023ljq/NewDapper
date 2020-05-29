using DapperCacheHelps.CSRedisHelper;
using DapperCommonMethod.CommonConfig;
using DapperCommonMethod.CommonEnum;
using DapperCommonMethod.CommonMethod;
using DapperCommonMethod.CommonModel;
using DapperDAL;
using DapperModel.BuilderModel;
using System;
using System.Collections.Generic;

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

        /// <summary>
        /// 获取数据库数据树结构
        /// </summary>
        /// <returns></returns>
        public ResultMsg GetDateList()
        {
            string connstring = redisCoreHelper.StringGet((int)CSRedisEnum.Common, CommonConfigs.BuilderRedis);

            if (String.IsNullOrEmpty(connstring))
            {
                return ReturnHelpMethod.ReturnWarning((int)HttpCodeEnum.Http_Connection_611);
            }

            BuilderDAL builderDAL = new BuilderDAL(connstring);

            bool IsOpen = builderDAL.IsOpen();

            if (IsOpen)
            {
                string sql = $@"SELECT dbid as Id,NAME as label,'0' as ParentId FROM SYSDATABASES WHERE dbid>6 ORDER BY dbid";

                List<DataTreeModel> dataTreeList = builderDAL.GetList<DataTreeModel>(sql);

                foreach (var item in dataTreeList)
                {
                    item.children = new List<DataTreeModel>();

                    //查表
                    string sqltable = $@"Select Id,Name as label,'{item.id}' as ParentId  FROM [{item.label}]..sysobjects  Where xtype='U' ORDER BY name";
                    List<DataTreeModel> tableTreeList = builderDAL.GetList<DataTreeModel>(sqltable);
                    if (tableTreeList.Count > 0)
                    {
                        item.children.Add(new DataTreeModel
                        {
                            id = 119,
                            label = "数据表",
                            ParentId = item.id.ToString(),
                            children = tableTreeList,
                        });
                    }

                    //查视图
                    string sqlview = $@"Select Id,Name as label,'{item.id}' as ParentId  FROM [{item.label}]..sysobjects  Where xtype='v'  ORDER BY name";
                    List<DataTreeModel> viewTreeList = builderDAL.GetList<DataTreeModel>(sqlview);
                    if (viewTreeList.Count > 0)
                    {
                        item.children.Add(new DataTreeModel
                        {
                            id = 120,
                            label = "视图",
                            ParentId = item.id.ToString(),
                            children = viewTreeList,
                        });
                    }
                }

                return ReturnHelpMethod.ReturnSuccess((int)HttpCodeEnum.Http_200, new { data = dataTreeList });
            }
            else
            {
                return ReturnHelpMethod.ReturnWarning((int)HttpCodeEnum.Http_Connection_611);
            }
        }

        /// <summary>
        /// 测试接口
        /// </summary>
        /// <param name="connectionModel"></param>
        /// <returns></returns>
        public ResultMsg Text(ConnectionModel connectionModel)
        {
            var asd = AESMethod.DecryptByAES(connectionModel.Pwd);

            return ReturnHelpMethod.ReturnSuccess((int)HttpCodeEnum.Http_Connection_610,asd);
        }
    }
}
