﻿using System;

namespace DapperModel.ViewModel
{
    /// <summary>
    /// Author：Geek Dog  Content：返回系统管理员信息 AddTime：2019-5-27 10:15:14  
    /// </summary>
    public class ManagerReturnModel
    {
        /// <summary>
        /// 管理员主键
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 管理员账号(Nickname)
        /// </summary>
        public string AdminName { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// 角色名
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        ///注册时间
        /// </summary>
        public DateTime RegisteTime { get; set; }
    }
}
