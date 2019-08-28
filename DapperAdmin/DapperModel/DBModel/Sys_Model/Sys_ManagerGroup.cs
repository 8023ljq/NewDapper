using System;
using System.Collections.Generic;

namespace DapperModel
{
    /// <summary>
    /// 管理员组表
    /// </summary>
    public class Sys_ManagerGroup
    {
        /// <summary>
        /// 主键Id
        /// </summary>	
        public string Id { get; set; }

        /// <summary>
        /// 父级ID
        /// </summary>
        public string ParentId { get; set; }

        /// <summary>
        /// 组名
        /// </summary>	
        public string GroupName { get; set; }

        /// <summary>
        /// 添加人
        /// </summary>	
        public string AddUserId { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>	
        public DateTime? AddTime { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>	
        public string UpdateUserId { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>	
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 是否锁定(0:否1:是)
        /// </summary>	
        public bool IsLocking { get; set; }

        /// <summary>
        /// 是否删除(0:否1:是)
        /// </summary>	
        public bool IsDelete { get; set; }

        /// <summary>
        /// 备注
        /// </summary>	
        public string Remarks { get; set; }
    }
}


