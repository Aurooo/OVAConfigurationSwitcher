using ConfigurationSwitcherGUI.View;
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
        public IConfigurationSwitcherView view { get; }

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
        public IEnumerable<EnvironmentDirectory> GetEnvironmentDirectories()
        {
            string name;
            var environments = new List<EnvironmentDirectory>();

            foreach (var environment in configSwitcher.GetEnvironments().ToList())
            {
                var files = new List<AgencyConfigurationFile>();

                name = environment.EnvironmentName;

                foreach (var file in configSwitcher.GetAgencyConfigurationFiles(environment.EnvironmentName).ToList())
                    files.Add(file);


                environments.Add(new EnvironmentDirectory(name, files));
            }

            return environments;
        }
        public bool Apply(string environment, string agencyConfiguration)
        {
            var agencyConfigurationFile = new AgencyConfigurationFile(Path.Combine(_appSettings.RootDirectory, environment, agencyConfiguration));
            bool applied = false;
            try
            {
                applied = configSwitcher.ApplyConfigurationFile(agencyConfigurationFile);

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

            return applied;
        }
    }
}
