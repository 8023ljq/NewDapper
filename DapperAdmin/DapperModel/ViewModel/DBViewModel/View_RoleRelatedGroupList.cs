using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DapperModel.ViewModel.DBViewModel
{
    /// <summary>
    /// 管理员组与角色联合数据
    /// </summary>
    public class View_RoleRelatedGroupList
    {
        /// <summary>
        /// Id
        /// </summary>	
        public string Id { get; set; }

        /// <summary>
        /// GroupName
        /// </summary>	
        public string GroupName { get; set; }

        /// <summary>
        /// AddTime
        /// </summary>	
        public DateTime AddTime { get; set; }

        /// <summary>
        /// AddUserId
        /// </summary>	
        public string AddUserId { get; set; }

        /// <summary>
        /// GroupIsLocking
        /// </summary>	
        public bool? GroupIsLocking { get; set; }

        /// <summary>
        /// GroupIsDelete
        /// </summary>	
        public bool? GroupIsDelete { get; set; }

        /// <summary>
        /// RelatedId
        /// </summary>	
        public string RelatedId { get; set; }

        /// <summary>
        /// RoleId
        /// </summary>	
        public string RoleId { get; set; }

        /// <summary>
        /// RoleName
        /// </summary>	
        public string RoleName { get; set; }

        /// <summary>
        /// RoleType
        /// </summary>	
        public int? RoleType { get; set; }

        /// <summary>
        /// RoleIsDefault
        /// </summary>	
        public bool? RoleIsDefault { get; set; }

        /// <summary>
        /// RoleIsDelete
        /// </summary>	
        public bool? RoleIsDelete { get; set; }

        /// <summary>
        /// AddName
        /// </summary>	
        public string AddName { get; set; }
    }
}
