using Microsoft.Win32;

namespace RegistryReader.Models
{
    public class RegistryValueElement
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public RegistryValueKind Type { get; set; }
    }
}
