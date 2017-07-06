using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Threading.Tasks;

namespace MSWatchDog.Logic
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }

        private void RetrieveServiceName()
        {
            var serviceName = Context.Parameters["servicename"];
            if (!string.IsNullOrEmpty(serviceName))
            {
                this.serviceInstaller1.ServiceName = serviceName;
                this.serviceInstaller1.DisplayName = serviceName;
            }
        }

        public override void Install(IDictionary stateSaver)
        {
            RetrieveServiceName();
            base.Install(stateSaver);
        }

        public override void Uninstall(IDictionary savedState)
        {
            RetrieveServiceName();
            base.Uninstall(savedState);
        }

    }
}
