using System;
using System.Threading.Tasks;

namespace XPike.Configuration
{
    public interface IConfig<TConfig>
        where TConfig : class
    {
        string ConfigurationKey { get; }

        TConfig CurrentValue { get; }

        Task<TConfig> GetLatestValueAsync();

        TConfig GetLatestValue();

        DateTime RetrievedUtc { get; }
    }
}