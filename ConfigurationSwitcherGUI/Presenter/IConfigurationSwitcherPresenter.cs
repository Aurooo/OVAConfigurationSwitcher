using ConfigurationSwitcherGUI.Models;
using System.Collections.Generic;

namespace ConfigurationSwitcherGUI.Presenter
{
    public interface IConfigurationSwitcherPresenter
    {
        bool Apply(string environment, string agencyConfiguration);
        IEnumerable<EnvironmentDirectory> GetEnvironmentDirectories();
    }
}
