using DapperCommonMethod.CommonConfig;
using DapperCommonMethod.CommonModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Web.Hosting;

namespace DapperCommonMethod.CommonMethod
{
    /// <summary>
    /// 公共调用方法
    /// </summary>
    public class CommonMethod
    {
        /// <summary>
        /// 返回银行卡归属银行
        /// </summary>
        /// <param name="GetBankNum">银行卡号</param>
        /// <returns></returns>
        public ResultMsg GetBankName(string GetBankNum)
        {
            ResultMsg msg = new ResultMsg();
            string str = String.Empty;
            try
            {
                string JsonUrl = AppSettingsConfig.BankJsonName;
                string ApiUrl = AppSettingsConfig.BankNameApi;
                string WebUrl = HostingEnvironment.MapPath("~/");
                JsonUrl = WebUrl + JsonUrl;
                ApiUrl = ApiUrl.Replace("##", GetBankNum);
                using (HttpClient client = new HttpClient())
                {
                    var request = new HttpRequestMessage(HttpMethod.Get, ApiUrl);
                    var response = client.SendAsync(request).Result;
                    response.EnsureSuccessStatusCode();
                    var filenamestr = response.Content.ReadAsStringAsync();
                    ResultBankApi result = JsonConvert.DeserializeObject<ResultBankApi>(filenamestr.Result);
                    msg.ResultCode = 1;

                    using (System.IO.StreamReader file = System.IO.File.OpenText(JsonUrl))
                    {
                        using (JsonTextReader reader = new JsonTextReader(file))
                        {
                            JObject jObject = (JObject)JToken.ReadFrom(reader);
                            str = jObject["" + result.bank + ""].ToString();
                        }
                    }
                    msg.ResultMsgs = str;
                }
            }
            catch (Exception ex)
            {
                WriteLogMethod.WriteLogs(ex);
                msg.ResultCode = 0;
                msg.ResultMsgs = "银行卡格式错误";
            }
            return msg;
        }

        /// <summary>
        /// 这个函数把文件的每一行读入list(暂时无用)
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public void ReadInfoFromFile(string filePath = "")
        {
            var templeturl = HostingEnvironment.MapPath("~/") + "\\File\\DBModel.txt";//模板文件路径

            if (File.Exists(templeturl))
            {
                StreamReader reader = new StreamReader(filePath, Encoding.Default);
                String a = reader.ReadToEnd();
                //将a.hhp文件中bb替换为cc。
                a = a.Replace("{Description}", "测试数据");

                var url = HostingEnvironment.MapPath("~/") + "\\Model";

                if (!Directory.Exists(url))
                {
                    //创建文件夹
                    Directory.CreateDirectory(url);
                }

                StreamWriter readTxt = new StreamWriter(url + '\\' + "Model.cs", false, Encoding.Default);
                readTxt.Write(a);
                readTxt.Flush();
                readTxt.Close();
                reader.Close();
                //b.hhp重命名为a.hhp,并删除b.hhp
                //File.Copy(@"b.hhp", @"a.hhp", true);
                //File.Delete(@"b.hhp");
            }
        }

        /// <summary>
        /// 处理字段属性转换
        /// </summary>
        /// <param name="dbType"></param>
        /// <returns></returns>
        public static string DBTypeToCSharpType(string dbType)
        {
            string cSharpType = string.Empty;
            switch (dbType.ToLower())
            {
                case "bit":
                    cSharpType = "bool";
                    break;
                case "tinyint":
                    cSharpType = "byte";
                    break;
                case "smallint":
                    cSharpType = "short";
                    break;
                case "int":
                    cSharpType = "int";
                    break;
                case "bigint":
                    cSharpType = "long";
                    break;
                case "real":
                    cSharpType = "float";
                    break;
                case "float":
                    cSharpType = "double";
                    break;
                case "smallmoney":
                case "money":
                case "decimal":
                case "numeric":
                    cSharpType = "decimal";
                    break;
                case "char":
                case "varchar":
                case "nchar":
                case "nvarchar":
                case "text":
                case "ntext":
                    cSharpType = "string";
                    break;
                case "samlltime":
                case "date":
                case "smalldatetime":
                case "datetime":
                case "datetime2":
                case "datetimeoffset":
                    cSharpType = "DateTime";
                    break;
                case "timestamp":
                case "image":
                case "binary":
                case "varbinary":
                    cSharpType = "byte[]";
                    break;
                case "uniqueidentifier":
                    cSharpType = "System.Guid";
                    break;
                case "variant":
                case "sql_variant":
                    cSharpType = "object";
                    break;
                default:
                    cSharpType = "string";
                    break;
            }
            return cSharpType;
        }

        /// <summary>
        /// 生成文件
        /// </summary>
        /// <param name="Path">文件路径</param>
        /// <param name="str">文件内容</param>
        public static void CreateFile(string Path, string str)
        {
            //创建对文件的引用
            FileInfo file = new FileInfo(Path);
            //获取父目录路径
            var di = file.Directory;
            //判断父目录是否存在
            if (!di.Exists)
                di.Create();
            //判断文件是否存在
            if (!file.Exists)
            {
                //创建文件 并释放文件资源
                FileStream fs = file.Create();
                fs.Close();
                fs.Dispose();
            }
            else
            {
                file.Delete();
                //创建文件 并释放文件资源
                FileStream fs = file.Create();
                fs.Close();
                fs.Dispose();
            }
            //写入流、释放资源
            StreamWriter sw = file.AppendText();
            sw.Write(str);
            sw.Close();
            sw.Dispose();

        }
    }
}
