using System;

namespace DapperModel
{
    /// <summary>
    /// 管理员与管理员组关联表
    /// </summary>
    public class Sys_ManagerRelatedGroup
    {
	     
        /// <summary>
        /// 主键Id
        /// </summary>	
        public string Id { get; set; }
 
        /// <summary>
        /// 组ID
        /// </summary>	
        public string GroupId { get; set; }
 
        /// <summary>
        /// 管理员ID
        /// </summary>	
        public string ManagerId { get; set; }
 
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


