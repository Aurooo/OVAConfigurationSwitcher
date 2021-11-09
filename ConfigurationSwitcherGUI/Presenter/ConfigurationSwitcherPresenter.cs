using ConfigurationSwitcherGUI.Views;
using ConfigurationSwitcherGUI.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OVAConfigSwitcher.Business;
using OVAConfigSwitcher.Business.Contracts.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ConfigurationSwitcherGUI.Presenter
{
    public class ConfigurationSwitcherPresenter : IConfigurationSwitcherPresenter
    {
        private readonly ILogger _logger;
        private readonly AppSettings _appSettings;
        private readonly ConfigSwitcher configSwitcher;
        private IConfigurationSwitcherView view { get; }

        public ConfigurationSwitcherPresenter(IConfigurationSwitcherView view, IOptions<AppSettings> appsettings, ILogger<ConfigurationSwitcherPresenter> logger)
        {
            this.view = view ?? throw new ArgumentNullException(nameof(view) + " is null");
            _appSettings = appsettings.Value ?? throw new ArgumentNullException(nameof(appsettings) + " is null");
            _logger = logger ?? throw new ArgumentNullException(nameof(_logger) + " is null");

            configSwitcher = new ConfigSwitcher(appsettings);
        }

        public IConfigurationSwitcherView ShowView()
        {
            return view;
        }
        public void LoadView()
        {
            view.Environments = configSwitcher.GetEnvironments().Select(environment => environment.EnvironmentName);
        }
        public IEnumerable<string> GetConfigurations(string environmentName)
        {
            return configSwitcher.GetAgencyConfigurationFiles(environmentName).Select(configuration => configuration.AgencyFileName);
        }
        public void ApplyConfiguration(string environment, string agencyConfiguration)
        {
            var agencyConfigurationFile = new AgencyConfigurationFile(Path.Combine(_appSettings.RootDirectory, environment, agencyConfiguration));
            
            try
            {
                configSwitcher.ApplyConfigurationFile(agencyConfigurationFile);

                MessageBox.Show("Configuration applied", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                _logger.LogInformation($"Now using: ({agencyConfigurationFile.EnvironmentName})" +
                    $"{agencyConfigurationFile.AgencyFileName}. Configuration switch successful");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                _logger.LogInformation($"Configuration switch error: {ex.Message} in " +
                    $"({agencyConfigurationFile.EnvironmentName})" +
                    $"{agencyConfigurationFile.AgencyFileName}. Configuration switch unsuccessful");
            }
        }
    }
}
