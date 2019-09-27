using System;

namespace DapperModel.ViewModel.DBViewModel
{
    /// <summary>
    /// 管理员的角色具体数据
    /// </summary>
    public class View_ManagerRoleDetails
    {
        /// <summary>
        /// Id
        /// </summary>	
        public string Id { get; set; }

        /// <summary>
        /// RelationId
        /// </summary>	
        public string RelationId { get; set; }

        /// <summary>
        /// TokenId
        /// </summary>	
        public string TokenId { get; set; }

        /// <summary>
        /// Name
        /// </summary>	
        public string Name { get; set; }

        /// <summary>
        /// Password
        /// </summary>	
        public string Password { get; set; }

        /// <summary>
        /// RandomCode
        /// </summary>	
        public string RandomCode { get; set; }

        /// <summary>
        /// Avatar
        /// </summary>	
        public string Avatar { get; set; }

        /// <summary>
        /// Nickname
        /// </summary>	
        public string Nickname { get; set; }

        /// <summary>
        /// Phone
        /// </summary>	
        public string Phone { get; set; }

        /// <summary>
        /// Email
        /// </summary>	
        public string Email { get; set; }

        /// <summary>
        /// LoginTimes
        /// </summary>	
        public int LoginTimes { get; set; }

        /// <summary>
        /// LastLoginIP
        /// </summary>	
        public string LastLoginIP { get; set; }

        /// <summary>
        /// LastLoginTime
        /// </summary>	
        public DateTime LastLoginTime { get; set; }

        /// <summary>
        /// AddUserId
        /// </summary>	
        public string AddUserId { get; set; }

        /// <summary>
        /// AddTime
        /// </summary>	
        public DateTime AddTime { get; set; }

        /// <summary>
        /// UpdateUserId
        /// </summary>	
        public string UpdateUserId { get; set; }

        /// <summary>
        /// UpdateTime
        /// </summary>	
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// IsLocking
        /// </summary>	
        public bool IsLocking { get; set; }

        /// <summary>
        /// IsDelete
        /// </summary>	
        public bool IsDelete { get; set; }

        /// <summary>
        /// Remarks
        /// </summary>	
        public string Remarks { get; set; }

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
        public int RoleType { get; set; }

        /// <summary>
        /// IsDefault
        /// </summary>	
        public bool IsDefault { get; set; }

        /// <summary>
        /// RoleIsDelete
        /// </summary>	
        public bool RoleIsDelete { get; set; }
    }
}
