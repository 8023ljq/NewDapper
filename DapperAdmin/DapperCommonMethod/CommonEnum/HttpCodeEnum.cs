using System.ComponentModel;

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

        /// <summary>
        /// 删除成功
        /// </summary>
        [Description("删除成功")]
        Http_Delete_604 = 604,

        /// <summary>
        /// 删除失败
        /// </summary>
        [Description("删除失败")]
        Http_Delete_605 = 605,

        // ******************************************************************************
        // Author：Geek Dog  
        // Content：程序错误提示标识(1000--1999匹配json文件中的key值一一对应)  
        // AddTime：2019-1-8 16:32:05  
        //*******************************************************************************

        #region 程序错误提示标识

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

        /// <summary>
        /// 当前用户组尚有下级,请先删除下级用户组
        /// </summary>
        [Description("当前用户组尚有下级,请先删除下级用户组")]
        Http_1015 = 1015,

        /// <summary>
        /// 当前角色名已存在
        /// </summary>
        [Description("当前角色名已存在")]
        Http_1016 = 1016,

        /// <summary>
        /// 您不是超级管理员,无权修改角色信息
        /// </summary>
        [Description("您不是超级管理员,无权修改角色信息")]
        Http_1017 = 1017,

        /// <summary>
        /// 当前角色下绑定有管理员,无法删除
        /// </summary>
        [Description("当前角色下绑定有管理员,无法删除")]
        Http_1018 = 1018,

        /// <summary>
        /// 当期角色属于超级管理员,不允许操作
        /// </summary>
        [Description("当期角色属于超级管理员,不允许操作")]
        Http_1019 = 1019,

        /// <summary>
        /// 当期角色属于超级管理员,不允许删除
        /// </summary>
        [Description("当期角色属于超级管理员,不允许删除")]
        Http_1020 = 1020,

        #endregion

        // ******************************************************************************
        // Author：Geek Dog  
        // Content：实体数据验证错误码(2000-2999匹配json文件中的key值一一对应)  
        // AddTime：2019-1-8 16:32:05  
        //*******************************************************************************

        #region 实体数据验证错误码

        /// <summary>
        /// 请将必填数据填写完整
        /// </summary>
        [Description("请将必填数据填写完整")]
        Http_Erify_2000 = 2000,

        /// <summary>
        /// 长度最大80个字符
        /// </summary>
        [Description("长度最大80个字符")]
        Http_Erify_2001 = 2001,

        #endregion
    }
}
