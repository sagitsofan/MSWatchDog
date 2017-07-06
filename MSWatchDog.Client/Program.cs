using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MSWatchDog.Logic;
using MSWatchDog.Helper;

namespace MSWatchDog.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = "http://amssamples.streaming.mediaservices.windows.net/91492735-c523-432b-ba01-faba6c2206a2/AzureMediaServicesPromo.ism";
            string name = "someuniquename";

            new EndPoint(url, name).Process();

            Notification.Notify(NotificationType.Email, "test");
        }
    }
}
