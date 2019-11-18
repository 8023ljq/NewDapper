using Dapper.Contrib.Extensions;
using System;

namespace DapperModel.DataModel
{
    /// <summary>
    /// 管理员登录日志
    /// </summary>
    [Table("L_AdminLoginLog")]
    public class L_AdminLoginLog
    {
        /// <summary>
        /// 主键Id
        /// </summary>	
        [ExplicitKey]
        public string Id { get; set; }
 
        /// <summary>
        /// 角色ID
        /// </summary>	
        public string RoleId { get; set; }
 
        /// <summary>
        /// 角色名称
        /// </summary>	
        public string RoleIdName { get; set; }
 
        /// <summary>
        /// 管理员ID
        /// </summary>	
        public string AdminId { get; set; }
 
        /// <summary>
        /// 管理员名称
        /// </summary>	
        public string AdminName { get; set; }
 
        /// <summary>
        /// 登录时间
        /// </summary>	
        public DateTime LoginTime { get; set; }
 
        /// <summary>
        /// 登录IP
        /// </summary>	
        public string LoginIp { get; set; }

        /// <summary>
        /// 测试字段
        /// </summary>
        [Computed]
        public string LoginIpStr { get; set; }
    }
}


