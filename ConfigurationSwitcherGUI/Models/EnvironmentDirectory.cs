using OVAConfigSwitcher.Business.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationSwitcherGUI.Models
{
    public class EnvironmentDirectory
    {
        public string Name { get; set; }
        public IEnumerable<AgencyConfigurationFile> Files { get; set; }

        public EnvironmentDirectory(string name, IEnumerable<AgencyConfigurationFile> files)
        {
            Name = name;
            Files = files;
        }
    }
}
