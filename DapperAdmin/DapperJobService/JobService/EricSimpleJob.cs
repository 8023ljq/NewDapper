using Quartz;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DapperJobService.JobService
{
    public class EricSimpleJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            string filepath = @"C:\timertest.txt";

            if (!File.Exists(filepath))
            {
                using (FileStream fs = File.Create(filepath)) { }
            }

            using (StreamWriter sw = new StreamWriter(filepath, true))
            {
                sw.WriteLine(DateTime.Now.ToLongTimeString());
            }

            return Task.CompletedTask;
        }
    }
}
