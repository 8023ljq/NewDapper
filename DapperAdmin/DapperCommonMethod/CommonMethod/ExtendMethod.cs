using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Reflection;
using DapperModel.CommonModel;
using DapperCommonMethod.CommonConfig;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Threading;
using DapperCommonMethod.CommonEnum;

namespace DapperCommonMethod.CommonMethod
{
    /// <summary>
    /// Author：Geek Dog  Content：扩展方法 AddTime：2019-11-14 13:55:46  
    /// </summary>
    public static class ExtendMethod
    {
        #region 常用方法

        /// <summary>
        /// 获取时间戳(string类型)  
        /// </summary>
        /// <returns></returns>
        public static string GetTimeStamp(this DateTime NowTime)
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        /// <summary>
        /// 处理手机号隐藏中间几位数字
        /// </summary>
        /// <returns></returns>
        public static string GetPhone(this string Phone)
        {
            return Phone.Substring(0, 3) + "****" + Phone.Substring(Phone.Length - 4, 4);
        }

        /// <summary>
        /// 处理银行卡号隐藏中间几位数字
        /// </summary>
        /// <returns></returns>
        public static string GetBankCode(this string BankCode)
        {
            return BankCode.Substring(0, 4) + " **** **** " + BankCode.Substring(BankCode.Length - 4, 4);
        }

        /// <summary>
        /// 金额小写转大写
        /// </summary>
        /// <param name="value">金额</param>
        /// <returns>中文大写</returns>
        public static string GetChineseNum(this decimal value)
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
        /// 生成不重复的编号(时间格式年月日时毫秒)
        /// </summary>
        /// <param name="Prefix">前缀字段</param>
        /// <returns></returns>
        public static string GetNumber(this int Prefix)
        {
            string PrefixStr = String.Empty;
            string Num = String.Empty;
            object Locker = new object();
            lock (Locker)  //lock 关键字可确保当一个线程位于代码的临界区时，另一个线程不会进入该临界区。
            {
                Thread.Sleep(3);
                Num = DateTime.Now.ToString("yyyyMMddHHfff", DateTimeFormatInfo.InvariantInfo);  //年月日时分秒
            }
            switch (Prefix)
            {
                case (int)NumberPrefixEnum.Apply:
                    PrefixStr = NumberPrefixEnum.Apply.GetDescription();
                    break;
                default:
                    break;
            }
            return PrefixStr + Num;
        }

        /// <summary>
        /// 生成随机位数的随机数
        /// </summary>
        /// <param name="Num">生成位数</param>
        /// <param name="Sleep">是否挂起线程</param>
        /// <param name="EnumType">生成类型</param>
        /// <returns></returns>
        public static string GetRandNum(this int EnumType, int Num, bool Sleep)
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
            Random random = new Random(~unchecked((int)DateTime.Now.Ticks));
            for (int i = 0; i < Num; i++)
            {
                int rnd = random.Next(0, n);
                result += Pattern[rnd];

            }
            return result;
        }

        #endregion

        #region 处理数据表格(导出操作)

        /// <summary>
        /// 表格最大行数
        /// </summary>
        private static int EXCEL03_MaxRow = 65535;

        /// <summary>
        /// 将DataTable转换为excel2003格式。
        /// </summary>
        /// <param name="dt">数据集</param>
        /// <param name="sheetName">生成文件名</param>
        /// <returns></returns>
        public static byte[] DataTable2Excel(this DataTable dt, string sheetName)
        {

            IWorkbook book = new HSSFWorkbook();
            if (dt.Rows.Count < EXCEL03_MaxRow)
                DataWrite2Sheet(dt, 0, dt.Rows.Count - 1, book, sheetName);
            else
            {
                int page = dt.Rows.Count / EXCEL03_MaxRow;
                for (int i = 0; i < page; i++)
                {
                    int start = i * EXCEL03_MaxRow;
                    int end = (i * EXCEL03_MaxRow) + EXCEL03_MaxRow - 1;
                    DataWrite2Sheet(dt, start, end, book, sheetName + i.ToString());
                }
                int lastPageItemCount = dt.Rows.Count % EXCEL03_MaxRow;
                DataWrite2Sheet(dt, dt.Rows.Count - lastPageItemCount, lastPageItemCount, book, sheetName + page.ToString());
            }
            MemoryStream ms = new MemoryStream();
            book.Write(ms);
            return ms.ToArray();
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="startRow"></param>
        /// <param name="endRow"></param>
        /// <param name="book"></param>
        /// <param name="sheetName"></param>
        private static void DataWrite2Sheet(DataTable dt, int startRow, int endRow, IWorkbook book, string sheetName)
        {
            ISheet sheet = book.CreateSheet(sheetName);
            IRow header = sheet.CreateRow(0);
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                ICell cell = header.CreateCell(i);
                string val = dt.Columns[i].Caption ?? dt.Columns[i].ColumnName;
                cell.SetCellValue(val);
            }
            int rowIndex = 1;
            for (int i = startRow; i <= endRow; i++)
            {
                DataRow dtRow = dt.Rows[i];
                IRow excelRow = sheet.CreateRow(rowIndex++);
                for (int j = 0; j < dtRow.ItemArray.Length; j++)
                {
                    excelRow.CreateCell(j).SetCellValue(dtRow[j].ToString());
                }
            }
        }

        /// <summary>
        /// List转DataTable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(this List<T> items)
        {
            var tb = new DataTable(typeof(T).Name);
            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in props)
            {
                string PropertyName = String.Empty;
                var attr = (DescriptionAttribute)Attribute.GetCustomAttribute(prop, typeof(DescriptionAttribute));
                if (attr != null)
                {
                    PropertyName = attr.Description.ToString();
                }
                else
                {
                    PropertyName = prop.Name;
                }
                Type t = GetCoreType(prop.PropertyType);
                tb.Columns.Add(PropertyName, t);
            }

            foreach (T item in items)
            {
                var values = new object[props.Length];

                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }

                tb.Rows.Add(values);
            }
            return tb;
        }

        /// <summary>
        /// 如果类型为空，则返回基础类型，否则返回类型
        /// </summary>
        public static Type GetCoreType(Type t)
        {
            if (t != null && IsNullable(t))
            {
                if (!t.IsValueType)
                {
                    return t;
                }
                else
                {
                    return Nullable.GetUnderlyingType(t);
                }
            }
            else
            {
                return t;
            }
        }

        /// <summary>
        /// 确定指定类型为空
        /// </summary>
        public static bool IsNullable(Type t)
        {
            return !t.IsValueType || (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>));
        }

        #endregion

        #region 对比实体赋值和修改记录

        /// <summary>
        /// 记录修改前后数据(单做记录处理)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t1">原数据实体</param>
        /// <param name="t2">新数据实体</param>
        /// <param name="logInfo">返回内容实体</param>
        /// <returns></returns>
        public static List<UpdateInfo> Comparison<T>(this T t1, T t2, List<UpdateInfo> logInfo = null) where T : class, new()
        {
            if (t1 == null || t2 == null)
            {
                return null;
            }
            Type type1 = t1.GetType();
            Type type2 = t2.GetType();
            PropertyInfo[] properties1 = type1.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            PropertyInfo[] properties2 = type2.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            string tStr = string.Empty;
            if (logInfo == null)
            {
                logInfo = new List<UpdateInfo>();
            }

            foreach (PropertyInfo item in properties1)
            {
                string PropertyName = item.Name;
                if (item.PropertyType.Name == "List`1")
                {
                    break;
                }
                //获取实体类的Description属性
                var attr = (DescriptionAttribute)Attribute.GetCustomAttribute(item, typeof(DescriptionAttribute));
                if (attr != null)
                {
                    PropertyName = attr.Description.ToString();
                }

                object value1 = item.GetValue(t1, null);
                object value2 = item.GetValue(t2, null);
                if (!item.PropertyType.IsConstructedGenericType)
                {
                    if (item.PropertyType != typeof(string) && !item.PropertyType.IsValueType && item.PropertyType != typeof(DateTime))
                    {
                        value1.Comparison(value2, logInfo);
                    }
                    else
                    {
                        string str1 = string.Empty;
                        string str2 = string.Empty;
                        if (value1 != null)
                        {
                            str1 = value1.ToString();
                        }
                        if (value2 != null)
                        {
                            str2 = value2.ToString();
                        }

                        if (str1 != str2)
                        {
                            UpdateInfo info = new UpdateInfo();
                            info.Key = PropertyName;
                            info.OldValue = str1.ToString();
                            info.NewValue = str2.ToString();
                            //tStr += $@"{PropertyName}的值由:'{value1}'更改为:'{value2}',";
                            logInfo.Add(info);
                        }
                    }
                }
            }
            return logInfo;
        }

        /// <summary>
        /// 根据同属性给要赋值的类(t1)赋值并生成记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        /// <param name="strName"></param>
        /// <param name="IsFiltration"></param>
        /// <returns></returns>
        public static List<UpdateInfo> UpdateLog<T>(this T t1, T t2, string strName = null, bool IsFiltration = true)
        {
            Type type1 = t1.GetType();
            Type type2 = t2.GetType();
            List<UpdateInfo> updateInfos = new List<UpdateInfo>();
            PropertyInfo[] oldpro = type1.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            //PropertyInfo[] newpro = type2.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            bool IsFiltra = IsFiltration;
            foreach (PropertyInfo sp in oldpro)
            {
                if (!String.IsNullOrEmpty(strName))
                {
                    var PropertyArr = strName.Split(',');
                    if (PropertyArr.Any(s => s == sp.Name))
                    {
                        if (sp.PropertyType != typeof(string) && !sp.PropertyType.IsValueType && sp.PropertyType != typeof(DateTime))
                        {
                            break;
                        }
                        UpdateInfo info = new UpdateInfo();
                        info.Key = sp.Name;
                        info.OldValue = sp.GetValue(t1).ToString();
                        info.NewValue = sp.GetValue(t2).ToString();
                        updateInfos.Add(info);
                        sp.SetValue(t1, sp.GetValue(t2));

                    }
                }
                else
                {
                    //开启就过滤并验证
                    if (IsFiltra)
                    {
                        IsFiltra = IsSetValue(sp.Name);
                    }
                    //不开启直接赋值
                    else
                    {
                        IsFiltra = false;
                    }
                    //属性相同并且值不为空赋值
                    if (IsFiltra && sp.GetValue(t2) != null)
                    {
                        if (sp.PropertyType != typeof(string) && !sp.PropertyType.IsValueType && sp.PropertyType != typeof(DateTime))
                        {
                            break;
                        }
                        UpdateInfo info = new UpdateInfo();
                        info.Key = sp.Name;
                        info.OldValue = sp.GetValue(t1).ToString();
                        info.NewValue = sp.GetValue(t2).ToString();
                        updateInfos.Add(info);
                        sp.SetValue(t1, sp.GetValue(t2));
                    }
                    //重置IsFiltra
                    IsFiltra = IsFiltration;
                }
            }
            return updateInfos;
        }

        /// <summary>
        /// 默认设置不赋值
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        public static bool IsSetValue(string Name)
        {
            string Filtration = AppSettingsConfig.Filtration;
            if (!String.IsNullOrEmpty(Filtration))
            {
                List<string> Filtra = Filtration.Split(',').ToList();
                if (Filtra.Contains(Name))
                {
                    return false;
                }
                return true;
            }
            else
            {
                return true;
            }
        }

        #endregion
    }
}
