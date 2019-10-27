using XPike.IoC;

namespace XPike.Configuration.Microsoft.AspNetCore
{
    /// <summary>
    /// XPike Configuration - Configuration Source for Microsoft.Extensions.Configuration
    /// NOTE: Loading this package should not be necessary.  It's here merely for consistency.
    /// 
    /// Package Dependencies:
    /// - XPike.Configuration
    /// 
    /// Singleton Registrations:
    /// - IXPikeConfigurationSource => XPikeConfigurationSource
    /// </summary>
    public class Package
        : IDependencyPackage
    {
        public void RegisterPackage(IDependencyCollection container)
        {
            container.LoadPackage(new XPike.Configuration.Package());

            container.RegisterSingleton<IXPikeConfigurationSource, XPikeConfigurationSource>();
        }
    }
}