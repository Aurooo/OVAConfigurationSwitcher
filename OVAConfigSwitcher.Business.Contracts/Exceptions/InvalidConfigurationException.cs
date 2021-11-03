using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OVAConfigSwitcher.Business.Contracts.Exceptions
{
    public class InvalidConfigurationException : Exception
    {
        public InvalidConfigurationException() : base() { }
        public InvalidConfigurationException(string message) : base("Invalid configuration: " + message) { }
        public InvalidConfigurationException(string message, Exception inner) : base(message, inner) { }
        protected InvalidConfigurationException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }

    }
}
