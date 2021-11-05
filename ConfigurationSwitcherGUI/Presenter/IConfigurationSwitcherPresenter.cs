using OVAConfigSwitcher.Business.Contracts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationSwitcherGUI.Presenter
{
    public interface IConfigurationSwitcherPresenter
    {
        void Apply(string environment, string agencyConfiguration);
    }
}
