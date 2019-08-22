using DapperCommonMethod.CommonConfig;
using DapperCommonMethod.CommonMethod;
using DapperCommonMethod.CommonModel;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace DapperAdminApi.Controllers.Common
{
    /// <summary>
    /// Author：Geek Dog  Content： AddTime：2019-6-26 10:16:36  
    /// </summary>
    [RoutePrefix("api/uploadfile")]
    public class UploadFileController : ApiController
    {
        [HttpPost]
        [Route("newuploadfile")]
        public IHttpActionResult NewUploadFile()
        {
            ResultMsg result = new ResultMsg();
            string[] pictureFormatArray = AppSettingsConfig.UploadFormat.Split(',');
            try
            {
                //获取上传图片文件
                var fileImg = HttpContext.Current.Request.Files[0];
                Stream userfile = fileImg.InputStream;//.InputStream;
                string ext1 = Path.GetExtension(fileImg.FileName).ToLower();//获取文件扩展名(后缀)

                int UploadSize = int.Parse(AppSettingsConfig.UploadFileSize);
                //判断文件大小不允许超过100Mb
                if (fileImg.ContentLength > (UploadSize * 1024 * 1024))
                {
                    result.ResultCode = -1;
                    result.ResultMsgs = "上传失败,文件大小超过100MB";
                    return Ok(result);
                }

                //检查文件后缀名
                if (!pictureFormatArray.Contains(ext1.TrimStart('.')))
                {
                    result.ResultCode = -1;
                    result.ResultMsgs = "上传失败,文件格式必须为" + AppSettingsConfig.UploadFormat + "类型";
                    return Ok(result);
                }
                using (HttpClient client = new HttpClient())
                {
                    var request = new HttpRequestMessage(HttpMethod.Post, AppSettingsConfig.ServerImgaes + "/Upload/UploadImgageFromWeb?ImgPathEnum=" + 1 + "&IsFullPath=false" + "&ext=" + ext1.Substring(1));
                    var content = new MultipartFormDataContent();
                    content.Add(new StreamContent(userfile), "file", "file.jpg");
                    //content.Add(new StreamContent(HttpContext.Request.Form.Files[0].OpenReadStream()), "file", "file.jpg");
                    request.Content = content;

                    var response = client.SendAsync(request).Result;
                    response.EnsureSuccessStatusCode();
                    var filenamestr = response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<ResultMsg>(filenamestr.Result);
                    return Ok(result);
                }
            }
            catch (Exception ex)
            {
                WriteLogMethod.WriteLogs(ex);
                result.ResultCode = -1;
                result.ResultMsgs = "上传失败";
                return Ok(result);
            }
        }
    }
}

