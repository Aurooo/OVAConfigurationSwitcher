using System;
using System.Collections.Generic;
using RegistryReader.Interfaces;
using RegistryReader.Models;
using Microsoft.Win32;

namespace RegistryReader
{
    abstract class BaseRegistryReader : IRegistryReader
    {
        protected RegistryKey BaseKey { get; set; }

        protected BaseRegistryReader(RegistryKey baseKey)
        {
            BaseKey = baseKey ?? throw new ArgumentNullException(nameof(baseKey));
        }


        public IEnumerable<RegistryValueElement> Read(string registryKey)
        {
            var keyElements = new List<RegistryValueElement>();
            var baseKeyIndexInRegistryKey = registryKey.IndexOf(BaseKey.Name);
            var subKey = registryKey.Substring(baseKeyIndexInRegistryKey + BaseKey.Name.Length + 1);

            using(RegistryKey key = BaseKey.OpenSubKey(subKey))
            {
                string[] names;

                try
                {
                    names = key.GetValueNames();
                }
                catch (Exception)
                {
                    throw new Exception("Not an existing registry key");
                }

                if(names.Length == 0)
                    throw new Exception("Registry key is empty");

                foreach(var name in names)
                {
                    var type = key.GetValueKind(name);

                    if (type == RegistryValueKind.Binary)
                        throw new Exception("Binary types not supported");

                    keyElements.Add(new RegistryValueElement
                    {
                        Name = name,
                        Value = key.GetValue(name).ToString(),
                        Type = type
                    });
                }
            }

            return keyElements;
        }
    }
}
