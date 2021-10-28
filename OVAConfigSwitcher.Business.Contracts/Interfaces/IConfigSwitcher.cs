using System.Collections.Generic;
using OVAConfigSwitcher.Business.Contracts.Models;

namespace OVAConfigSwitcher.Business.Contracts.Interfaces
{
    public interface IConfigSwitcher
    {
        IEnumerable<EnvironmentType> GetEnvironments();
        IEnumerable<AgencyConfigurationFile> GetAgencyConfigurationFiles(string environmentName);
        bool ApplyConfigurationFile(AgencyConfigurationFile agencyConfigurationFile);
    }
}
