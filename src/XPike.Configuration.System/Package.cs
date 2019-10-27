using XPike.IoC;

namespace XPike.Configuration.System
{
    /// <summary>
    /// XPike Configuration - System.Configuration.ConfigurationManager Provider
    /// 
    /// Package Dependencies:
    /// - XPike.Configuration
    /// 
    /// Singleton Registrations:
    /// - ISystemConfigurationProvider => SystemConfigurationProvider
    /// 
    /// Collection Registrations:
    /// - IConfigurationProvider += ISystemConfigurationProvider
    /// </summary>
    public class Package
        : IDependencyPackage
    {
        public void RegisterPackage(IDependencyCollection container)
        {
            container.LoadPackage(new XPike.Configuration.Package());

            container.RegisterSingleton<ISystemConfigurationProvider, SystemConfigurationProvider>();

            container.AddSingletonToCollection<IConfigurationProvider, ISystemConfigurationProvider>((provider) =>
            {
                return provider.ResolveDependency<ISystemConfigurationProvider>();
            });
        }
    }
}