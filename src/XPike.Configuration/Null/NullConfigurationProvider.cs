using System.Collections.Generic;

namespace XPike.Configuration.Null
{
    /// <summary>
    /// A Configuration Provider that yields no values.
    /// Primarily intended for use in unit/integration tests.
    /// </summary>
    public class NullConfigurationProvider
        : ConfigurationProviderBase,
          INullConfigurationProvider
    {
        public override string GetValueOrDefault(string key, string defaultValue = null) =>
            defaultValue;

        public IDictionary<string, string> Load() =>
            new Dictionary<string, string>();
    }
}