using System;
using System.Collections.Generic;
using System.Linq;

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
        public override string GetValueOrDefault(string key, string defaultValue = null)
        {
            try
            {
                return System.Environment.GetEnvironmentVariable(key.Replace(":", "_")) ?? defaultValue;
            }
            catch (Exception)
            {
                // TODO: Do we want to dump this to console/debug/trace output?

                return defaultValue;
            }
        }

        public IDictionary<string, string> Load()
        {
            var env = System.Environment.GetEnvironmentVariables();
            var dict = new Dictionary<string, string>();

            foreach (var key in env.Keys.Cast<string>())
                dict[key] = (string) env[key];

            return dict;
        }
    }
}