using ConfigurationSwitcherGUI.Models;
using OVAConfigSwitcher.Business.Contracts.Models;
using System.Collections.Generic;

namespace ConfigurationSwitcherGUI.Presenter
{
    public interface IConfigurationSwitcherPresenter
    {
        void ApplyConfiguration(string environment, string agencyConfiguration);
        IEnumerable<AgencyConfigurationFile> GetConfigurations(string environmentName);
        void LoadView();
    }
}
