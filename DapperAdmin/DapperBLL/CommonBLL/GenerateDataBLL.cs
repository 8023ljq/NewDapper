using DapperCommonMethod.CommonEnum;
using DapperCommonMethod.CommonMethod;
using DapperCommonMethod.CommonModel;
using DapperDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperBLL
{
    /// <summary>
    /// 生成数据业务逻辑层
    /// </summary>
    public class GenerateDataBLL
    {
        GenerateDataDAL dataDAL = new GenerateDataDAL();

        /// <summary>
        /// 获取当前数据库所有表名
        /// </summary>
        /// <returns></returns>
        public ResultMsg GetNowTableName()
        {
            List<string> TableNameList = dataDAL.GetNowTableName();
            return ReturnHelpMethod.ReturnSuccess((int)HttpCodeEnum.Http_200, new { data = TableNameList });
        }

        /// <summary>
        /// 生成数据文件
        /// </summary>
        /// <param name="TableNameList">所选表名</param>
        /// <returns></returns>
        public ResultMsg GenerateFile(string[] TableNameList)
        {
            foreach (var item in TableNameList)
            {

            }


            return ReturnHelpMethod.ReturnSuccess((int)HttpCodeEnum.Http_200);
        }
    }
}
