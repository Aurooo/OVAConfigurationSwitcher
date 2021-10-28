using Microsoft.Win32;

namespace RegistryReader
{
    internal class LocalMachineRegistryReader : BaseRegistryReader
    {
        public LocalMachineRegistryReader() : base(Registry.LocalMachine)
        {
        }
    }
}