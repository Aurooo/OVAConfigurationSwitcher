using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace OVAConfigSwitcher.Business
{
    [XmlRootAttribute("Configuration")]
    class ConfigurationDataTable
    {
        [XmlElement("RequestQueues")]
        public Queue[] Queues { get; set; }

        [XmlElement("DATABASENAME")]
        public string DatabaseName { get; set; }

        [XmlElement("SERVERNAME")]
        public string ServerName { get; set; }
        
        [XmlElement("Office365")]
        public string Office365 { get; set; }
        
        [XmlElement("Office365URL")]
        public string Office365URL { get; set; }
    }


    public class Queue
    {
        [XmlAttribute("Active")]
        public string Active { get; set; }

        [XmlAttribute("TimeoutInMilliseconds")]
        public string TimeoutInMilliseconds { get; set; }

        [XmlElement("Queue")]
        public string Value { get; set; }
    }
}
