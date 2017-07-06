using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

using Quartz;
using Quartz.Impl;

using MSWatchDog.Logic;
using MSWatchDog.Helper;

namespace MSWatchDog.Service
{
    public static class Manager
    {
        public static void Start()
        {
            Logger.Write("WatcherHelper Start");

            try
            {
                var WatcherEndPointsSection = ConfigurationManager.GetSection(Consts.WATCHER_END_POINTS) as WatcherEndPointsSection;

                if (WatcherEndPointsSection != null && WatcherEndPointsSection.Instances != null)
                {
                    foreach (WatcherEndPointElement watcher in WatcherEndPointsSection.Instances)
                    {
                        Scheduler.Instance.AddWatcher(watcher.Name, watcher.Url);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteError("WatcherHelper Start Error", ex);
                throw ex;
            }
        }

        public static void Stop()
        {
            Logger.Write("WatcherHelper Stop");

            try
            {
                Scheduler.Instance.Stop();
            }
            catch (Exception ex)
            {
                Logger.Write("WatcherHelper Stop Error", ex);
            }
        }
    }
}
