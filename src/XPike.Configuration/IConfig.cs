using System;

namespace XPike.Configuration
{
    public interface IConfig<TConfig>
        where TConfig : class
    {
        string ConfigurationKey { get; }

        TConfig Value { get; }

        DateTime RetrievedUtc { get; }
    }
}