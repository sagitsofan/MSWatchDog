using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using System.Configuration;

using MSWatchDog.Helper;
using MSWatchDog.Logic;

namespace MSWatchDog.AzureFunctions
{
    public static class MediaServicesWatchDog
    {
        [FunctionName("MediaServicesWatchDog")]
        public static void Run([TimerTrigger("0 */5 * * * *")]TimerInfo myTimer, TraceWriter log)
        {
            log.Info($"function started at: {DateTime.Now}");
            int index = 1;

            foreach (string endPoint in Config.EndPoints)
            {
                var vaild = new EndPoint(endPoint, index.ToString()).Process();
                log.Info($"job id: { index } result: { vaild }");
                index++;
            }

            log.Info($"function finished at: {DateTime.Now}");
        }
    }
}