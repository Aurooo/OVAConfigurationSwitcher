﻿using Microsoft.Extensions.Logging;
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
        private ConfigSwitcher configSwitcher { get; set; }

        public Appl(IOptions<AppSettings> appSettings, ILogger<Appl> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _appSettings = appSettings?.Value ?? throw new ArgumentNullException(nameof(appSettings));
            configSwitcher = InitialiseConfigSwitcher();
        }

        public void Run(string[] args)
        {
            // menu interattivo
            var usage = "\n\rUse -e to list available environments.\n\r" +
                "Use -f <envName> to list available configuration files in <envName>.\n\r" +
                "Use -a <envName> <configFile> to apply new configuration.\n\r";

            EnvironmentType chosenEnvironment = null;
            AgencyConfigurationFile chosenConfigurationFile = null;

            if (args.Length == 0)
            {
                var exit = false;

                do
                {
                    int count = 0;
                    string inputString;

                    var environments = configSwitcher.GetEnvironments().ToList();

                    Console.WriteLine("Select environment:");

                    foreach (var env in environments)
                    {
                        Console.WriteLine($"{count++} - {env.EnvironmentName}");
                    }

                    inputString = Console.ReadLine();

                    if (Int32.TryParse(inputString, out count) && count >= 0 && count < environments.Count)
                    {
                        chosenEnvironment = environments[count];
                        exit = true;
                    }
                    else if (IsValidEnvironment(inputString))
                    {
                        chosenEnvironment = environments.Find(env => env.EnvironmentName == inputString);
                        exit = true;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Invalid environment\n");
                    }
                } while (!exit);

                do
                {
                    int count = 0;
                    string inputString;
                    var configFiles = configSwitcher.GetAgencyConfigurationFiles(chosenEnvironment.EnvironmentName).ToList();

                    Console.WriteLine("Select configuration file:");

                    foreach (var file in configFiles)
                    {
                        Console.WriteLine($"{count++} - {file.AgencyFileName}");
                    }

                    inputString = Console.ReadLine();

                    if (Int32.TryParse(inputString, out count) && count >= 0 && count < configFiles.Count)
                    {
                        chosenConfigurationFile = configFiles[count];
                        exit = false;
                    }
                    else if (IsValidConfigurationFile(chosenEnvironment.EnvironmentName, inputString))
                    {
                        chosenConfigurationFile = configFiles.Find(file => file.AgencyFileName == inputString);
                        exit = false;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Invalid configuration file\n");
                    }

                } while (exit);

                configSwitcher.ApplyConfigurationFile(chosenConfigurationFile);

                _logger.LogInformation($"Now using: {chosenEnvironment.EnvironmentName} => {chosenConfigurationFile.AgencyFileName}. Configuration switch succesful");
                Console.ReadLine();

            }
            else
            {
                if (args.Length == 1 && args[0] == "-h")
                {
                    Console.WriteLine(usage);
                    Console.ReadLine();

                    return;
                }

                if (args.Length == 1 && args[0] == "-e")
                {
                    configSwitcher.GetEnvironments().ToList().ForEach(env => Console.WriteLine(env.EnvironmentName));
                    Console.ReadLine();

                    return;
                }

                if (args.Length == 2 && args[0] == "-f" && IsValidEnvironment(args[1]))
                {
                    configSwitcher.GetAgencyConfigurationFiles(args[1]).ToList().ForEach(file => Console.WriteLine(file.AgencyFileName));
                    Console.ReadLine();

                    return;
                }

                if (args.Length == 3 && args[0] == "-a" && IsValidEnvironment(args[1]) && IsValidConfigurationFile(args[1], args[2]))
                {
                    var agencyConfigurationFile = configSwitcher.GetAgencyConfigurationFiles(args[1])
                        .Where(file => file.AgencyFileName == args[2])
                        .Select(file => file).Single();

                    configSwitcher.ApplyConfigurationFile(agencyConfigurationFile);

                    _logger.LogInformation($"Now using: {args[1]} => {agencyConfigurationFile.AgencyFileName}. Configuration switch succesful");
                    Console.ReadLine();

                    return;
                }

                _logger.LogError(usage);
                Console.ReadLine();

                return;
            }




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
            return configSwitcher.GetAgencyConfigurationFiles(environment).Where(file => file.AgencyFileName == agencyConfigurationFile)
                .Select(env => env.AgencyFileName).ToList().Count > 0;
        }

        private bool IsValidEnvironment(string environment)
        {
            return configSwitcher.GetEnvironments().Where(env => env.EnvironmentName == environment)
                .Select(env => env.EnvironmentName).ToList().Count > 0;
        }
    }
}
