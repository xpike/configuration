namespace XPike.Configuration
{
    public interface IConfigManager<TConfig>
        where TConfig : class
    {
        IConfig<TConfig> GetConfig();

        IConfig<TConfig> GetConfigOrDefault(TConfig defaultValue);

        TConfig GetValue();
    }
}