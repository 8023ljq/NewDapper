using DapperCommonMethod.CommonEnum;
using System;
using System.Globalization;
using System.Text;
using System.Threading;

namespace DapperCommonMethod.CommonMethod
{
    /// <summary>
    /// 扩展方法
    /// </summary>
    public class ExpandMethod
    {
        /// <summary>
        /// 生成随机位数的随机数
        /// </summary>
        /// <param name="Num">生成位数</param>
        /// <param name="Sleep">是否挂起线程</param>
        /// <param name="EnumType">生成类型</param>
        /// <returns></returns>
        public static string GetRandNum(int Num, bool Sleep, int EnumType)
        {
            if (Sleep)
            {
                Thread.Sleep(3);
            }
            char[] Pattern = new char[] { };

            switch (EnumType)
            {
                case (int)RandNumEnum.Number:
                    Pattern = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
                    break;
                case (int)RandNumEnum.NumberAndLetter:
                    Pattern = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
                    break;
                default:
                    break;
            }

            string result = "";
            int n = Pattern.Length;
            System.Random random = new Random(~unchecked((int)DateTime.Now.Ticks));
            for (int i = 0; i < Num; i++)
            {
                int rnd = random.Next(0, n);
                result += Pattern[rnd];

            }
            return result;
        }

        /// <summary>
        /// 生成不重复的编号(时间格式年月日时毫秒)
        /// </summary>
        /// <param name="PrefixStr">前缀字段</param>
        /// <returns></returns>
        public static string GetNumberId(string PrefixStr = "")
        {
            string Num = String.Empty;
            object Locker = new object();
            lock (Locker)  //lock 关键字可确保当一个线程位于代码的临界区时，另一个线程不会进入该临界区。
            {
                Thread.Sleep(3);
                Num = DateTime.Now.ToString("yyyyMMddHHfff", DateTimeFormatInfo.InvariantInfo);  //年月日时分秒
            }
            return PrefixStr + Num;
        }

        /// <summary>
        /// 处理手机号隐藏中间几位数字
        /// </summary>
        /// <returns></returns>
        public static string GetPhone(string Phone)
        {
            return Phone.Substring(0, 3) + "****" + Phone.Substring(Phone.Length - 4, 4);
        }

        /// <summary>
        /// 处理银行卡号隐藏中间几位数字
        /// </summary>
        /// <returns></returns>
        public static string GetBankCode(string BankCode)
        {
            return BankCode.Substring(0, 4) + " **** **** " + BankCode.Substring(BankCode.Length - 4, 4);
        }

        /// <summary>
        /// 金额小写转大写
        /// </summary>
        /// <param name="value">金额</param>
        /// <returns>中文大写</returns>
        public static string GetChineseNum(decimal value)
        {
            const string Chinese = "零壹贰叁肆伍陆柒捌玖";
            const string Unit = "元十百千万十百千亿十百千兆十百千";
            const string Unit2 = "角分";

            StringBuilder builder = new StringBuilder();
            long u = 1;
            int i = 0;

            // 整数   
            while (value >= u)
            {
                int n = (int)((long)value / u % 10);
                u *= 10;
                if (n == 0)
                {
                    i++;
                    continue;
                }
                if (i > 1 && i < 4)
                {
                    builder.Insert(0, "元");
                }
                else if (i > 4 && i < 8)
                {
                    builder.Insert(0, "万");
                }
                else if (i > 8)
                {
                    builder.Insert(0, "亿");
                }
                builder.Insert(0, Unit[i++]);
                builder.Insert(0, Chinese[n]);
            }

            // 小数   
            if ((long)value != value)
            {
                long value2 = (long)((value - (long)value) * 100);
                int n = (int)(value2 / 10 % 10);
                builder.Append(Chinese[n]);
                builder.Append(Unit2[0]);
                n = (int)(value2 % 10);
                builder.Append(Chinese[n]);
                builder.Append(Unit2[1]);
            }
            else
            {
                builder.Append("元整");
            }

            return builder.ToString();
        }

        /// <summary>
        /// 获取时间戳(string类型)  
        /// </summary>
        /// <returns></returns>
        public static string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }
    }
}
