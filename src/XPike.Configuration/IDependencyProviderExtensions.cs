using XPike.Configuration.Pipeline;
using XPike.IoC;

namespace XPike.Configuration
{
    public static class IDependencyProviderExtensions
    {
        public static IDependencyProvider AddConfigurationPipe<TPipe>(this IDependencyProvider provider)
            where TPipe : class, IConfigurationPipe
        {
            provider.ResolveDependency<IConfigurationService>()
                .AddToPipeline(provider.ResolveDependency<TPipe>());

            return provider;
        }
    }
}