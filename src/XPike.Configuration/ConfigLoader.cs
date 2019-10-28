using System;

namespace XPike.Configuration
{
    public class ConfigLoader<TConfig>
        : IConfigLoader<TConfig>
        where TConfig : class
    {
        private readonly IConfig<TConfig> _config;

        public ConfigLoader(IConfigManager<TConfig> configManager)
        {
            _config = configManager.GetConfig();
        }

        public string ConfigurationKey =>
            _config.ConfigurationKey;

        public TConfig Value =>
            _config.Value;

        public DateTime RetrievedUtc =>
            _config.RetrievedUtc;
    }
}