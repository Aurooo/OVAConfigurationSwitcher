using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RegistryReader;
using System;
using System.Collections.Generic;
using System.Linq;
using OVAConfigSwitcher.Business;
using System.IO;

namespace OVAConfigSwitcher.App
{
    public class Appl
    {
        private readonly ILogger<Appl> _logger;
        private readonly AppSettings _appSettings;

        public Appl(IOptions<AppSettings> appSettings, ILogger<Appl> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _appSettings = appSettings?.Value ?? throw new ArgumentNullException(nameof(appSettings));
        }

        public void Run(string[] args)
        {
            ValidateArgs(args);

            var currentConfig = new RegistryStream().Read(_appSettings.RegistryKey)
                .Where(element => element.Name == "ConfigFilePath1")
                .Select(element => element).FirstOrDefault().Value;

            var configSwitcher = new ConfigSwitcher(_appSettings.RootDirectory, currentConfig);

            // complete switch in configuration
        }

        private void ValidateArgs(string[] args)
        {
            if (!(args.Length == 2))
                throw new ArgumentException("Provide 2 arguments: env configfile");

            //get envs from root
            if (!args[0].StartsWith("-") && "pre/prod/test".Contains(args[0]))
                throw new ArgumentException("Not a valid environment");

            //finish validating for xml file (arg[1])
            if (!args[1].Contains(".xml") && !args[1].Contains(".json"))
                throw new ArgumentException("Not an xml or json file");

            if (!ConfigurationExists(_appSettings, args))
                throw new FileNotFoundException($"{args[1]} not found in {args[0]}");
        }

        private bool ConfigurationExists(AppSettings appSettings, string[] args)
        {
            string environment = appSettings.RootDirectory;

            switch (args[0])
            {
                case "-test":
                    environment = Path.Combine(environment, "test");
                    break;
                case "-pre":
                    environment = Path.Combine(environment, "pre_prod");
                    break;
                case "-prod":
                    environment = Path.Combine(environment, "prod");
                    break;
            }

            return File.Exists(Path.Combine(environment, args[1]));

        }
    }
}
