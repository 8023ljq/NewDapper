using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DapperModel
{
    /// <summary>
    /// 后台管理员
    /// </summary>
    public partial class Sys_Manager
    {
	     
        /// <summary>
        /// 主键Id
        /// </summary>	
        public string Id { get; set; }
 
        /// <summary>
        /// 角色ID
        /// </summary>	
        public string RoleId { get; set; }
 
        /// <summary>
        /// 用户名
        /// </summary>	
        public string Name { get; set; }
 
        /// <summary>
        /// 登录密码
        /// </summary>	
        public string Password { get; set; }
 
        /// <summary>
        /// 加密随机码
        /// </summary>
        public string RandomCode { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary>	
        public string Avatar { get; set; }
 
        /// <summary>
        /// 用户昵称
        /// </summary>	
        public string Nickname { get; set; }
 
        /// <summary>
        /// 手机号
        /// </summary>	
        public string Phone { get; set; }
 
        /// <summary>
        /// 邮箱地址
        /// </summary>	
        public string Email { get; set; }
 
        /// <summary>
        /// 登录次数
        /// </summary>	
        public int LoginTimes { get; set; }
 
        /// <summary>
        /// 最后一次登录IP
        /// </summary>	
        public string LastLoginIP { get; set; }
 
        /// <summary>
        /// 最后一次登录时间
        /// </summary>	
        public DateTime LastLoginTime { get; set; }
 
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
        /// 是否锁定(0:否1:是)
        /// </summary>	
        public bool IsLocking { get; set; }
 
        /// <summary>
        /// 是否删除(0:否1:是)
        /// </summary>	
        public bool IsDelete { get; set; }
 
        /// <summary>
        /// 备注
        /// </summary>	
        public string Remarks { get; set; }

        /// <summary>
        /// 管理员登录票据ID
        /// </summary>
        public string TokenId { get; set; }
    }
}


