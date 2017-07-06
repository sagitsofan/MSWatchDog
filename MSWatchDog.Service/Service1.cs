using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

using MSWatchDog.Service;

namespace MSWatchDog.Logic
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Manager.Start();
        }

        protected override void OnStop()
        {
            Manager.Stop();
        }
    }
}
