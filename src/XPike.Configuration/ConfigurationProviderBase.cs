using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace XPike.Configuration
{
    /// <summary>
    /// Abstract implementation of a Configuration Provider, to simplify the creation of new Providers.
    /// 
    /// In particular, adds proper exceptions when requesting "required" values using GetValueAsync(),
    /// as well as intrinsic datatype conversion and JSON deserialization support.
    /// 
    /// To create a new Provider, simply override this class and implement the GetValueOrDefaultAsync() method.
    /// </summary>
    public abstract class ConfigurationProviderBase
        : IConfigurationProvider
    {
        public abstract string GetValueOrDefault(string key, string defaultValue = null);

        public abstract Task<string> GetValueOrDefaultAsync(string key, string defaultValue = null);

        public string GetValue(string key)
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

        public virtual Task<string> GetValueAsync(string key)
        {
            try
            {
                return GetValueOrDefaultAsync(key) ??
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

        public T GetValue<T>(string key)
        {
            try
            {
                var value = GetValue(key);

                return ConvertValue<T>(key, value);
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

        public virtual T ConvertValue<T>(string key, string value)
        {
            try
            {
                if (typeof(T) == typeof(string))
                    return (T)(object)value;

                if (typeof(T) == typeof(byte))
                    return (T) (object) byte.Parse(value, NumberFormatInfo.InvariantInfo);

                if (typeof(T) == typeof(short))
                    return (T) (object) short.Parse(value, NumberFormatInfo.InvariantInfo);

                if (typeof(T) == typeof(ushort))
                    return (T) (object) ushort.Parse(value, NumberFormatInfo.InvariantInfo);

                if (typeof(T) == typeof(int))
                    return (T) (object) int.Parse(value, NumberFormatInfo.InvariantInfo);

                if (typeof(T) == typeof(uint))
                    return (T) (object) uint.Parse(value, NumberFormatInfo.InvariantInfo);

                if (typeof(T) == typeof(long))
                    return (T) (object) long.Parse(value, NumberFormatInfo.InvariantInfo);

                if (typeof(T) == typeof(ulong))
                    return (T) (object) ulong.Parse(value, NumberFormatInfo.InvariantInfo);

                if (typeof(T) == typeof(float))
                    return (T) (object) float.Parse(value, NumberFormatInfo.InvariantInfo);

                if (typeof(T) == typeof(double))
                    return (T) (object) double.Parse(value, NumberFormatInfo.InvariantInfo);

                if (typeof(T) == typeof(decimal))
                    return (T) (object) decimal.Parse(value, NumberFormatInfo.InvariantInfo);

                if (typeof(T) == typeof(bool))
                    return (T)(object)bool.Parse(value);

                if (typeof(T).IsEnum)
                    return (T)Enum.Parse(typeof(T), value);

                if (typeof(T) == typeof(TimeSpan))
                    return (T) (object) TimeSpan.Parse(value, DateTimeFormatInfo.InvariantInfo);

                if (typeof(T) == typeof(DateTime))
                    return (T) (object) DateTime.Parse(value, DateTimeFormatInfo.InvariantInfo);

                return JsonConvert.DeserializeObject<T>(value);
            }
            catch (Exception ex)
            {
                throw new InvalidConfigurationException(key, ex);
            }
        }

        public virtual async Task<T> GetValueAsync<T>(string key)
        {
            try
            {
                var value = await GetValueAsync(key).ConfigureAwait(false);

                return ConvertValue<T>(key, value);
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

        public T GetValueOrDefault<T>(string key, T defaultValue = default)
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

        public virtual Task<T> GetValueOrDefaultAsync<T>(string key, T defaultValue = default)
        {
            try
            {
                return GetValueAsync<T>(key);
            }
            catch (Exception)
            {
                return Task.FromResult(defaultValue);
            }
        }
    }
}