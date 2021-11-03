using OVAConfigSwitcher.Business.Contracts.Models;
using OVAConfigSwitcher.Business.Contracts.Exceptions;
using System.Xml.Schema;
using System.IO;
using System.Xml.Linq;
using System;

namespace OVAConfigSwitcher.Business
{
    class XmlValidator
    {
        public bool Validate(AgencyConfigurationFile agencyConfigurationFile)
        {

            if (!File.Exists(agencyConfigurationFile.FilePath))
            {
                throw new FileNotFoundException($"file does not exist: {nameof(agencyConfigurationFile.FilePath)}");
            }

            XmlSchemaSet configurationSchema = new XmlSchemaSet();
            configurationSchema.Add("", "Configuration.xsd");

            XDocument newConfiguration = XDocument.Load(agencyConfigurationFile.FilePath);

            if (!string.IsNullOrWhiteSpace(newConfiguration.Root.GetDefaultNamespace().ToString()))
                throw new InvalidConfigurationException("Namespace is not empty");

            newConfiguration.Validate(configurationSchema, (s, e) => throw new InvalidConfigurationException(e.Message));

            return true;
        }
    }
}
