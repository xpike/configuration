﻿using Newtonsoft.Json;
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
                var value = GetValue(key);

                if (typeof(T) == typeof(string))
                    return (T) (object) value;

                if (typeof(T) == typeof(byte))
                    return (T)(object)byte.Parse(value);

                if (typeof(T) == typeof(short))
                    return (T)(object)short.Parse(value);

                if (typeof(T) == typeof(ushort))
                    return (T)(object)ushort.Parse(value);

                if (typeof(T) == typeof(int))
                    return (T) (object) int.Parse(value);

                if (typeof(T) == typeof(uint))
                    return (T)(object)uint.Parse(value);

                if (typeof(T) == typeof(long))
                    return (T) (object) long.Parse(value);

                if (typeof(T) == typeof(ulong))
                    return (T)(object)ulong.Parse(value);

                if (typeof(T) == typeof(float))
                    return (T) (object) float.Parse(value);

                if (typeof(T) == typeof(double))
                    return (T) (object) double.Parse(value);

                if (typeof(T) == typeof(decimal))
                    return (T) (object) decimal.Parse(value);

                if (typeof(T) == typeof(bool))
                    return (T) (object) bool.Parse(value);

                if (typeof(T).IsEnum)
                    return (T) Enum.Parse(typeof(T), value);

                if (typeof(T) == typeof(TimeSpan))
                    return (T) (object) TimeSpan.Parse(value);

                if (typeof(T) == typeof(DateTime))
                    return (T) (object) DateTime.Parse(value);

                return JsonConvert.DeserializeObject<T>(value);
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