using System;
using System.Collections.Generic;
using System.Linq;
using OVAConfigSwitcher.Business.Contracts.Models;
using OVAConfigSwitcher.Business.Contracts.Interfaces;
using System.IO;
using Microsoft.Extensions.Options;
using RegistryReader;

namespace OVAConfigSwitcher.Business
{
    public class ConfigSwitcher : IConfigSwitcher
    {
        private readonly string _rootPath;
        private readonly string _registryKey;

        public ConfigSwitcher(IOptions<AppSettings> appSettings)
        {
            _rootPath = appSettings.Value.RootDirectory ?? throw new ArgumentNullException(nameof(appSettings.Value.RootDirectory));
            _registryKey = appSettings.Value.RegistryKey ?? throw new ArgumentNullException(nameof(appSettings.Value.RegistryKey));
        }

        public bool ApplyConfigurationFile(AgencyConfigurationFile agencyConfigurationFile)
        {
            var validator = new ConfigurationValidator();
            validator.Validate(agencyConfigurationFile);

            var currentConfiguration = GetCurrentConfiguration();

            if (!Directory.Exists(Path.GetDirectoryName(currentConfiguration)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(currentConfiguration));
            }

            File.Copy(agencyConfigurationFile.FilePath, currentConfiguration, true);

            return true;
        }

        private string GetCurrentConfiguration()
        {
            return new RegistryStream().Read(_registryKey)
                .Where(element => element.Name == "ConfigFilePath")
                .Select(element => element).FirstOrDefault().Value;
        }

        public IEnumerable<AgencyConfigurationFile> GetAgencyConfigurationFiles(string environmentName)
        {
            var configurationFiles = new List<AgencyConfigurationFile>();

            var configurationFileList = Directory.GetFiles(Path.Combine(_rootPath, environmentName), "*.*", SearchOption.TopDirectoryOnly)
                .Where(file => file.EndsWith(".xml") || file.EndsWith(".XML"));

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
