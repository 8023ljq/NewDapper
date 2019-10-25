using Aliyun.Acs.Core;
using Aliyun.Acs.Core.Exceptions;
using Aliyun.Acs.Core.Profile;
using Aliyun.Acs.Dysmsapi.Model.V20170525;
using System;

namespace DapperCommonMethod.CommonMethod
{
    /// <summary>
    /// Author：Geek Dog  Content：发送短信方法 AddTime：2019-10-23 14:53:29  
    /// </summary>
    public class SMSHelpMethod
    {
        /// <summary>
        /// 阿里云账号上获取
        /// </summary>
        private const string SMS_ACCESSKEYID = "LTAIUbrTx3CgNOz2";
        private const string SMS_SECRET = "89lt0WiKfx8PwnRv99h7hqI2u9Ntz5";
        /// <summary>
        /// 短信签名
        /// </summary>
        private const string SMS_SIGNNAME = "咔哒";
        /// <summary>
        /// 发送短信号码
        /// </summary>
        /// <param name="mobilePhone">手机号码</param>
        /// <param name="templateCode">发送的模板编号(阿里云上配置模板)</param>
        /// <param name="templateParam">发送的模板里参数值如模板:您的验证码为${code},则参数值为{'code':'12345'}</param>
        public static bool Send(string mobilePhone, string templateCode, string templateParam)
        {
            bool bo;
            String product = "Dysmsapi";//短信API产品名称
            String domain = "dysmsapi.aliyuncs.com";//短信API产品域名
            IClientProfile profile = DefaultProfile.GetProfile("cn-hangzhou", SMS_ACCESSKEYID, SMS_SECRET);
            DefaultProfile.AddEndpoint("cn-hangzhou", "cn-hangzhou", product, domain);
            IAcsClient acsClient = new DefaultAcsClient(profile);
            SendSmsRequest request = new SendSmsRequest();
            try
            {
                //必填:待发送手机号。支持以逗号分隔的形式进行批量调用，批量上限为20个手机号码,批量调用相对于单条调用及时性稍有延迟,验证码类型的短信推荐使用单条调用的方式
                request.PhoneNumbers = mobilePhone;
                request.TemplateParam = templateParam;
                request.SignName = SMS_SIGNNAME;
                request.TemplateCode = templateCode;
                //请求失败这里会抛ClientException异常
                SendSmsResponse sendSmsResponse = acsClient.GetAcsResponse(request);
                //System.Console.WriteLine(sendSmsResponse.Message);
                //以下各种情况的判断要根据不同平台具体调整
                if (sendSmsResponse.Message.Contains("OK"))//返回成功
                {
                    bo = true;
                }
                else
                {
                    bo = false;
                }
                return bo;
            }
            catch (ServerException e)
            {
                WriteLogMethod.WriteLogs(e);
                return false;
            }
            catch (ClientException e)
            {
                WriteLogMethod.WriteLogs(e);
                return false;
            }
            catch (Exception ex)
            {
                WriteLogMethod.WriteLogs(ex);
                return false;
            }
        }

        /// <summary>
        /// Author：Geek Dog  Content：发送短信提醒 AddTime：2019-5-6 17:23:12  
        /// </summary>
        /// <param name="mobilePhone">手机号</param>
        /// <param name="templateCode">短信编码</param>
        /// <param name="templateParam">短信内容</param>
        public static void SMSSend(string mobilePhone, string templateCode, string templateParam)
        {
            string product = "Dysmsapi";//短信API产品名称
            string domain = "dysmsapi.aliyuncs.com";//短信API产品域名
            IClientProfile profile = DefaultProfile.GetProfile("cn-hangzhou", SMS_ACCESSKEYID, SMS_SECRET);
            DefaultProfile.AddEndpoint("cn-hangzhou", "cn-hangzhou", product, domain);
            IAcsClient acsClient = new DefaultAcsClient(profile);
            SendSmsRequest request = new SendSmsRequest();
            try
            {
                //必填:待发送手机号。支持以逗号分隔的形式进行批量调用，批量上限为20个手机号码,批量调用相对于单条调用及时性稍有延迟,验证码类型的短信推荐使用单条调用的方式
                request.SignName = SMS_SIGNNAME;
                request.PhoneNumbers = mobilePhone;
                request.TemplateParam = templateParam;
                request.TemplateCode = templateCode;
                //请求失败这里会抛ClientException异常
                SendSmsResponse sendSmsResponse = acsClient.GetAcsResponse(request);
                //System.Console.WriteLine(sendSmsResponse.Message);
                //以下各种情况的判断要根据不同平台具体调整
                if (sendSmsResponse.Message.Contains("OK"))//返回成功
                {
                    return;
                }
                else
                {
                    WriteLogMethod.WriteLogs(null, sendSmsResponse.Message);
                }
            }
            catch (ServerException e)
            {
                WriteLogMethod.WriteLogs(e);
            }
            catch (ClientException e)
            {
                WriteLogMethod.WriteLogs(e);
            }
            catch (Exception ex)
            {
                WriteLogMethod.WriteLogs(ex);
            }
        }
    }
}
