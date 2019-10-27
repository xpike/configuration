namespace XPike.Configuration.Environment
{
    /// <summary>
    /// This interface represents a Configuration Providers that retrieves
    /// its value from System Environment Variables.
    /// </summary>
    public interface IEnvironmentConfigurationProvider
        : IConfigurationProvider,
          IConfigurationLoader
    {
    }
}