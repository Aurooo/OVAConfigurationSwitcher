using OVAConfigSwitcher.Business.Contracts.Models;
using System.Collections.Generic;

namespace ConfigurationSwitcherGUI.Views
{
    public interface IConfigurationSwitcherView
    {
        IEnumerable<string> Environments { set; }
        IEnumerable<AgencyConfigurationFile> Configurations { set; }
    }
}