using DapperBLL.BaseBLL;
using DapperCommonMethod.CommonEnum;
using DapperCommonMethod.CommonMethod;
using DapperCommonMethod.CommonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperBLL.Sys_BLL
{
    /// <summary>
    /// 管理员组业务层
    /// </summary>
    public class ManagerGroupBLL: BaseBLLS
    {
        public ResultMsg GetManagerGroupList()
        {



            return ReturnHelpMethod.ReturnSuccess((int)HttpCodeEnum.Http_200);
        }
    }
}
