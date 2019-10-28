using System;
using System.Collections.Generic;

namespace XPike.Configuration
{
    public class XPikeConfigBuilder
        : IXPikeConfigBuilder
    {
        private readonly IList<IConfigurationProvider> _providers;

        public XPikeConfigBuilder()
        {
            _providers = new List<IConfigurationProvider>();
        }

        public IConfigurationService ConfigurationService { get; private set; }

        public IXPikeConfigBuilder AddProvider(IConfigurationProvider instance)
        {
            if (ConfigurationService != null)
                throw new InvalidOperationException("Configuration Providers can only be added before the Configuration Service is constructed using Build().");

            _providers.Add(instance);

            return this;
        }

        public IConfigurationService Build()
        {
            if (ConfigurationService != null)
                throw new InvalidOperationException("The Configuration Service was already constructed from a previous call to Build().");

            return ConfigurationService = new ConfigurationService(_providers);
        }

        public IXPikeConfigBuilder ClearProviders()
        {
            if (ConfigurationService != null)
                throw new InvalidOperationException("Configuration Providers can only be removed before the Configuration Service is constructed using Build().");

            _providers.Clear();

            return this;
        }
    }
}