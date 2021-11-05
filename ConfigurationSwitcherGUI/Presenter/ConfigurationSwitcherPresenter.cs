using ConfigurationSwitcherGUI.View;
using ConfigurationSwitcherGUI.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OVAConfigSwitcher.Business;
using OVAConfigSwitcher.Business.Contracts.Models;
using RegistryReader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            this.view = view;
            _appSettings = appsettings.Value;
            _logger = logger;
            configSwitcher = new ConfigSwitcher(appsettings);
        }

        public IConfigurationSwitcherView ShowView()
        {
            PopulateView();
            return view;
        }
        public void PopulateView()
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

            view.Populate(environments);
        }
        public void Apply(string environment, string agencyConfiguration)
        {
            var agencyConfigurationFile = new AgencyConfigurationFile(Path.Combine(_appSettings.RootDirectory, environment, agencyConfiguration));
            try
            {
                configSwitcher.ApplyConfigurationFile(agencyConfigurationFile);
            }
            catch (Exception ex)
            {
                view.ShowError(ex.Message);
            }
        }
    }
}
