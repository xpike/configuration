using System;
using Microsoft.Extensions.Configuration;

namespace XPike.Configuration.Microsoft.AspNetCore
{
    /// <summary>
    /// Extension methods to configure XPike to provide settings to Microsoft.Extensions IConfiguration.
    /// </summary>
    public static class IConfigurationBuilderExtensions
    {
        /// <summary>
        /// Adds an XPike IConfigurationService as a source for Microsoft.Extensions.IConfiguration.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configService"></param>
        /// <returns></returns>
        public static IConfigurationBuilder AddXPikeConfiguration(this IConfigurationBuilder builder, IConfigurationService configService) =>
            // Add the IConfigurationService as a Source to the IConfigurationBuilder - this shims into Microsoft IConfiguration and wires-up the xPike Providers.
            builder.Add(new XPikeConfigurationSource(configService));

        /// <summary>
        /// Configures a new XPike IConfigurationService and adds it as a source for Microsoft.Extensions.IConfiguration.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="xpikeProviderSetup"></param>
        /// <returns></returns>
        public static IConfigurationBuilder AddXPikeConfiguration(this IConfigurationBuilder builder, Action<IXPikeConfigBuilder> xpikeProviderSetup = null) =>
            // Construct and configure providers in the IConfigurationService.  This is used to feed values into Microsoft IConfiguration.
            builder.AddXPikeConfiguration(new XPikeConfigBuilder(xpikeProviderSetup).Build());
    }
}