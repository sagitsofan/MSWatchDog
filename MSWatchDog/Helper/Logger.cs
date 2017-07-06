using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

using NLog;

namespace MSWatchDog.Helper
{
    public static class Logger
    {
        public static NLog.Logger logger = LogManager.GetLogger("SecurityProvider");

        public static void Write(string Message)
        {
            logger.Trace(Message);
            Console.WriteLine(Message);
        }

        public static void Write(string Message, params object[] args)
        {
            string strParams = string.Join(",", args.Select(i => i != null ? i.ToString() : "{NULL}"));

            if (strParams.Length > 1000)
            {
                strParams = strParams.Substring(0, 1000);
                strParams += "...";
            }

            logger.Trace(Message + ": " + strParams);
            Console.WriteLine(Message);
        }

        public static void WriteError(string Message, Exception ex)
        {
            logger.Error(Message, ex);
            Console.WriteLine(Message);
        }
    }
}