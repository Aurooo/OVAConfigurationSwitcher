using System.IO;

namespace OVAConfigSwitcher.Business.Contracts.Models
{
    public class EnvironmentType
    {
        public string EnvironmentName { get; set; }
        public string EnvironmentPath { get; set; }

        public EnvironmentType(string environmentPath)
        {
            EnvironmentPath = environmentPath;
            EnvironmentName = environmentPath.Replace(Path.GetDirectoryName(environmentPath) + Path.DirectorySeparatorChar, "");
        }
    }
}
