using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using XPike.IoC;

namespace XPike.Configuration.Microsoft
{
    public static class IDependencyCollectionExtensions
    {
        /// <summary>
        /// Adds Microsoft Extensions Configuration as a provider for XPike Configuration.
        /// Uses an optional IDictionary to retrieve default configuration values.
        /// Obtains XPike Configuration Providers from the DI container.
        /// If no IConfiguration is specified, this will also be retrieved from the DI container.
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="staticConfig"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IDependencyCollection AddXPikeMicrosoftConfiguration(this IDependencyCollection collection,
            IDictionary<string, string> staticConfig = null,
            IConfiguration configuration = null)
        {
            collection.LoadPackage(new XPike.Configuration.Microsoft.Package(configuration, staticConfig));
            return collection;
        }

        /// <summary>
        /// Adds Microsoft Extensions Configuration as the only provider for XPike Configuration after removing any previous registrations.
        /// Uses an optional IDictionary to retrieve default configuration values.
        /// Obtains XPike Configuration Providers from the DI container.
        /// If no IConfiguration is specified, this will also be retrieved from the DI container.
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="staticConfig"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IDependencyCollection UseMicrosoftConfigurationForXPike(this IDependencyCollection collection,
            IDictionary<string, string> staticConfig = null,
            IConfiguration configuration = null)
        {
            collection.ResetCollection<IConfigurationProvider>();
            return collection.AddXPikeMicrosoftConfiguration(staticConfig, configuration);
        }
    }
}