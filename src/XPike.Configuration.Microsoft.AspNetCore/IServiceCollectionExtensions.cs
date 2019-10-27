using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace XPike.Configuration.Microsoft.AspNetCore
{
    public static class IServiceCollectionExtensions
    {
        /// <summary>
        /// Configures XPike Configuration to retrieve its values from
        /// Microsoft.Extensions.Configuration.IConfiguration.
        /// </summary>
        /// <param name="serviceCollection"></param>
        public static void UseMicrosoftConfigurationForXPike(this IServiceCollection serviceCollection)
        {
            serviceCollection.RemoveAll<IConfigurationProvider>();
            serviceCollection.AddSingleton<IConfigurationProvider, MicrosoftConfigurationProvider>();
        }
    }
}