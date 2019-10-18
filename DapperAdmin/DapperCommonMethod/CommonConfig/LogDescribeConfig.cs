using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperCommonMethod.CommonConfig
{
    /// <summary>
    /// 管理员操作日志模板
    /// </summary>
    public class LogDescribeConfig
    {
        /// <summary>
        /// 添加日志模板
        /// </summary>
        public static string AddDescribe = @"管理员:[{0}]在[{1}]添加[{2}]";

        /// <summary>
        /// 删除日志模板
        /// </summary>
        public static string DeleteDescribe = @"管理员:[{0}]在[{1}]删除[{2}]";

        /// <summary>
        /// 修改日志模板
        /// </summary>
        public static string UpdateDescribe = @"管理员:[{0}]在[{1}]删除[{2}]";
    }
}
