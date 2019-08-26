using System;

namespace DapperModel
{
    /// <summary>
    /// 管理员与角色关联表
    /// </summary>
    public class Sys_ManagerRelatedRole
    {
	     
        /// <summary>
        /// 主键Id
        /// </summary>	
        public string Id { get; set; }
 
        /// <summary>
        /// 组ID
        /// </summary>	
        public string ManagerId { get; set; }
 
        /// <summary>
        /// 角色ID
        /// </summary>	
        public string RoleId { get; set; }
 
        /// <summary>
        /// 添加人
        /// </summary>	
        public string AddUserId { get; set; }
 
        /// <summary>
        /// 添加时间
        /// </summary>	
        public DateTime AddTime { get; set; }
 
        /// <summary>
        /// 修改人
        /// </summary>	
        public string UpdateUserId { get; set; }
 
        /// <summary>
        /// 修改时间
        /// </summary>	
        public DateTime UpdateTime { get; set; }

    }
}


