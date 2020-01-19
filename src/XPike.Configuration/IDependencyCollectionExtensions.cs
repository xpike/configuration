using System;
using System.Collections.Generic;
using XPike.IoC;

namespace XPike.Configuration
{
    /// <summary>
    /// Extension methods for registering XPike Configuration with a DI container.
    /// </summary>
    public static class IDependencyCollectionExtensions
    {
        /// <summary>
        /// Registers XPike.Configuration with the DI container.
        /// Uses an optional IDictionary to obtain default configuration values.
        /// 
        /// The collection of configuration providers will be obtained from the DI container.
        /// </summary>
        /// <param name="dependencyCollection"></param>
        /// <param name="staticConfig"></param>
        /// <returns></returns>
        public static IDependencyCollection AddXPikeConfiguration(this IDependencyCollection dependencyCollection,
            IDictionary<string, string> staticConfig = null)
        {
            dependencyCollection.LoadPackage(new XPike.Configuration.Package(staticConfig));
            return dependencyCollection;
        }

        /// <summary>
        /// Registers XPike.Configuration with the DI container with a custom-built IConfigurationService.
        /// Uses an optional IDictionary to obtain default configuration values.
        /// 
        /// The collection of configuration providers registered with the DI container will not be used.
        /// </summary>
        /// <param name="dependencyCollection"></param>
        /// <param name="providerSetup"></param>
        /// <param name="staticConfig"></param>
        /// <returns></returns>
        public static IDependencyCollection AddXPikeConfiguration(this IDependencyCollection dependencyCollection, Action<IXPikeConfigBuilder> providerSetup)
        {
            dependencyCollection.AddXPikeConfiguration()
                .RegisterSingleton(new XPikeConfigBuilder(providerSetup).Build());

            return dependencyCollection;
        }
    }
}