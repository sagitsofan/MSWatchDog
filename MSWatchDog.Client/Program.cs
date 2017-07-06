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
            string url = "https://pluginprodma.streaming.mediaservices.windows.net/b39ff8a1-8a0f-4647-8e79-0174544d4fce/%D7%94%D7%95%D7%A4%D7%94%20-%20%D7%A2%D7%95%D7%9E%D7%A8%20%D7%90%D7%93%D7%9D%20-%2055651.ism";
            string name = "someuniquename";

            new EndPoint(url, name).Process();

            Notification.Notify(NotificationType.Email, "test");
        }
    }
}
