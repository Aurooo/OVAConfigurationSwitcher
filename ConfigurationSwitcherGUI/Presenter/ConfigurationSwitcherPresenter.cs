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
        
        private readonly IConfigurationSwitcherView _view;
        private readonly AppSettings _appSettings;
        private readonly ILogger _logger;
        private readonly ConfigSwitcher _configSwitcher;
        private readonly IEnumerable<EnvironmentDirectory> _environmentDirectories;

        public ConfigurationSwitcherPresenter(IConfigurationSwitcherView view, IOptions<AppSettings> appsettings, ILogger<ConfigurationSwitcherPresenter> logger)
        {
            this._view = view ?? throw new ArgumentNullException(nameof(view) + " is null");
            _appSettings = appsettings.Value ?? throw new ArgumentNullException(nameof(appsettings) + " is null");
            _logger = logger ?? throw new ArgumentNullException(nameof(_logger) + " is null");
            _configSwitcher = new ConfigSwitcher(appsettings);
            _environmentDirectories = new EnvironmentDirectoriesFactory().GetEnvironmentDirectories(_configSwitcher);
        }

        public IConfigurationSwitcherView ShowView()
        {
            return _view;
        }
        public void LoadView()
        {
            _view.Environments = _environmentDirectories.Select(environment => environment.Name);
        }
        public IEnumerable<AgencyConfigurationFile> GetConfigurations()
        {
            return _environmentDirectories.Where(directory => directory.Name == _view.SelectedEnvironment).Single().Files;
        }
        public void ApplyConfiguration()
        {
            var agencyConfigurationFile = new AgencyConfigurationFile(Path.Combine(_appSettings.RootDirectory, _view.SelectedEnvironment, _view.SelectedConfiguration));
            
            try
            {
                _configSwitcher.ApplyConfigurationFile(agencyConfigurationFile);

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
