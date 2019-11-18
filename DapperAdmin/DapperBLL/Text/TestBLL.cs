using DapperBLL.BaseBLL;
using DapperModel.DataModel;
using System;
using System.Collections.Generic;

namespace DapperBLL
{
    public class TestBLL : BaseBLLS
    {
        public bool UpdateModel()
        {
            L_AdminLoginLog adminLoginLog = baseDALS.GetModelById<L_AdminLoginLog>("6ba760ab-96f3-482c-8ce4-52837309eb5b");

            adminLoginLog.RoleIdName = "测试数据测试数据测试数据";

            adminLoginLog.AdminName = "测试管理员测试管理员测试管理员";

            baseDALS.UpdateModel<L_AdminLoginLog>(adminLoginLog);

            return true;
        }

        public bool AddModel()
        {
            L_AdminLoginLog adminLoginLog = new L_AdminLoginLog()
            {
                Id = Guid.NewGuid().ToString(),
                RoleId = "18EADB40-494E-4AE3-84D4-D38C8867A68F",
                RoleIdName = "测试数据",
                AdminId = "524eed52-1a33-40ca-9a70-1c621c8d2640",
                AdminName = "测试管理员",
                LoginTime = DateTime.Now,
                LoginIp = "127.0.0.1"
            };

            var ads = baseDALS.InsertModelGuid<L_AdminLoginLog>(adminLoginLog);

            return true;
        }


        public bool GetDataBases()
        {
            List<string> StrList = baseDALS.GetList<string>("select * from sysdatabases");

            List<string> StrLists = baseDALS.GetList<string>("  select* from sysobjects where xtype = 'U'");



            return true;
        }
    }
}
