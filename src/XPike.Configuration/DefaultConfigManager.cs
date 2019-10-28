namespace XPike.Configuration
{
    public class DefaultConfigManager<TConfig>
        : ConfigManager<TConfig>
        where TConfig : class
    {
        public DefaultConfigManager(IConfigurationService configurationService)
            : base(configurationService)
        {
            // NOTE: Intentional no-op.
        }
    }
}