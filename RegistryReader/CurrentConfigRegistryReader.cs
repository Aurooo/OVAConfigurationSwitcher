using Microsoft.Win32;

namespace RegistryReader
{
    internal class CurrentConfigRegistryReader : BaseRegistryReader
    {
        public CurrentConfigRegistryReader() : base(Registry.CurrentConfig)
        {
        }
    }
}