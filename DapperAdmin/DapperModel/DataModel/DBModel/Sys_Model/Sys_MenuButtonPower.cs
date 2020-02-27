using System;

namespace DapperModel.DataModel
{
    /// <summary>
    /// 菜单按钮权限表
    /// </summary>
    [Table("Sys_MenuButtonPower")]
    public class Sys_MenuButtonPower
    {
        /// <summary>
        /// 主键Id
        /// </summary>	
        [ExplicitKey]
        public string Id { get; set; }

        /// <summary>
        /// 关联Id(菜单关联)
        /// </summary>	
        public string RelationMenuId { get; set; }

        /// <summary>
        /// 关联Id(按钮关联)
        /// </summary>	
        public string RelationButtonId { get; set; }

        /// <summary>
        /// 关联Id(角色表主键关联)
        /// </summary>
        public string RelationRoleId { get; set; }

        /// <summary>
        /// 按钮名称
        /// </summary>	
        public string ButtonName { get; set; }
 
        /// <summary>
        /// 按钮标识
        /// </summary>	
        public string ButtonMark { get; set; }
 
        /// <summary>
        /// 请求地址
        /// </summary>	
        public string RequestUrl { get; set; }
 
        /// <summary>
        /// 是否显示
        /// </summary>	
        public bool? IsShow { get; set; }
 
        /// <summary>
        /// 是否默认
        /// </summary>	
        public bool? IsDefault { get; set; }
 
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
        public bool? IsDelete { get; set; }
 
        /// <summary>
        /// 备注
        /// </summary>	
        public string Remarks { get; set; }

    }
}


