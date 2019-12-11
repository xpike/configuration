using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;

namespace XPike.Configuration.System
{
    /// <summary>
    /// Configuration Provider which uses System.Configuration.ConfigurationManager to
    /// retrieve values from either app.config or web.config.
    /// 
    /// Can be used in either .NET Framework or .NET Core.
    /// </summary>
    public class SystemConfigurationProvider
        : ConfigurationProviderBase,
          ISystemConfigurationProvider
    {
        private static readonly Dictionary<string,string> _configKeys = new Dictionary<string, string>();

        static SystemConfigurationProvider()
        {
            var settings = ConfigurationManager.AppSettings;
            
            foreach (var item in settings.AllKeys)
                _configKeys[item] = settings[item];
        }

        public override string GetValueOrDefault(string key, string defaultValue = null)
        {
            try
            {
                return _configKeys.TryGetValue(key, out var value) ? value ?? defaultValue : defaultValue;
            }
            catch (Exception)
            {
                // TODO: Do we want to dump this "panic" state to console/debug output?

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