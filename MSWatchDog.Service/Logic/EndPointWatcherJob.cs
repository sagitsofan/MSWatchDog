using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Quartz;
using Quartz.Impl;

using MSWatchDog.Modules.DashManifest;
using MSWatchDog.Logic;

namespace MSWatchDog.Logic
{
    public class EndPointWatcherJob : IJob
    {
        /// <summary>
        /// Main enterence point
        /// </summary>
        /// <param name="context"></param>
        public void Execute(IJobExecutionContext context)
        {
            var Url = context.JobDetail.JobDataMap["Url"].ToString();
            var Name = context.JobDetail.JobDataMap["Name"].ToString();

            new EndPoint(Url, Name).Process();
        }
    }
}
