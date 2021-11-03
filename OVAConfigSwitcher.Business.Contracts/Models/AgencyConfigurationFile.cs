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

            FilePath = filePath;
            AgencyFileName = filePath.Replace(directoryPath + Path.DirectorySeparatorChar, "");
            EnvironmentName = directoryPath.Replace(Path.GetDirectoryName(directoryPath) + Path.DirectorySeparatorChar, "");
        }
    }
}
