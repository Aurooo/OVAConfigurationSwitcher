using ConfigurationSwitcherGUI.Models;
using OVAConfigSwitcher.Business;
using System.Collections.Generic;
using System.Linq;

namespace ConfigurationSwitcherGUI
{
    public class EnvironmentDirectoriesFactory
    {
        public IEnumerable<EnvironmentDirectory> GetEnvironmentDirectories(ConfigSwitcher configSwitcher)
        {
            var directories = new List<EnvironmentDirectory>();

            var environments = configSwitcher.GetEnvironments().Select(environment => environment.EnvironmentName);

            foreach (var environment in environments)
            {
                var files = configSwitcher.GetAgencyConfigurationFiles(environment);
                directories.Add(new EnvironmentDirectory(environment, files));
            }

            return directories;
        }
    }
}
