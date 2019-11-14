using System;

namespace DapperModel.DataModel
{
    /// <summary>
    /// 角色权限表
    /// </summary>
    public class Sys_RolePurview
    {

        /// <summary>
        /// 主键Id
        /// </summary>	
        public string Id { get; set; }

        /// <summary>
        /// 所属角色Id
        /// </summary>	
        public string RoleId { get; set; }

        /// <summary>
        /// 资源Id(菜单或是按钮的主键ID)
        /// </summary>	
        public string ResourceId { get; set; }

        /// <summary>
        /// 资源类型(0菜单1按钮)
        /// </summary>	
        public int? ResourceType { get; set; }

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
        public bool? IsLocking { get; set; }

        /// <summary>
        /// 是否删除(0:否1:是)
        /// </summary>	
        public bool? IsDelete { get; set; }

    }
}
