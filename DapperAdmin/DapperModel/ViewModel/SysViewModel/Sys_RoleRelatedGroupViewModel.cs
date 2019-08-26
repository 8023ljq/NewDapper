using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DapperModel.ViewModel.DBViewModel
{
    /// <summary>
    /// 角色与管理员组关联表
    /// </summary>
    public class Sys_RoleRelatedGroupViewModel
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


