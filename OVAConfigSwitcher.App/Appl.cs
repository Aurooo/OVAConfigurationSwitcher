using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using OVAConfigSwitcher.Business;
using OVAConfigSwitcher.Business.Contracts.Models;
using OVAConfigSwitcher.Business.Contracts.Exceptions;
using System.Collections.Generic;
using System.IO;

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
            configSwitcher = new ConfigSwitcher(appSettings);
        }

        public void Run(string[] args)
        {
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
                    string inputString;
                    var environments = configSwitcher.GetEnvironments().ToList();

                    Console.WriteLine(":> Select environment:\n");
                    Print(environments);

                    Console.Write(":>");
                    inputString = Console.ReadLine();

                    if (Int32.TryParse(inputString, out int index) && index >= 0 && index < environments.Count)
                    {
                        chosenEnvironment = environments[index];
                        exit = true;
                    }
                    else if (IsValidEnvironment(inputString))
                    {
                        chosenEnvironment = environments.Find(env => env.EnvironmentName == inputString);
                        exit = true;
                    }
                    else
                    {
                        Console.WriteLine("\n:> Invalid environment\n");
                    }
                } while (!exit);

                do
                {
                    string inputString;

                    var configFiles = configSwitcher.GetAgencyConfigurationFiles(chosenEnvironment.EnvironmentName).ToList();

                    Console.WriteLine($"\n{chosenEnvironment.EnvironmentName} ->");
                    Console.WriteLine("  0 - ..");

                    Print(configFiles);

                    Console.Write(":>");
                    inputString = Console.ReadLine();

                    if (inputString == ".." || (Int32.TryParse(inputString, out int index) && index == 0))
                    {
                        GoBack();
                    }
                    if (Int32.TryParse(inputString, out index) && index >= 0 && index <= configFiles.Count)
                    {
                        chosenConfigurationFile = configFiles[index - 1];
                        exit = false;
                    }
                    else if (IsValidConfigurationFile(chosenEnvironment.EnvironmentName, inputString))
                    {
                        chosenConfigurationFile = configFiles.Find(file => file.AgencyFileName == inputString);
                        exit = false;
                    }
                    else
                    {
                        Console.WriteLine("\n:> Invalid input: choose the number or the name of a file in the list\n");
                    }

                } while (exit);

                var applied = Apply(chosenConfigurationFile);

                if (applied)
                {
                    Log(chosenConfigurationFile);
                }
                else
                {
                    GoBack();
                }
            }
            else
            {
                if (args.Length == 1 && args[0] == "-h")
                {
                    Console.WriteLine(usage);
                }
                else if (args.Length == 1 && args[0] == "-e")
                {
                    configSwitcher.GetEnvironments().ToList().ForEach(env => Console.WriteLine(env.EnvironmentName));
                }
                else if (args.Length == 2 && args[0] == "-f")
                {
                    if (!IsValidEnvironment(args[1]))
                    {
                        Console.WriteLine($"\'{args[1]}\' is not a valid environment");
                    }
                    else
                    {
                        configSwitcher.GetAgencyConfigurationFiles(args[1]).ToList().ForEach(file => Console.WriteLine(file.AgencyFileName));
                    }

                }
                else if (args.Length == 3 && args[0] == "-a")
                {
                    if (!IsValidEnvironment(args[1]))
                    {
                        Console.WriteLine($"\'{args[1]}\' is not a valid environment");
                    }
                    else if (!IsValidConfigurationFile(args[1], args[2]))
                    {
                        Console.WriteLine($"\'{args[2]}\' not found in {args[1]}");
                    }
                    else
                    {
                        var agencyConfigurationFile = configSwitcher.GetAgencyConfigurationFiles(args[1])
                        .Where(file => file.AgencyFileName == args[2])
                        .Select(file => file).Single();

                        bool applied = Apply(agencyConfigurationFile);

                        if (applied)
                        {
                            Log(agencyConfigurationFile);
                        }
                    }

                }
                else
                {
                    Console.WriteLine("Invalid arguments. For help type: ConsoleApp.exe -h");
                }
            }
        }

        private void Log(AgencyConfigurationFile agencyConfigurationFile)
        {
            Console.WriteLine($":> Now using: {agencyConfigurationFile.EnvironmentName} => " +
            $"{agencyConfigurationFile.AgencyFileName}. Configuration switch successful");

            _logger.LogInformation($"Now using: {agencyConfigurationFile.EnvironmentName} => " +
                $"{agencyConfigurationFile.AgencyFileName}. Configuration switch successful");
        }
        private void Print(List<AgencyConfigurationFile> configFiles)
        {
            int count = 1;

            foreach (var file in configFiles)
            {
                Console.WriteLine($"  {count++} - {file.AgencyFileName}");
            }


            Console.WriteLine();
        }
        private void Print(List<EnvironmentType> environments)
        {
            int count = 0;

            foreach (var env in environments)
            {
                Console.WriteLine($"{count++} - {env.EnvironmentName}");
            }

            Console.WriteLine();
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
        private void GoBack()
        {
            string[] a = new string[0];
            Console.WriteLine();
            Run(a);
        }
        private bool Apply(AgencyConfigurationFile agencyConfigurationFile)
        {
            bool applied;

            try
            {
                configSwitcher.ApplyConfigurationFile(agencyConfigurationFile);
                applied = true;
            }
            catch (InvalidConfigurationException ex)
            {
                Console.WriteLine(ex.Message);
                applied = false;
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine(":> Configuration file not found");
                applied = false;
            }

            return applied;
        }
    }
}
