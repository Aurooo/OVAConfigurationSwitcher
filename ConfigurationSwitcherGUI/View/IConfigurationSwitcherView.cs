using ConfigurationSwitcherGUI.Models;
using OVAConfigSwitcher.Business.Contracts.Models;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ConfigurationSwitcherGUI.View
{
    public interface IConfigurationSwitcherView
    {
        void ShowError(string ErrorMessage);
    }
}