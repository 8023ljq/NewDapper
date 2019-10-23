using System;

namespace DapperModel.CommonModel
{
    /// <summary>
    /// 存入Redis中的管理员数据
    /// </summary>
    public class RedisManagerModel
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public string RelationId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; set; }

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
        public DateTime? LastLoginTime { get; set; }

        /// <summary>
        /// 是否系统默认
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// 备注
        /// </summary>	
        public string Remarks { get; set; }
    }
}
