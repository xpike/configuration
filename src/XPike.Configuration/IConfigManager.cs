using System.Threading.Tasks;

namespace XPike.Configuration
{
    public interface IConfigManager<TConfig>
        where TConfig : class
    {
        IConfig<TConfig> GetConfig();

        Task<IConfig<TConfig>> GetConfigAsync();

        IConfig<TConfig> GetConfigOrDefault(TConfig defaultValue);

        Task<IConfig<TConfig>> GetConfigOrDefaultAsync(TConfig defaultValue);

        TConfig GetValue();

        Task<TConfig> GetValueAsync();
    }
}