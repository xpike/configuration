using System;
using System.Threading.Tasks;

namespace XPike.Configuration
{
    public class StaticConfig<TConfig>
        : IConfig<TConfig>
        where TConfig : class
    {
        public string ConfigurationKey { get; }

        public TConfig CurrentValue { get; }
        
        public DateTime RetrievedUtc { get; }

        public StaticConfig(TConfig config)
            : this(typeof(TConfig).FullName, config)
        {
        }

        public StaticConfig(string configKey, TConfig config)
        {
            CurrentValue = config;
            ConfigurationKey = configKey;
            RetrievedUtc = DateTime.UtcNow;
        }

        public Task<TConfig> GetLatestValueAsync() =>
            Task.FromResult(GetLatestValue());

        public TConfig GetLatestValue() =>
            CurrentValue;
    }
}