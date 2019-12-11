using System;
using System.Threading.Tasks;

namespace XPike.Configuration
{
    public class Config<TConfig>
        : IConfig<TConfig>
        where TConfig : class
    {
        private readonly IConfigurationService _configurationService;

        public TConfig CurrentValue { get; protected set; }

        public string ConfigurationKey { get; }

        public DateTime RetrievedUtc { get; protected set; }

        public Config(string configKey, TConfig config, IConfigurationService configurationService)
        {
            ConfigurationKey = configKey;
            CurrentValue = config;
            RetrievedUtc = DateTime.UtcNow;
            _configurationService = configurationService;
        }

        public TConfig GetLatestValue()
        {
            try
            {
                CurrentValue = _configurationService.GetValue<TConfig>(ConfigurationKey);
                RetrievedUtc = DateTime.UtcNow;
            }
            catch (Exception)
            {
            }

            return CurrentValue;
        }

        public async Task<TConfig> GetLatestValueAsync()
        {
            try
            {
                CurrentValue = await _configurationService.GetValueAsync<TConfig>(ConfigurationKey);
                RetrievedUtc = DateTime.UtcNow;
            }
            catch (Exception)
            {
            }

            return CurrentValue;
        }
    }
}