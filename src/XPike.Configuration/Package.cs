using System.Collections.Generic;
using XPike.Configuration.Environment;
using XPike.Configuration.Memory;
using XPike.Configuration.Null;
using XPike.IoC;

namespace XPike.Configuration
{
    /// <summary>
    /// XPike Configuration Package
    /// 
    /// Singleton Registrations:
    /// - IEnvironmentConfigurationProvider => EnvironmentConfigurationProvider 
    /// - INullConfigurationProvider => NullConfigurationProvider
    /// - IConfigurationService => ConfigurationService
    /// - IMemoryConfigurationProvider => MemoryConfigurationProvider (when specified, see note below)
    /// 
    /// Collection Registrations:
    /// - IConfigurationProvider += EnvironmentConfigurationProvider
    /// - IConfigurationProvider += MemoryConfigurationProvider (when specified, see note below)
    /// 
    /// NOTE:
    /// If the Package is instantiated by passing in a Dictionary&lt;string, string&gt;,
    /// these will be used as development-time defaults.  Values in other registered Providers
    /// (Environment Configuration Provider is added by default) will override these.
    /// </summary>
    public class Package
        : IDependencyPackage
    {
        private readonly IDictionary<string, string> _configuration;

        public Package()
        {
        }

        public Package(IDictionary<string, string> configuration)
        {
            _configuration = configuration;
        }

        public void RegisterPackage(IDependencyCollection dependencyCollection)
        {
            // NOTE: No need to load the IoC package.  This is a responsibility of the IoC Provider.

            dependencyCollection.RegisterSingleton<IEnvironmentConfigurationProvider, EnvironmentConfigurationProvider>();
            dependencyCollection.RegisterSingleton<INullConfigurationProvider, NullConfigurationProvider>();

            // NOTE: This is not valid, because MemoryConfigurationProvider's constructor expects a Dictionary<string, string>.
            // dependencyCollection.RegisterSingleton<IMemoryConfigurationProvider, MemoryConfigurationProvider>();

            // NOTE: But this is valid:
            //dependencyCollection.RegisterSingleton<IMemoryConfigurationProvider>(provider => new MemoryConfigurationProvider(new Dictionary<string, string>
            //{
            //    {"key", "value"}
            //}));

            if (_configuration != null)
            {
                dependencyCollection.RegisterSingleton<IMemoryConfigurationProvider>(new MemoryConfigurationProvider(_configuration));
                dependencyCollection.AddSingletonToCollection<IConfigurationProvider, MemoryConfigurationProvider>(provider => provider.ResolveDependency<IMemoryConfigurationProvider>());
            }

            dependencyCollection.AddSingletonToCollection<IConfigurationProvider, EnvironmentConfigurationProvider>((provider) => {
                return provider.ResolveDependency<IEnvironmentConfigurationProvider>();
            });

            dependencyCollection.RegisterSingleton<IConfigurationService, ConfigurationService>();
        }
    }
}