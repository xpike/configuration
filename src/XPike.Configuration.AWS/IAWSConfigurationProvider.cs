namespace XPike.Configuration.AWS
{
    /// <summary>
    /// Represents a Configuration Provider that retrieves its settings from the
    /// AWS SSM Simple Parameter Service.
    /// </summary>
    public interface IAWSConfigurationProvider
        : IConfigurationProvider
    {
    }
}