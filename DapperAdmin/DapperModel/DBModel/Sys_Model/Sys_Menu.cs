using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DapperCommonMethod.DBModel.Sys_Model
{
    /// <summary>
    /// 系统菜单
    /// </summary>
    public class Sys_Menu
    {
	     
        /// <summary>
        /// 主键Id
        /// </summary>	
        public string Id { get; set; }
 
        /// <summary>
        /// 父级Id
        /// </summary>	
        public string ParentId { get; set; }
 
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


