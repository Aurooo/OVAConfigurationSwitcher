using Microsoft.Win32;

namespace RegistryReader
{
    class ClassesRootRegistryReader : BaseRegistryReader
    {
        public ClassesRootRegistryReader() : base(Registry.ClassesRoot)
        {
        }
    }
}