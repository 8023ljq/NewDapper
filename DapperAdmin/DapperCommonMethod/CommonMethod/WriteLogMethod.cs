using System;
using System.IO;
using System.Web.Hosting;

namespace DapperCommonMethod.CommonMethod
{
    /// <summary>
    /// 日志处理
    /// </summary>
    public class WriteLogMethod
    {
        /// <summary>
        /// Author：Geek Dog  Content：写入错误信息 AddTime：2019-1-7 16:04:13  
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="LogAddress"></param>
        public static void WriteLogs(Exception ex = null, string LogAddress = "")
        {
            //此处理是否存在对应的错误日志文件夹
            if (ex == null)
            {
                StreamWriter fs = new StreamWriter(LogAddress, true);
                fs.WriteLine(LogAddress);
                fs.WriteLine();
                fs.Close();
            }
            else
            {
                var url = HostingEnvironment.MapPath("~/") + "\\UploadLog";
                //var url = Environment.CurrentDirectory + "\\UploadLog";
                if (!Directory.Exists(url))
                {
                    //创建错误日志文件夹
                    Directory.CreateDirectory(url);
                }


                //如果日志文件为空，则默认在Debug目录下新建 YYYY-mm-dd_Log.log文件
                if (LogAddress == "")
                {
                    LogAddress = url + '\\' +
                        DateTime.Now.Year + '-' +
                        DateTime.Now.Month + '-' +
                        DateTime.Now.Day + "_Log.txt";
                }
                //把异常信息输出到文件，因为异常文件由这几部分组成，这样就不用我们自己复制到文档中了
                StreamWriter fs = new StreamWriter(LogAddress, true);
                fs.WriteLine("当前时间：" + DateTime.Now.ToString());
                fs.WriteLine("异常信息：" + ex.Message);
                fs.WriteLine("异常对象：" + ex.Source);
                fs.WriteLine("调用堆栈：\n" + ex.StackTrace.Trim());
                fs.WriteLine("触发方法：" + ex.TargetSite);
                fs.WriteLine();
                fs.Close();
            }
        }
    }
}
