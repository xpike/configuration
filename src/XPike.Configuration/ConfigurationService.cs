using System.Collections.Generic;

namespace XPike.Configuration
{
    public class ConfigurationService
        : ConfigurationServiceBase<IConfigurationProvider>
    {
        public ConfigurationService(IEnumerable<IConfigurationProvider> providers)
            : base(providers)
        {
        }
    }
}