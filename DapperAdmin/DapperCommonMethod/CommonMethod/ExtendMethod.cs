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

namespace DapperCommonMethod.CommonMethod
{
    /// <summary>
    /// Author：Geek Dog  Content：扩展方法 AddTime：2019-11-14 13:55:46  
    /// </summary>
    public static class ExtendMethod
    {
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
    }
}
