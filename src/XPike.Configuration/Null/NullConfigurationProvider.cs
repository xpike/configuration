using System.Collections.Generic;
using System.Threading.Tasks;

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

        public override Task<string> GetValueOrDefaultAsync(string key, string defaultValue = null) =>
            Task.FromResult(GetValueOrDefault(key, defaultValue));

        public IDictionary<string, string> Load() =>
            new Dictionary<string, string>();

        public Task<IDictionary<string, string>> LoadAsync() =>
            Task.FromResult(Load());
    }
}