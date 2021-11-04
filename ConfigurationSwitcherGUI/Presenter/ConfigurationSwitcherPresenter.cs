using ConfigurationSwitcherGUI.View;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OVAConfigSwitcher.Business;
using OVAConfigSwitcher.Business.Contracts.Models;
using RegistryReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConfigurationSwitcherGUI.Presenter
{
    class ConfigurationSwitcherPresenter
    {
        private readonly ILogger _logger;
        private readonly AppSettings _appSettings;
        private readonly ConfigSwitcher configSwitcher;
        private IConfigurationSwitcherView view { get; set; }

        public ConfigurationSwitcherPresenter(IConfigurationSwitcherView view, IOptions<AppSettings> appsettings, ILogger<ConfigurationSwitcherPresenter> logger)
        {
            _appSettings = appsettings.Value;
            _logger = logger;
            configSwitcher = InitializeConfigSwitcher();
            this.view = view;
        }

        private ConfigSwitcher InitializeConfigSwitcher()
        {
            var currentConfig = new RegistryStream().Read(_appSettings.RegistryKey)
                .Where(element => element.Name == "ConfigFilePath1")
                .Select(element => element).FirstOrDefault().Value;

            return new ConfigSwitcher(_appSettings.RootDirectory, currentConfig);
        }

        
    }
}
