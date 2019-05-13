using DapperCommonMethod.CommonConfig;
using DapperCommonMethod.CommonModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
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
                string JsonUrl = CommonConfigs.BankJsonName;
                string ApiUrl = CommonConfigs.BankNameApi;
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
                    msg.code = 1;

                    using (System.IO.StreamReader file = System.IO.File.OpenText(JsonUrl))
                    {
                        using (JsonTextReader reader = new JsonTextReader(file))
                        {
                            JObject jObject = (JObject)JToken.ReadFrom(reader);
                            str = jObject["" + result.bank + ""].ToString();
                        }
                    }
                    msg.msg = str;
                }
            }
            catch (Exception ex)
            {
                WriteLogMethod.WriteLogs(ex);
                msg.code = 0;
                msg.msg = "银行卡格式错误";
            }
            return msg;
        }
    }
}
