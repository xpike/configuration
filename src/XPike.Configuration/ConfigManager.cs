using System;

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

        public virtual IConfig<TConfig> GetConfig() =>
            new Config<TConfig>(ConfigurationKey, PostConfigure(_configService.GetValue<TConfig>(ConfigurationKey)));

        public virtual IConfig<TConfig> GetConfigOrDefault(TConfig defaultValue) =>
            new Config<TConfig>(ConfigurationKey, PostConfigure(_configService.GetValueOrDefault<TConfig>(ConfigurationKey, defaultValue)));

        public virtual TConfig GetValue() =>
            GetConfig().Value;

        /* Helper Methods for configuration value retrieval */

        protected virtual T GetValueOrDefault<T>(string key, T defaultValue = default) =>
            _configService.GetValueOrDefault($"{ConfigurationKey}::{key}", defaultValue);

        protected virtual T GetValue<T>(string key) =>
            _configService.GetValue<T>($"{ConfigurationKey}::{key}");

        protected string GetValue(string key) =>
            _configService.GetValue($"{ConfigurationKey}::{key}");

        protected string GetValueOrDefault(string key, string defaultValue = null) =>
            _configService.GetValueOrDefault($"{ConfigurationKey}::{key}", defaultValue);
    }
}
