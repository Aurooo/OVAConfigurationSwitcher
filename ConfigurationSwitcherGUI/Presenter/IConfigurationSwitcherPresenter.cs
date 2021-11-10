using ConfigurationSwitcherGUI.Models;
using OVAConfigSwitcher.Business.Contracts.Models;
using System.Collections.Generic;

namespace ConfigurationSwitcherGUI.Presenter
{
    public interface IConfigurationSwitcherPresenter
    {
        void ApplyConfiguration();
        IEnumerable<AgencyConfigurationFile> GetConfigurations();
        void LoadView();
    }
}
