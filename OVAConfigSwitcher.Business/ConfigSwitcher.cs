using System;
using System.Collections.Generic;
using System.Linq;
using OVAConfigSwitcher.Business.Contracts.Models;
using OVAConfigSwitcher.Business.Contracts.Interfaces;
using System.IO;

namespace OVAConfigSwitcher.Business
{
    public class ConfigSwitcher : IConfigSwitcher
    {
        private readonly string _rootPath;
        private readonly string _currentConfiguration;

        public ConfigSwitcher(string rootPath, string currentConfiguration)
        {
            _rootPath = rootPath ?? throw new ArgumentNullException(nameof(rootPath));
            _currentConfiguration = currentConfiguration ?? throw new ArgumentNullException(nameof(currentConfiguration));
        }

        public bool ApplyConfigurationFile(AgencyConfigurationFile agencyConfigurationFile)
        {
            if (!File.Exists(agencyConfigurationFile.FilePath))
            {
                throw new FileNotFoundException($"file does not exist: {nameof(agencyConfigurationFile.FilePath)}");
            }
            // validazione xml (incluso sopra)
            File.Copy(agencyConfigurationFile.FilePath, _currentConfiguration, true);

            return true;
        }

        public IEnumerable<AgencyConfigurationFile> GetAgencyConfigurationFiles(string environmentName)
        { 
            var configurationFiles = new List<AgencyConfigurationFile>();

            var configurationFileList = Directory.GetFiles(Path.Combine(_rootPath, environmentName), "*.*", SearchOption.TopDirectoryOnly)
                .Where(file => file.EndsWith(".xml") || file.EndsWith(".json"));

            foreach (var configurationFile in configurationFileList)
            {
                configurationFiles.Add(new AgencyConfigurationFile(configurationFile));
            }

            return configurationFiles;
        }

        public IEnumerable<EnvironmentType> GetEnvironments()
        {
            var environments = new List<EnvironmentType>();

            foreach (var environment in Directory.GetDirectories(_rootPath))
            {
                environments.Add(new EnvironmentType(environment));
            };

            return environments;
        }
    }
}
