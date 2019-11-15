using Dapper.Contrib.Extensions;
using System;
using System.ComponentModel.DataAnnotations;

namespace DapperModel.DataModel
{
    /// <summary>
    /// 后台管理员
    /// </summary>
    [Table("Sys_Manager")]
    public partial class Sys_Manager
    {

        /// <summary>
        /// 主键Id
        /// </summary>	
        [RegularExpression(@"^[a-fA-F0-9]{8}(-[a-fA-F0-9]{4}){3}-[a-fA-F0-9]{12}$", ErrorMessage = "5000")]
        public string Id { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>	
        [Required(ErrorMessage = "3000")]
        public string RelationId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>	
        [Required(ErrorMessage = "3001")]
        [RegularExpression(@"^[A-Za-z0-9]{6,16}$", ErrorMessage = "5001")]
        public string Name { get; set; }

        /// <summary>
        /// 登录密码(添加时单独验证)
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
        [Required(ErrorMessage = "3003")]
        [RegularExpression(@"^0{0,1}(13[0-9]|14[0-9]|15[0-9]|16[0-9]|17[0-9]|18[0-9]|19[0-9])[0-9]{8}$", ErrorMessage = "5003")]
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
        public DateTime? LastLoginTime { get; set; }

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
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 是否系统默认(0:否1:是)
        /// </summary>
        public bool IsDefault { get; set; }

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


