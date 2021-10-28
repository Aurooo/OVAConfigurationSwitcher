using RegistryReader.Models;
using System.Collections.Generic;

namespace RegistryReader.Interfaces
{
    public interface IRegistryReader
    {
        IEnumerable<RegistryValueElement> Read(string registryKey);
    }
}
