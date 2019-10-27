using Newtonsoft.Json;
using System;

namespace XPike.Configuration
{
    /// <summary>
    /// Abstract implementation of a Configuration Provider, to simplify the creation of new Providers.
    /// 
    /// In particular, adds proper exceptions when requesting "required" values using GetValue(),
    /// as well as intrinsic datatype conversion and JSON deserialization support.
    /// 
    /// To create a new Provider, simply override this class and implement the GetValueOrDefault() method.
    /// </summary>
    public abstract class ConfigurationProviderBase
        : IConfigurationProvider
    {
        public abstract string GetValueOrDefault(string key, string defaultValue = null);

        public virtual string GetValue(string key)
        {
            try
            {
                return GetValueOrDefault(key) ??
                    throw new InvalidConfigurationException(key);
            }
            catch (InvalidConfigurationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new InvalidConfigurationException(key, ex);
            }
        }

        public virtual T GetValue<T>(string key)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(GetValue(key));
            }
            catch (InvalidConfigurationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new InvalidConfigurationException(key, ex);
            }
        }

        public virtual T GetValueOrDefault<T>(string key, T defaultValue = default)
        {
            try
            {
                return GetValue<T>(key);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }
    }
}