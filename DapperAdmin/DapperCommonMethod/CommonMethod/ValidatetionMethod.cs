using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace DapperCommonMethod.CommonMethod
{
    /// <summary>
    /// 数据验证方法(帮助类)
    /// </summary>
    public class ValidatetionMethod
    {
        /// <summary>
        /// Author：Geek Dog  Content：数据验证 AddTime：2019-4-15 17:29:24  
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static ValidResult IsValid(object value)
        {
            ValidResult result = new ValidResult();
            try
            {
                var validationContext = new ValidationContext(value);
                var results = new List<ValidationResult>();
                var isValid = Validator.TryValidateObject(value, validationContext, results, true);

                if (!isValid)
                {
                    result.IsVaild = false;
                    result.ErrorMembers = results.FirstOrDefault().ErrorMessage;
                    //foreach (var item in results)
                    //{
                    //    result.ErrorMembers.Add(new ErrorMember()
                    //    {
                    //        ErrorMessage = item.ErrorMessage,
                    //        ErrorMemberName = item.MemberNames.FirstOrDefault()
                    //    });
                    //}
                }
                else
                {
                    result.IsVaild = true;
                }
            }
            catch (Exception)
            {
                result.IsVaild = false;
                result.ErrorMembers = "Internal error";
            }

            return result;
        }

        /// <summary>
        /// 验证参数
        /// </summary>
        public class ValidResult
        {
            /// <summary>
            /// 错误信息
            /// </summary>
            public string ErrorMembers { get; set; }
            /// <summary>
            /// 是否通过
            /// </summary>
            public bool IsVaild { get; set; }
        }

        public class ErrorMember
        {
            public string ErrorMessage { get; set; }
            public string ErrorMemberName { get; set; }
        }
    }
}
