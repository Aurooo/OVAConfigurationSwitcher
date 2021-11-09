using ConfigurationSwitcherGUI.Models;
using OVAConfigSwitcher.Business.Contracts.Models;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ConfigurationSwitcherGUI.Views
{
    public interface IConfigurationSwitcherView
    {
        IEnumerable<string> Environments { set; }
        IEnumerable<string> Configurations { set; }
    }
}