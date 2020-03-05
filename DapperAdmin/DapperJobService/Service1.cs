using DapperJobService.JobService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace DapperJobService
{
    public partial class Service1 : ServiceBase
    {
        JobScheduler scheduler;

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            scheduler = new JobScheduler();
            scheduler.Start();
        }

        protected override void OnStop()
        {
            if (scheduler != null)
                scheduler.Stop();
        }
    }
}
