using Dapper.Contrib.Extensions;
using System;

namespace DapperModel.DataModel
{
    /// <summary>
    /// 后台管理员角色表
    /// </summary>
    [Table("L_AdminLoginLog")]
    public partial class Sys_ManagerRole
    {

        /// <summary>
        /// 主键Id
        /// </summary>	
        [ExplicitKey]
        public string Id { get; set; }

        /// <summary>
        /// 角色名称
        /// </summary>	
        public string RoleName { get; set; }

        /// <summary>
        /// 角色类型(1:超管2:系管[具体查看项目枚举RoleTypeEnum])
        /// </summary>	
        public int RoleType { get; set; }

        /// <summary>
        /// 是否默认(只可在数据进行修改)
        /// </summary>	
        public bool IsDefault { get; set; }

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
        /// 是否删除(0:否1:是)
        /// </summary>	
        public bool IsDelete { get; set; }

        /// <summary>
        /// 是否锁定(0:否1:是)
        /// </summary>	
        public bool IsLocking { get; set; }

        /// <summary>
        /// 备注
        /// </summary>	
        public string Remarks { get; set; }

    }
}


