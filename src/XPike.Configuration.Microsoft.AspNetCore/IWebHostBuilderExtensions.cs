﻿using System;
using Microsoft.AspNetCore.Hosting;

namespace XPike.Configuration.Microsoft.AspNetCore
{
    /// <summary>
    /// Extension methods to setup both XPike Configuration.
    /// </summary>
    public static class IWebHostBuilderExtensions
    {
        /// <summary>
        /// Registers XPike Configuration as a provider for Microsoft Extensions IConfiguration using
        /// the providers specified by the provider setup lambda.
        ///
        /// Registers Microsoft IConfiguration as the only provider for XPike IConfigurationService
        /// after clearing any other providers registered with the DI container collection.
        /// </summary>
        /// <param name="hostBuilder"></param>
        /// <param name="providerSetup"></param>
        /// <returns></returns>
        public static IWebHostBuilder UseXPikeConfiguration(this IWebHostBuilder hostBuilder,
            Action<IXPikeConfigBuilder> providerSetup = null) =>
            hostBuilder.ConfigureAppConfiguration((context, configBuilder) =>
                    configBuilder.AddXPikeConfiguration(providerSetup))
                .ConfigureServices((context, services) =>
                    services.UseMicrosoftConfigurationForXPike(null, context.Configuration));
    }
}