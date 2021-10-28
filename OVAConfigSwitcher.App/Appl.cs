using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RegistryReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OVAConfigSwitcher.Business;

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

        private static void ValidateArgs(string[] args)
        {
            if (!(args.Length == 2))
                throw new ArgumentException(nameof(args));

            if (!args[0].StartsWith("-") && "pre/prod/dev".Contains(args[0]))
                throw new ArgumentException(nameof(args));

            //finish validating for xml file (arg[1])
        }
    }
}
