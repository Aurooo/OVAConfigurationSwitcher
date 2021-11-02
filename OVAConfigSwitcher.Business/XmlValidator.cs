using OVAConfigSwitcher.Business.Contracts.Models;
using OVAConfigSwitcher.Business.Contracts.Interfaces;
using OVAConfigSwitcher.Business.Contracts.Exceptions;
using System.Xml;
using System.Xml.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;

namespace OVAConfigSwitcher.Business
{
    class XmlValidator : IXmlValidator
    {
        public bool Validate(AgencyConfigurationFile agencyConfigurationFile)
        {
            if (!File.Exists(agencyConfigurationFile.FilePath))
            {
                throw new FileNotFoundException($"file does not exist: {nameof(agencyConfigurationFile.FilePath)}");
            }

            XmlReader reader = XmlReader.Create(agencyConfigurationFile.FilePath);

            XmlSerializer serializer = new XmlSerializer(typeof(ConfigurationDataTable));
            ConfigurationDataTable configuration = serializer.Deserialize(reader) as ConfigurationDataTable;

            XmlSchemaSet schemaSet = new XmlSchemaSet();
            schemaSet.Add("", "data.xsd");

            XmlSchemaValidator validator = new XmlSchemaValidator(null, schemaSet, null, XmlSchemaValidationFlags.None);
            validator.Initialize();

            try
            {
                validator.ValidateElement("Configuration", "", null);
            }
            catch (XmlSchemaValidationException)
            {
                throw new InvalidConfigurationException();
            }

            return true;
        }
    }
}
