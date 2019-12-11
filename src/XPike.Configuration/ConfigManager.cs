using System;
using System.Threading.Tasks;

namespace XPike.Configuration
{
    public class ConfigManager<TConfig>
        : IConfigManager<TConfig>
          where TConfig : class
    {
        protected readonly IConfigurationService _configService;
        protected readonly Action<TConfig> _postConfigureAction;

        protected virtual string ConfigurationKey { get; }

        public ConfigManager(IConfigurationService configService)
            : this(configService, typeof(TConfig).ToString())
        {
        }

        public ConfigManager(IConfigurationService configService, string configKey)
            : this(configService, configKey, null)
        {
        }

        public ConfigManager(IConfigurationService configService, Action<TConfig> postConfigureAction)
            : this(configService, typeof(TConfig).ToString(), postConfigureAction)
        {
        }

        public ConfigManager(IConfigurationService configService, string configKey, Action<TConfig> postConfigureAction)
        {
            _configService = configService;
            ConfigurationKey = configKey;
            _postConfigureAction = postConfigureAction;
        }

        public virtual TConfig PostConfigure(TConfig settings)
        {
            _postConfigureAction?.Invoke(settings);

            return settings;
        }

        public IConfig<TConfig> GetConfig() =>
            new Config<TConfig>(ConfigurationKey, PostConfigure(_configService.GetValue<TConfig>(ConfigurationKey)), _configService);

        public virtual async Task<IConfig<TConfig>> GetConfigAsync() =>
            new Config<TConfig>(ConfigurationKey, PostConfigure(await _configService.GetValueAsync<TConfig>(ConfigurationKey)), _configService);

        public IConfig<TConfig> GetConfigOrDefault(TConfig defaultValue) =>
            new Config<TConfig>(ConfigurationKey, PostConfigure(_configService.GetValueOrDefault<TConfig>(ConfigurationKey, defaultValue)), _configService);

        public virtual async Task<IConfig<TConfig>> GetConfigOrDefaultAsync(TConfig defaultValue) =>
            new Config<TConfig>(ConfigurationKey, PostConfigure(await _configService.GetValueOrDefaultAsync<TConfig>(ConfigurationKey, defaultValue)), _configService);

        public TConfig GetValue() =>
            GetConfig().CurrentValue;

        public virtual async Task<TConfig> GetValueAsync() =>
            (await GetConfigAsync()).CurrentValue;

        /* Helper Methods for configuration value retrieval */

        protected virtual Task<T> GetValueOrDefaultAsync<T>(string key, T defaultValue = default) =>
            _configService.GetValueOrDefaultAsync($"{ConfigurationKey}::{key}", defaultValue);

        protected virtual T GetValueOrDefault<T>(string key, T defaultValue = default) =>
            _configService.GetValueOrDefault($"{ConfigurationKey}::{key}", defaultValue);

        protected virtual Task<T> GetValueAsync<T>(string key) =>
            _configService.GetValueAsync<T>($"{ConfigurationKey}::{key}");

        protected virtual T GetValue<T>(string key) =>
            _configService.GetValue<T>($"{ConfigurationKey}::{key}");

        protected Task<string> GetValueAsync(string key) =>
            _configService.GetValueAsync($"{ConfigurationKey}::{key}");

        protected string GetValue(string key) =>
            _configService.GetValue($"{ConfigurationKey}::{key}");

        protected Task<string> GetValueOrDefaultAsync(string key, string defaultValue = null) =>
            _configService.GetValueOrDefaultAsync($"{ConfigurationKey}::{key}", defaultValue);

        protected string GetValueOrDefault(string key, string defaultValue = null) =>
            _configService.GetValueOrDefault($"{ConfigurationKey}::{key}", defaultValue);
    }
}
