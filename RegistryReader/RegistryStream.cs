using System.Collections.Generic;
using RegistryReader.Interfaces;
using RegistryReader.Models;

namespace RegistryReader
{
    public class RegistryStream
    {
        public IEnumerable<RegistryValueElement> Read(string registryKey)
        {
            IRegistryReader registryReader = new RegistryFactory().GetBaseKey(registryKey);
            return registryReader.Read(registryKey);
        }
    }
}
