using ConfigurationSwitcherGUI.Models;
using OVAConfigSwitcher.Business.Contracts.Models;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ConfigurationSwitcherGUI.View
{
    public interface IConfigurationSwitcherView
    {
        void ShowView();
        void ShowError(string ErrorMessage);
        void Populate(IEnumerable<EnvironmentDirectory> environments);
    }
}