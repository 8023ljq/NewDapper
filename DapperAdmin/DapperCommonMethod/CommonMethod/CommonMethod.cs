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
    public static class CommonMethod
    {
        /// <summary>
        /// 返回银行卡归属银行
        /// </summary>
        /// <param name="GetBankNum">银行卡号</param>
        /// <returns></returns>
        public static ResultMsg GetBankName(string GetBankNum)
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
        public static void ReadInfoFromFile(string filePath = "")
        {
            filePath = "G:\\GIT\\NewDapper\\DapperAdmin\\DapperAdmin\\File\\DBModel.txt";
            if (File.Exists(filePath))
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

       
    }
}
