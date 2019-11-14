using System.ComponentModel.DataAnnotations;

namespace DapperModel.ViewModel
{
    /// <summary>
    /// Author：Geek Dog  
    /// Content：登录请求类   Required数据验证  ErrorMessage参数必须与json文件里面的值保持一致由HttpCodeEnum统一管理
    /// AddTime：2019-5-22 17:52:25  
    /// </summary>
    public class LoginModelRequest
    {
        /// <summary>
        /// 用户名
        /// 1003:请输入用户名
        /// </summary>
        [Required(ErrorMessage = "1003")]
        [RegularExpression(@"^[A-Za-z0-9]{4,16}$", ErrorMessage = "1002")]
        public string UserName { get; set; }

        /// <summary>
        /// 登录密码
        /// 1004:请输入密码
        /// </summary>
        [Required(ErrorMessage = "1004")]
        [RegularExpression(@"^(?![0-9]+$)(?![a-zA-Z]+$)[0-9A-Za-z]{6,16}$", ErrorMessage = "1002")]
        public string PassWord { get; set; }
    }
}
