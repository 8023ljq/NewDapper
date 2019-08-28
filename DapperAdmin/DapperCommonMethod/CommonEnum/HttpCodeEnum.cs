﻿using System.ComponentModel;

namespace DapperCommonMethod.CommonEnum
{
    /// <summary>
    /// HttpCodeEnum返回状态码
    /// </summary>
    public enum HttpCodeEnum
    {
        /// <summary>
        /// 请求成功
        /// </summary>
        [Description("请求成功")]
        Http_200 = 200,

        /// <summary>
        /// 请求服务失败
        /// </summary>
        [Description("请求服务失败")]
        Http_300 = 300,

        /// <summary>
        /// 请求数据不存在
        /// </summary>
        [Description("请求数据不存在")]
        Http_400 = 400,

        /// <summary>
        /// 服务内部错误
        /// </summary>
        [Description("服务内部错误")]
        Http_500 = 500,

        /// <summary>
        /// 登录掉线
        /// </summary>
        [Description("登录掉线")]
        Http_700 = 700,

        /// <summary>
        /// 添加成功
        /// </summary>
        [Description("添加成功")]
        Http_Add_600 = 600,

        /// <summary>
        /// 添加失败
        /// </summary>
        [Description("添加失败")]
        Http_Add_601 = 601,


        /// <summary>
        /// 修改成功
        /// </summary>
        [Description("修改成功")]
        Http_Update_602 = 602,

        /// <summary>
        /// 修改失败
        /// </summary>
        [Description("修改失败")]
        Http_Update_603 = 603,

        // ******************************************************************************
        // Author：Geek Dog  
        // Content：1000--2999匹配json文件中的key值(一一对应)  
        // AddTime：2019-1-8 16:32:05  
        //*******************************************************************************

        /// <summary>
        /// 请将必填数据填写完整
        /// </summary>
        [Description("请将必填数据填写完整")]
        Http_1000 = 1000,

        /// <summary>
        /// 登录成功
        /// </summary>
        [Description("登录成功")]
        Http_1001 = 1001,

        /// <summary>
        /// 用户名或密码错误
        /// </summary>
        [Description("用户名或密码错误")]
        Http_1002 = 1002,

        /// <summary>
        /// 请输入用户名
        /// </summary>
        [Description("请输入用户名")]
        Http_1003 = 1003,

        /// <summary>
        /// 请输入密码
        /// </summary>
        [Description("请输入密码")]
        Http_1004 = 1004,

        /// <summary>
        /// 请您先登录
        /// </summary>
        [Description("请您先登录")]
        Http_1005 = 1005,

        /// <summary>
        /// 退出成功
        /// </summary>
        [Description("退出成功")]
        Http_1006 = 1006,

        /// <summary>
        /// 退出失败
        /// </summary>
        [Description("退出失败")]
        Http_1007 = 1007,

        /// <summary>
        /// 未找到当前管理员信息
        /// </summary>
        [Description("未找到当前管理员信息")]
        Http_1008 = 1008,

        /// <summary>
        /// 当前账号已存在
        /// </summary>
        [Description("当前账号已存在")]
        Http_1009 = 1009,

        /// <summary>
        /// 当前昵称已存在
        /// </summary>
        [Description("当前昵称已存在")]
        Http_1010 = 1010,

        /// <summary>
        /// 当前联系电话已存在
        /// </summary>
        [Description("当前联系电话已存在")]
        Http_1011 = 1011,

        /// <summary>
        /// 当前邮箱地址已存在
        /// </summary>
        [Description("当邮箱地址已存在")]
        Http_1012 = 1012,

        /// <summary>
        /// 当前管理组名已存在
        /// </summary>
        [Description("当前管理组名已存在")]
        Http_1013 = 1013,

        /// <summary>
        /// 未找到当前用户组
        /// </summary>
        [Description("未找到当前用户组")]
        Http_1014 = 1014,


    }
}
