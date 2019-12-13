/**
*┌──────────────────────────────────────┐
*│　描    述：{Description}
*│　作    者：{Author}
*│　版    本：1.0    模板代码自动生成     
*│　创建时间：{DateTime}
*└──────────────────────────────────────┘
*┌──────────────────────────────────────┐
*│　命名空间：{Namespaces}
*│　类    名：{ClassName}
*└──────────────────────────────────────┘
*/
using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Dapper.Contrib.Extensions;


namespace 
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

    }
}
