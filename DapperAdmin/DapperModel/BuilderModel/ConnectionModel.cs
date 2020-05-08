using System.ComponentModel.DataAnnotations;

namespace DapperModel.BuilderModel
{
    /// <summary>
    /// 连接数据库实体参数
    /// </summary>
    public class ConnectionModel
    {
        /// <summary>
        /// 服务器地址
        /// </summary>
        [Required(ErrorMessage = "1000")]
        public string Ip { get; set; }

        /// <summary>
        /// 登录账号
        /// </summary>
        [Required(ErrorMessage = "1000")]
        public string Account { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        [Required(ErrorMessage = "1000")]
        public string Pwd { get; set; }
    }
}
