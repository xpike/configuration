namespace XPike.Configuration
{
    public interface IConfigLoader<TConfig>
        : IConfig<TConfig>
        where TConfig : class
    {
    }
}