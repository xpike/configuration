using Microsoft.Extensions.Configuration;
using XPike.IoC;

namespace XPike.Configuration.Microsoft
{
    /// <summary>
    /// XPike Configuration - Microsoft.Extensions.Configuration Provider
    /// 
    /// Package Dependencies:
    /// - XPike.Configuration
    /// 
    /// Singleton Registrations:
    /// - IMicrosoftConfigurationProvier => MicrosoftConfigurationProvider
    /// 
    /// Collection Registrations:
    /// - IConfigurationProvider => IMicrosoftConfigurationProvider
    /// </summary>
    public class Package
        : IDependencyPackage
    {
        private readonly IConfiguration _configuration;

        public Package()
        {
        }

        public Package(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void RegisterPackage(IDependencyCollection container)
        {
            // Load other Packages we depend on.
            container.LoadPackage(new XPike.Configuration.Package());

            // Register the XPike Configuration Provider for Microsoft.Extensions.Configuration
            if (_configuration == null)
            {
                container.RegisterSingleton<IMicrosoftConfigurationProvider, MicrosoftConfigurationProvider>();
            }
            else
            {
                container.RegisterSingleton<IMicrosoftConfigurationProvider>(new MicrosoftConfigurationProvider(_configuration));
            }

            // Set the provider to be the only one in the list of Providers used by the Configuration Service
            container.ResetCollection<IConfigurationProvider>();
            container.AddSingletonToCollection<IConfigurationProvider, MicrosoftConfigurationProvider>(provider => provider.ResolveDependency<IMicrosoftConfigurationProvider>());
        }
    }
}