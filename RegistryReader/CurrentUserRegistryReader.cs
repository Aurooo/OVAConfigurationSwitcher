using Microsoft.Win32;

namespace RegistryReader
{
    internal class CurrentUserRegistryReader : BaseRegistryReader
    {
        public CurrentUserRegistryReader() : base(Registry.CurrentUser)
        {
        }
    }
}