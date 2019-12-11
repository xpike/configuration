using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XPike.Configuration.Environment
{
    /// <summary>
    /// Provides configuration values from environment variables.
    /// Uses System.Environment.GetEnvironmentVariable.
    /// Requested configuration keys will have all occurrences of ":" replaced with "_".
    /// In other words, to set the value for "MyLibrary::MySetting", you would set the "MyLibrary__MySetting" environment variable.
    /// </summary>
    public class EnvironmentConfigurationProvider
        : ConfigurationProviderBase,
          IEnvironmentConfigurationProvider
    {
        private static readonly Dictionary<string, string> _configKeys = new Dictionary<string, string>();

        static EnvironmentConfigurationProvider()
        {
            var env = System.Environment.GetEnvironmentVariables();

            foreach (var key in env.Keys.Cast<string>())
            {
                try
                {
                    _configKeys[key] = (string)env[key];
                }
                catch (Exception)
                {
                }
            }
        }

        public override string GetValueOrDefault(string key, string defaultValue = null)
        {
            try
            {
                return System.Environment.GetEnvironmentVariable(key.Replace(":", "_")) ??
                       defaultValue;
            }
            catch (Exception)
            {
                // TODO: Do we want to dump this to console/debug/trace output?

                return defaultValue;
            }
        }

        public override Task<string> GetValueOrDefaultAsync(string key, string defaultValue = null) =>
            Task.FromResult(GetValueOrDefault(key, defaultValue));

        public IDictionary<string, string> Load() =>
            _configKeys;

        public Task<IDictionary<string, string>> LoadAsync() =>
            Task.FromResult(Load());
    }
}