using System;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using XPike.IoC.Microsoft;

namespace XPike.Configuration.Microsoft.AspNetCore
{
    /// <summary>
    /// Extension methods for registering XPike with Microsoft.DependencyInjection.
    /// </summary>
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// Registers XPike.Configuration with the DI container.
        /// Uses an optional IDictionary to obtain default configuration values.
        /// 
        /// The collection of configuration providers will be obtained from the DI container.
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="staticConfig"></param>
        /// <returns></returns>
        public static IServiceCollection AddXPikeConfiguration(this IServiceCollection collection, IDictionary<string, string> staticConfig = null)
        {
            new MicrosoftDependencyCollection(collection).AddXPikeConfiguration(staticConfig);
            return collection;
        }

        /// <summary>
        /// Registers XPike.Configuration with the DI container with a custom-built IConfigurationService.
        /// Uses an optional IDictionary to obtain default configuration values.
        /// 
        /// The collection of configuration providers registered with the DI container will not be used.
        /// </summary>
        /// <param name="collection"></param>
        /// <param name="providerSetup"></param>
        /// <returns></returns>
        public static IServiceCollection AddXPikeConfiguration(this IServiceCollection collection, Action<IXPikeConfigBuilder> providerSetup)
        {
            new MicrosoftDependencyCollection(collection).AddXPikeConfiguration(providerSetup);
            return collection;
        }

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
        public static IServiceCollection AddXPikeMicrosoftConfiguration(this IServiceCollection collection,
            IDictionary<string, string> staticConfig = null,
            IConfiguration configuration = null)
        {
            new MicrosoftDependencyCollection(collection).AddXPikeMicrosoftConfiguration(staticConfig, configuration);
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
        public static IServiceCollection UseMicrosoftConfigurationForXPike(this IServiceCollection collection,
            IDictionary<string, string> staticConfig = null,
            IConfiguration configuration = null)
        {
            new MicrosoftDependencyCollection(collection).UseMicrosoftConfigurationForXPike(staticConfig, configuration);
            return collection;
        }
    }
}