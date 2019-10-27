using System;
using Microsoft.Extensions.Configuration;

namespace XPike.Configuration.Microsoft.AspNetCore
{
    public static class IConfigurationBuilderExtensions
    {
        /// <summary>
        /// Adds XPike Configuration to the IConfigurationBuilder so that IConfiguration can
        /// retrieve configuration values from the XPike Configuration Service.
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="configService"></param>
        /// <returns></returns>
        public static IConfigurationBuilder AddXPikeConfiguration(this IConfigurationBuilder builder, IConfigurationService configService) =>
            builder.Add(new XPikeConfigurationSource(configService));

        public static IConfigurationBuilder ConfigureXPikeConfiguration(this IConfigurationBuilder builder, Action<IXPikeConfigBuilder> configBuilderSetup)
        {
            // Create our xPike Config Builder
            var configBuilder = new XPikeConfigBuilder();

            // Call our setup action - this is where someone invoking AddXPikeLogging registers their Providers.
            configBuilderSetup(configBuilder);

            // Construct the initial ConfigurationService.  This is only used to feed values into Microsoft IConfiguration.
            var xpikeConfig = configBuilder.Build();

            // Add the actual xPike Configuration Source - this is the shim into Microsoft IConfiguration that actually wires-up our registered xPike Providers
            builder.Add(new XPikeConfigurationSource(xpikeConfig));

            return builder;
        }
    }
}