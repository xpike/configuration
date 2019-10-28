using System;

namespace XPike.Configuration
{
    public class Config<TConfig>
        : IConfig<TConfig>
        where TConfig : class
    {
        public TConfig Value { get; }

        public string ConfigurationKey { get; }

        public DateTime RetrievedUtc { get; }

        public Config(string configKey, TConfig config)
        {
            ConfigurationKey = configKey;
            Value = config;
            RetrievedUtc = DateTime.UtcNow;
        }
    }
}