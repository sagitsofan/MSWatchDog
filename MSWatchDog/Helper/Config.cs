using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSWatchDog.Helper
{
    public static class Config
    {
        public static int WatcherInterval
        {
            get
            {
                return int.Parse(ConfigurationManager.AppSettings["MSWatchDog.WatcherInterval"]);
            }
        }

        public static string SMTPServer
        {
            get
            {
                return ConfigurationManager.AppSettings["MSWatchDog.SMTPServer"];
            }
        }

        public static List<string> ToEmailsAlert
        {
            get
            {
                return ConfigurationManager.AppSettings["MSWatchDog.ToEmailsAlert"].Split(',').ToList();
            }
        }

        public static string FromEmailAlert
        {
            get
            {
                return ConfigurationManager.AppSettings["MSWatchDog.FromEmailAlert"];
            }
        }

        public static List<string> EndPoints
        {
            get
            {
                return ConfigurationManager.AppSettings["MSWatchDog.EndPoints"].Split(',').ToList();
            }
        }

        public static string SMTPUserName
        {
            get
            {
                return ConfigurationManager.AppSettings["MSWatchDog.SMTPUserName"];
            }
        }

        public static string SMTPPassword
        {
            get
            {
                return ConfigurationManager.AppSettings["MSWatchDog.SMTPPassword"];
            }
        }
    }
}
