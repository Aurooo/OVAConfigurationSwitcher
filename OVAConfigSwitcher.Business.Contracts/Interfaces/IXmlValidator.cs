using OVAConfigSwitcher.Business.Contracts.Models;

namespace OVAConfigSwitcher.Business.Contracts.Interfaces
{
    public interface IXmlValidator
    {
        bool Validate(AgencyConfigurationFile agencyConfigurationFile);
    }
}