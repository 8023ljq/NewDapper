using Quartz;
using Quartz.Impl;
using System.Collections.Specialized;

namespace DapperJobService.JobService
{
    public class JobScheduler
    {
        public async void Start()
        {
            var props = new NameValueCollection
            {
                { "quartz.serializer.type", "binary" }
            };
            StdSchedulerFactory schedFact = new StdSchedulerFactory(props);

            IScheduler sched = await schedFact.GetScheduler();
            await sched.Start();

            IJobDetail job = JobBuilder.Create<EricSimpleJob>()
                .WithIdentity("EricJob", "EricGroup")
                .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("EricTrigger", "EricGroup")
                .WithSimpleSchedule(x => x.WithIntervalInSeconds(5).RepeatForever())
                .Build();

            await sched.ScheduleJob(job, trigger);
        }

        public async void Stop()
        {
            IScheduler sched = await StdSchedulerFactory.GetDefaultScheduler();
            await sched.Shutdown();
        }
    }
}
