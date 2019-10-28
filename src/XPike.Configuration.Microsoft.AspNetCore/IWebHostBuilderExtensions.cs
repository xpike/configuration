using Microsoft.AspNetCore.Hosting;
using System;
using XPike.IoC.Microsoft;

namespace XPike.Configuration.Microsoft.AspNetCore
{
    public static class IWebHostBuilderExtensions
    {
        public static IWebHostBuilder AddXPikeConfiguration(this IWebHostBuilder hostBuilder, Action<IXPikeConfigBuilder> configBuilderSetup)
        {
            // Wire registered XPike Configuration Providers into Microsoft IConfiguration
            hostBuilder.ConfigureAppConfiguration((context, configBuilder) =>
                configBuilder.ConfigureXPikeConfiguration(configBuilderSetup)
            );

            // Wire an inverse shim from Microsoft IConfiguration to XPike Configuration
            // Add this to Dependency Injection for further usage within xPike
            hostBuilder.ConfigureServices((context, services) =>
            {
                new MicrosoftDependencyCollection(services).AddXPikeConfiguration(builder =>
                    builder.AddProvider(new MicrosoftConfigurationProvider(context.Configuration)));

                // Alternatively could do this...
                //new MicrosoftDependencyCollection(services).LoadPackage(new XPike.Configuration.Microsoft.AspNetCore.Package());
            });

            return hostBuilder;
        }
    }
}