using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
