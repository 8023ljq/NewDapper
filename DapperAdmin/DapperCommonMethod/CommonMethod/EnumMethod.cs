using System;
using System.ComponentModel;
using System.Linq;

namespace DapperCommonMethod.CommonMethod
{
    public static class EnumMethod
    {
        /// <summary>
        /// 获取枚举属性值(直接枚举点出来)
        /// </summary>
        /// <param name="o"></param>
        /// <returns></returns>
        public static string GetDescription(this object o)
        {
            return GetEnumAtribute(o);
        }

        /// <summary>
        /// 调用方法
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetEnumAtribute(object obj)
        {
            if (obj == null)
                return string.Empty;
            var o = GetCustomAttribute<DescriptionAttribute>(obj);
            if (o != null)
                return o.Description;
            return obj.ToString();
        }

        /// <summary>
        /// 调用方法
        /// </summary>
        /// <typeparam name="ATT"></typeparam>
        /// <param name="o"></param>
        /// <returns></returns>
        public static ATT GetCustomAttribute<ATT>(object o) where ATT : Attribute
        {
            if (o == null)
                return default(ATT);
            System.Reflection.FieldInfo f = o.GetType().GetField(o.ToString());
            if (f == null)
                return default(ATT);
            var a = f.GetCustomAttributes(typeof(ATT), true).FirstOrDefault();
            if (a == null)
                return default(ATT);
            else
                return (ATT)a;
        }

        /// <summary>
        /// int获取枚举的描述的扩展方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="nameInstend"></param>
        /// <returns></returns>
        public static string GetDesString<T>(this int value, bool nameInstend = true)
        {
            if (typeof(T).IsEnum)
            {
                T ms = (T)System.Enum.Parse(typeof(T), System.Enum.GetName(typeof(T), value));
                return ms.GetDescription();
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// 根据枚举的int值取出string值
        /// </summary>
        public static string GetEnunCallFunction<T>(int i)
        {
            T call = (T)Enum.Parse(typeof(T), i.ToString());
            return call.ToString();
        }
    }
}
