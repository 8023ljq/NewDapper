using System.Text.RegularExpressions;

namespace DapperCommonMethod.CommonMethod
{
    public  class RegexUtilsMethod
    {
        /// <summary>
        /// Author：Geek Dog  Content：验证邮箱 AddTime：2019-1-8 15:43:44  
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <returns></returns>
        public static bool CheckEmail(string email)
        {
            Regex reg = new Regex(@"^[\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+([\,\w-]+(\.[\w-]+)*@[\w-]+(\.[\w-]+)+)*$");
            return reg.IsMatch(email);
        }

        /// <summary>
        /// Author：Geek Dog  Content：验证手机号 AddTime：2019-1-8 16:28:23  
        /// </summary>
        /// <param name="Phone">手机号</param>
        /// <returns></returns>
        public static bool CheckPhone(string Phone)
        {
            Regex reg = new Regex(@"^0{0,1}(13[0-9]|14[0-9]|15[0-9]|16[0-9]|17[0-9]|18[0-9]|19[0-9])[0-9]{8}$");
            return reg.IsMatch(Phone);
        }

        /// <summary>
        /// Author：Geek Dog  Content：验证用户密码(6~16位数字,字母组合格式) AddTime：2019-1-8 16:42:24  
        /// </summary>
        /// <param name="Pwd">用户密码</param>
        /// <returns></returns>
        public static bool CheckPwd(string Pwd)
        {
            Regex reg = new Regex(@"^(?![0-9]+$)(?![a-zA-Z]+$)[0-9A-Za-z]{6,16}$");
            return reg.IsMatch(Pwd);
        }

        /// <summary>
        /// Author：Geek Dog  Content：验证用户名(4~16位数字,字母组合格式) AddTime：2019-1-9 9:40:18  
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <returns></returns>
        public static bool CheckUserName(string UserName)
        {
            Regex reg = new Regex(@"^[A-Za-z0-9]{6,16}$");
            return reg.IsMatch(UserName);
        }

        /// <summary>
        /// Author：Geek Dog  Content：检验联系人格式(0-10位数字/字母/汉字) AddTime：2019-2-14 16:29:38  
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public static bool CheckContact(string UserName)
        {
            Regex reg = new Regex(@"^[\u4e00-\u9fa5_a-zA-Z0-9_]{0,10}$");
            return reg.IsMatch(UserName);
        }

        /// <summary>
        /// Author：Geek Dog  Content：检验联系人格式(2-6位汉字) AddTime：2019-2-14 16:29:38  
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public static bool CheckRealName(string UserName)
        {
            Regex reg = new Regex(@"^[\u4E00-\u9FA5]{2,6}$");
            return reg.IsMatch(UserName);
        }

        /// <summary>
        /// Author：Geek Dog  Content：验证身份证号(18位身份证号) AddTime：2019-2-15 14:58:08  
        /// </summary>
        /// <param name="IDCode"></param>
        /// <returns></returns>
        public static bool CheckIDCode(string IDCode)
        {
            Regex reg = new Regex(@"^(\d{6})(\d{4})(\d{2})(\d{2})(\d{3})([0-9]|X)$");
            return reg.IsMatch(IDCode);
        }

        /// <summary>
        /// Author：Geek Dog  Content：验证支付密码(6位纯数字) AddTime：2019-2-13 18:06:29  
        /// </summary>
        /// <param name="PayPwd"></param>
        /// <returns></returns>
        public static bool CheckPayPwd(string PayPwd)
        {
            Regex reg = new Regex(@"^\d{6}$");
            return reg.IsMatch(PayPwd);
        }

        /// <summary>
        /// 验证GuId
        /// </summary>
        /// <param name="GuID"></param>
        /// <returns></returns>
        public static bool CheckGuID(string GuID)
        {
            Regex reg = new Regex(@"^[a-fA-F0-9]{8}(-[a-fA-F0-9]{4}){3}-[a-fA-F0-9]{12}$");
            return reg.IsMatch(GuID);
        }

        /// <summary>
        /// Author：Geek Dog  Content：sql注入验证 AddTime：2019-2-13 18:06:29 
        /// </summary>
        /// <param name="SqlStr"></param>
        /// <returns></returns>
        public static bool CheckSql(string SqlStr)
        {
            Regex reg = new Regex(@"^[^(and|or|exec|insert|select|union|update|count|*|%)]*$");
            return reg.IsMatch(SqlStr);
        }
    }
}
