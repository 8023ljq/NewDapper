using Dapper.Contrib.Extensions;
using System;
using System.ComponentModel.DataAnnotations;

namespace DapperModel.DataModel
{
    /// <summary>
    /// 系统菜单
    /// </summary>
    [Table("Sys_Menu")]
    public partial class Sys_Menu
    {
        /// <summary>
        /// 主键Id
        /// </summary>	
        [System.ComponentModel.DataAnnotations.Key]
        public string Id { get; set; }

        /// <summary>
        /// 自增标识
        /// </summary>
        [RegularExpression(@"^[a-fA-F0-9]{8}(-[a-fA-F0-9]{4}){3}-[a-fA-F0-9]{12}$", ErrorMessage = "5000")]
        public string GuId { get; set; }

        /// <summary>
        /// 父级Id
        /// </summary>	
        public string ParentId { get; set; }

        /// <summary>
        /// 所属资源类型(0菜单1按钮)
        /// </summary>
        public int ResourceType { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>	
        public string FullName { get; set; }

        /// <summary>
        /// 菜单层级
        /// </summary>	
        public int Layers { get; set; }

        /// <summary>
        /// 图标地址
        /// </summary>	
        public string IconUrl { get; set; }

        /// <summary>
        /// 连接地址
        /// </summary>	
        public string AddressUrl { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>	
        public int Sort { get; set; }

        /// <summary>
        /// 操作权限
        /// </summary>	
        public string Purview { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>	
        public bool IsShow { get; set; }

        /// <summary>
        /// 是否默认
        /// </summary>	
        public bool IsDefault { get; set; }

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


