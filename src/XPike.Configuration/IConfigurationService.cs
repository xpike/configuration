namespace XPike.Configuration
{
    /// <summary>
    /// Represents a Configuration Service, which is the "end-user-facing consumable" from this Package.
    /// This is what you should use to retrieve configuration values in most cases.
    /// The Configuration Service is also expected to aggregate a Bulk Load of values from Providers that support it.
    /// 
    /// This parallels (and is inter-operable with) the IConfiguration service in ASP.NET Core.
    /// See the XPike.Configuration.Microsoft.AspNetCore package for details.
    /// </summary>
    public interface IConfigurationService
        : IConfigurationProvider,
          IConfigurationLoader
    {
    }
}