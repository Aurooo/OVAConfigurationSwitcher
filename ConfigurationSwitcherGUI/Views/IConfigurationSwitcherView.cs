using OVAConfigSwitcher.Business.Contracts.Models;
using System.Collections.Generic;

namespace ConfigurationSwitcherGUI.Views
{
    public interface IConfigurationSwitcherView
    {
        IEnumerable<string> Environments { set; }
        IEnumerable<AgencyConfigurationFile> Configurations { set; }
        public string SelectedEnvironment { get; set; }
        public string SelectedConfiguration { get; set; }
    }
}