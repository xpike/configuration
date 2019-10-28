using System;
using System.Collections.Generic;
using System.Linq;

namespace XPike.Configuration.Memory
{
    /// <summary>
    /// Holds a collection of key-value pairs in memory to be used as configuration settings.
    /// Intended primarily for use in unit/integration tests.
    /// 
    /// Because this class requires a Dictionary to be passed into the constructor, in order to
    /// inject it into DI, it will need to be registered using:
    /// container.AddSingleton&lt;IConfigurationProvider&gt;(new MemoryConfigurationProvider(dictionary));
    /// </summary>
    public class MemoryConfigurationProvider
        : ConfigurationProviderBase,
          IMemoryConfigurationProvider
    {
        private readonly IDictionary<string, string> _configuration;

        public MemoryConfigurationProvider(IDictionary<string, string> configuration)
        {
            _configuration = configuration.ToDictionary(x => x.Key, x => x.Value);
        }

        public override string GetValueOrDefault(string key, string defaultValue = null)
        {
            try
            {
                return _configuration.TryGetValue(key, out var value) ?
                    value :
                    defaultValue;
            }
            catch (Exception)
            {
                // TODO: Do we want to dump this to console/debug/trace output?

                return defaultValue;
            }
        }

        public IDictionary<string, string> Load() =>
            _configuration;
    }
}