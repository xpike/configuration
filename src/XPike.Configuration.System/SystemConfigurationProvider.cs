using System;
using System.Configuration;

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
        public override string GetValueOrDefault(string key, string defaultValue = null)
        {
            try
            {
                return ConfigurationManager.AppSettings[key] ?? defaultValue;
            }
            catch (Exception)
            {
                // TODO: Do we want to dump this "panic" state to console/debug output?

                return defaultValue;
            }
        }
    }
}