using DapperCommonMethod.CommonConfig;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace DapperCommonMethod.CommonJson
{
    /// <summary>
    /// Author：Geek Dog  Content：Josn帮助类 AddTime：2019-5-23 10:06:57  
    /// </summary>
    public static class JosnHelp
    {
        #region 格式转换

        public static object ToJson(this string Json)
        {
            return Json == null ? null : JsonConvert.DeserializeObject(Json);
        }
        public static string ToJson(this object obj)
        {
            var timeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };
            return JsonConvert.SerializeObject(obj, timeConverter);
        }
        public static string ToJson(this object obj, string datetimeformats)
        {
            var timeConverter = new IsoDateTimeConverter { DateTimeFormat = datetimeformats };
            return JsonConvert.SerializeObject(obj, timeConverter);
        }
        public static T ToObject<T>(this string Json)
        {
            return Json == null ? default(T) : JsonConvert.DeserializeObject<T>(Json);
        }
        public static List<T> ToList<T>(this string Json)
        {
            return Json == null ? null : JsonConvert.DeserializeObject<List<T>>(Json);
        }
        public static DataTable ToTable(this string Json)
        {
            return Json == null ? null : JsonConvert.DeserializeObject<DataTable>(Json);
        }
        public static JObject ToJObject(this string Json)
        {
            return Json == null ? JObject.Parse("{}") : JObject.Parse(Json.Replace("&nbsp;", ""));
        }

        #endregion

        #region 读取josn文件

        /// <summary>
        /// 获取JSON文件并返回对应返回值
        /// </summary>
        /// <param name="Key">错误编号</param>
        /// <param name="Language">语言类型</param>
        /// <returns></returns>
        public static string Readjson(object Key, string Language)
        {
            string jsonfile = String.Empty;
            string WebUrl = HostingEnvironment.MapPath("~/");
            if (Language == LanguageConfig.CN)
            {
                jsonfile = AppSettingsConfig.CNJsonAddress;//JSON文件路径
            }
            else
            {
                jsonfile = AppSettingsConfig.ENJsonAddress;//JSON文件路径
            }


            using (System.IO.StreamReader file = System.IO.File.OpenText(WebUrl+jsonfile))
            {
                using (JsonTextReader reader = new JsonTextReader(file))
                {
                    JObject jObject = (JObject)JToken.ReadFrom(reader);
                    var value = jObject[Key].ToString();
                    return value;
                }
            }
        }

        #endregion
    }
}
