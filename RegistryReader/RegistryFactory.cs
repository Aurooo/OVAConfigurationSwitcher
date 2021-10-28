using System;
using System.Linq;
using RegistryReader.Interfaces;

namespace RegistryReader
{
    class RegistryFactory
    {
        const int INDEX_OF_BASE_KEY_NAME = 5;

        public IRegistryReader GetBaseKey(string registryKey)
        {
            if (!IsValidKey(registryKey))
                throw new ArgumentException(nameof(registryKey));

            string baseKey = registryKey.Split('\\')
                .Where(substring => substring.Contains("HKEY_"))
                .Select(substring => substring.Substring(INDEX_OF_BASE_KEY_NAME))
                .SingleOrDefault().ToString();

            switch(baseKey)
            {
                case "CURRENT_USER":
                    return new CurrentUserRegistryReader();

                case "CLASSES_ROOT": 
                    return new ClassesRootRegistryReader();

                case "LOCAL_MACHINE": 
                    return new LocalMachineRegistryReader();

                case "USERS": 
                    return new UsersRegistryReader();

                case "CURRENT_CONFIG":
                    return new CurrentConfigRegistryReader();

                default: throw new Exception("Base key not found");
            }
        }

        private bool IsValidKey(string registryKey)
        {
            return registryKey.Contains(@"\") && registryKey.Split('\\').Any(substring => substring.Contains("HKEY_"));
        }
    }
}
