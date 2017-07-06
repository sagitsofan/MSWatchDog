using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSWatchDog.Helper;

namespace MSWatchDog.Logic
{
    public class WatcherEndPointsSection : ConfigurationSection
    {
        [ConfigurationProperty("", IsRequired = true, IsDefaultCollection = true)]
        public WatcherEndPointInstanceCollection Instances
        {
            get { return (WatcherEndPointInstanceCollection)this[""]; }
            set { this[""] = value; }
        }
    }

    public class WatcherEndPointInstanceCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new WatcherEndPointElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            //set to whatever Element Property you want to use for a key
            return ((WatcherEndPointElement)element).Name;
        }
    }

    public class WatcherEndPointElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get
            {
                return (string)this["name"];
            }
            set
            {
                this["name"] = value;
            }
        }

        [ConfigurationProperty("url", IsRequired = true)]
        public string Url
        {
            get
            {
                return (string)this["url"];
            }
            set
            {
                this["url"] = value;
            }
        }
    }
}
