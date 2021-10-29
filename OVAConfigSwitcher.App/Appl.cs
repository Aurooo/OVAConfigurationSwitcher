using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RegistryReader;
using System;
using System.Collections.Generic;
using System.Linq;
using OVAConfigSwitcher.Business;
using System.IO;
using OVAConfigSwitcher.Business.Contracts.Models;

namespace OVAConfigSwitcher.App
{
    public class Appl
    {
        private readonly ILogger<Appl> _logger;
        private readonly AppSettings _appSettings;
        private ConfigSwitcher ConfigSwitcher { get; set; }

        public Appl(IOptions<AppSettings> appSettings, ILogger<Appl> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _appSettings = appSettings?.Value ?? throw new ArgumentNullException(nameof(appSettings));
            ConfigSwitcher = InitialiseConfigSwitcher();
        }

        public void Run(string[] args)
        {
            var environments = ConfigSwitcher.GetEnvironments().ToList();
            var usage = "\n\rUse -e to list available environments.\n\r" +
                "Use -f <envName> to list available configuration files in <envName>.\n\r" +
                "Use -a <envName> <configFile> to apply new configuration.\n\r";

            if (args.Length == 1 && args[0] == "-e")
            {
                environments.ForEach(env => Console.WriteLine(env.EnvironmentName));
                Console.ReadLine();

                return;
            }

            if (args.Length == 2 && args[0] == "-f" && IsValidEnvironment(args[1]))
            {
                ConfigSwitcher.GetAgencyConfigurationFiles(args[1]).ToList().ForEach(file => Console.WriteLine(file.AgencyFileName));
                Console.ReadLine();

                return;
            }

            if (args.Length == 3 && args[0] == "-a" && IsValidEnvironment(args[1]) && IsValidConfigurationFile(args[1], args[2]))
            {
                var agencyConfigurationFile = ConfigSwitcher.GetAgencyConfigurationFiles(args[1])
                    .Where(file => file.AgencyFileName == args[2])
                    .Select(file => file).Single();

                ConfigSwitcher.ApplyConfigurationFile(agencyConfigurationFile);

                _logger.LogInformation($"Now using: {args[1]} => {agencyConfigurationFile.AgencyFileName}. Configuration switch succesful");
                Console.ReadLine();

                return;
            }

            _logger.LogError(usage);
            Console.ReadLine();

            return;
        }
        private ConfigSwitcher InitialiseConfigSwitcher()
        {
            var currentConfig = new RegistryStream().Read(_appSettings.RegistryKey)
                .Where(element => element.Name == "ConfigFilePath1")
                .Select(element => element).FirstOrDefault().Value;

            return new ConfigSwitcher(_appSettings.RootDirectory, currentConfig);
        }

        private bool IsValidConfigurationFile(string environment, string agencyConfigurationFile)
        {
            return ConfigSwitcher.GetAgencyConfigurationFiles(environment).Where(file => file.AgencyFileName == agencyConfigurationFile)
                .Select(env => env.AgencyFileName).ToList().Count > 0;
        }

        private bool IsValidEnvironment(string environment)
        {
            return ConfigSwitcher.GetEnvironments().Where(env => env.EnvironmentName == environment)
                .Select(env => env.EnvironmentName).ToList().Count > 0;
        }
    }
}
