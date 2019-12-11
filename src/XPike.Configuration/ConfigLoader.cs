using System;
using System.Threading.Tasks;

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

        public TConfig CurrentValue =>
            _config.CurrentValue;

        public Task<TConfig> GetLatestValueAsync() =>
            _config.GetLatestValueAsync();

        public TConfig GetLatestValue() =>
            _config.GetLatestValue();

        public DateTime RetrievedUtc =>
            _config.RetrievedUtc;
    }
}