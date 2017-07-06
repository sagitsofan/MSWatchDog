using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using Quartz;
using Quartz.Impl;

using MSWatchDog.Modules.DashManifest;
using MSWatchDog.Helper;
using MSWatchDog.Logic;

namespace MSWatchDog.Logic
{
    internal class Scheduler : IDisposable
    {
        private static Scheduler _instance;
        public static Scheduler Instance { get { return _instance; } }

        private IScheduler _sched;

        static Scheduler()
        {
            _instance = new Scheduler();
        }

        private Scheduler()
        {
            ISchedulerFactory schedFact = new StdSchedulerFactory();
            _sched = schedFact.GetScheduler();
            _sched.Start();
        }

        public void AddWatcher(string Name, string Url)
        {
            JobKey jobKey = new JobKey(Name, "group");
            TriggerKey triggerKey = new TriggerKey(Name);

            IJobDetail job = JobBuilder.Create<EndPointWatcherJob>()
                    .WithIdentity(jobKey)
                    .UsingJobData("Url", Url)
                    .UsingJobData("Name", Name)
                    .Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity(triggerKey)
                .WithSimpleSchedule(x => x.WithIntervalInSeconds(Config.WatcherInterval).RepeatForever())
                .StartNow()
                .Build();

            _sched.ScheduleJob(job, trigger);
        }

        public void Stop()
        {
            _sched.Shutdown();
        }

        public void Dispose()
        {
            if (_sched != null)
            {
                _sched.Clear();
            }
        }
    }
}
