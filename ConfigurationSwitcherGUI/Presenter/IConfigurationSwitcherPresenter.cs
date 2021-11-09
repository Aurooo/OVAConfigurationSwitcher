using ConfigurationSwitcherGUI.Models;
using System.Collections.Generic;

namespace ConfigurationSwitcherGUI.Presenter
{
    public interface IConfigurationSwitcherPresenter
    {
        void ApplyConfiguration(string environment, string agencyConfiguration);
        IEnumerable<string> GetConfigurations(string environmentName);
        void LoadView();
    }
}
