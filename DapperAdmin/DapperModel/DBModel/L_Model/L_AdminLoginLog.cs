using System;

namespace DapperModel
{
    /// <summary>
    /// 管理员登录日志
    /// </summary>
    public class L_AdminLoginLog
    {
        /// <summary>
        /// 主键Id
        /// </summary>	
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

    }
}


