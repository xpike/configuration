using Microsoft.Extensions.Configuration;

namespace XPike.Configuration.Microsoft.AspNetCore
{
    /// <summary>
    /// Represents a Configuration Source for Microsoft.Extensions.Configuration
    /// which retrieves its values from XPike Configuration.
    /// </summary>
    public interface IXPikeConfigurationSource
        : IConfigurationSource,
          global::Microsoft.Extensions.Configuration.IConfigurationProvider
    {
    }
}