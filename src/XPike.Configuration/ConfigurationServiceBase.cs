using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace XPike.Configuration
{
    /// <summary>
    /// Abstract base implementation of a Configuration Service.
    /// 
    /// This is effectively a complete implementation, however.
    /// It is abstracted away because Configuration Service works identically to Settings Service, except for which type of Provider it uses.
    /// 
    /// This is very convenient since the Configuration Service and Settings Service share a common interface,
    /// as do Configuration Provider and Settings Provider.
    /// 
    /// To create a new Configuration Service, just inherit this class and specify the desired type of TProvider.
    /// </summary>
    /// <typeparam name="TProvider"></typeparam>
    public abstract class ConfigurationServiceBase<TProvider>
        : IConfigurationService
        where TProvider : IConfigurationProvider
    {
        private const string _PROVIDER_NULL = "The list of {0} instances can not contain null values.";

        protected readonly IList<TProvider> _providers;

        protected ConfigurationServiceBase(IEnumerable<TProvider> providers)
        {
            _providers = providers.ToList();

            if (_providers.Any(x => x == null))
                throw new ArgumentException(string.Format(_PROVIDER_NULL, typeof(TProvider).FullName), nameof(providers));

            _providers.Reverse();
        }

        public string GetValueOrDefault(string key, string defaultValue = null)
        {
            try
            {
                return GetValue(key) ?? defaultValue;
            }
            catch (Exception)
            {
                // TODO: Do we want to dump this to console/debug/trace output?

                return defaultValue;
            }
        }

        public async Task<string> GetValueOrDefaultAsync(string key, string defaultValue = null)
        {
            try
            {
                return await GetValueAsync(key) ?? defaultValue;
            }
            catch (Exception)
            {
                // TODO: Do we want to dump this to console/debug/trace output?

                return defaultValue;
            }
        }

        public string GetValue(string key)
        {
            string value;

            foreach (var provider in _providers)
            {
                try
                {
                    if ((value = provider.GetValue(key)) != null)
                        return value;
                }
                catch (Exception)
                {
                }
            }

            throw new InvalidConfigurationException(key);
        }

        public async Task<string> GetValueAsync(string key)
        {
            string value;

            foreach (var provider in _providers)
            {
                try
                {
                    if ((value = await provider.GetValueAsync(key)) != null)
                        return value;
                }
                catch (Exception)
                {
                }
            }

            throw new InvalidConfigurationException(key);
        }

        public T GetValue<T>(string key)
        {
            T value;

            foreach (var provider in _providers)
            {
                try
                {
                    if ((value = provider.GetValue<T>(key)) != null)
                        return value;
                }
                catch (Exception)
                {
                }
            }

            throw new InvalidConfigurationException(key);
        }

        public async Task<T> GetValueAsync<T>(string key)
        {
            T value;

            foreach (var provider in _providers)
            {
                try
                {
                    if ((value = await provider.GetValueAsync<T>(key)) != null)
                        return value;
                }
                catch (Exception)
                {
                }
            }

            throw new InvalidConfigurationException(key);
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

        public async Task<T> GetValueOrDefaultAsync<T>(string key, T defaultValue = default)
        {
            try
            {
                return await GetValueAsync<T>(key);
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        public IDictionary<string, string> Load()
        {
            var dict = new Dictionary<string, string>();

            // NOTE: Work from lowest to highest priority, to allow for proper coalescing of values.
            for (var i = _providers.Count - 1; i >= 0; --i)
            {
                var loader = _providers[i] as IConfigurationLoader;

                if (loader == null)
                    continue;

                foreach (var setting in loader.Load())
                    dict[setting.Key] = setting.Value;
            }

            return dict;
        }

        public async Task<IDictionary<string, string>> LoadAsync()
        {
            var dict = new Dictionary<string, string>();

            // NOTE: Work from lowest to highest priority, to allow for proper coalescing of values.
            for (var i = _providers.Count - 1; i >= 0; --i)
            {
                var loader = _providers[i] as IConfigurationLoader;

                if (loader == null)
                    continue;

                foreach (var setting in await loader.LoadAsync())
                    dict[setting.Key] = setting.Value;
            }

            return dict;
        }
    }
}