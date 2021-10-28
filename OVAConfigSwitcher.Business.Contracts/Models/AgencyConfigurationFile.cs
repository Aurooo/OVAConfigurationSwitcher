using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace OVAConfigSwitcher.Business.Contracts.Models
{
    public class AgencyConfigurationFile
    {
        public string AgencyFileName { get; set; }
        public string FilePath { get; set; }
        public string EnvironmentName { get; set; }

        public AgencyConfigurationFile(string filePath)
        {
            var directoryPath = Path.GetDirectoryName(filePath);

            AgencyFileName = filePath.Replace(directoryPath + Path.DirectorySeparatorChar, "");
            FilePath = filePath;
            EnvironmentName = directoryPath.Replace(Path.GetDirectoryName(directoryPath), "");
        }
    }
}
