using Microsoft.Win32;

namespace RegistryReader
{
    internal class UsersRegistryReader : BaseRegistryReader
    {
        public UsersRegistryReader() : base(Registry.Users)
        {
        }
    }
}