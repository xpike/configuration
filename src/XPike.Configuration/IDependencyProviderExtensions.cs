using XPike.Configuration.Pipeline;
using XPike.IoC;

namespace XPike.Configuration
{
    /// <summary>
    /// Extension methods for registering XPike Configuration middleware.
    /// </summary>
    public static class IDependencyProviderExtensions
    {
        /// <summary>
        /// Adds an instance of IConfigurationPipe to the list of XPike Configuration middleware.
        /// </summary>
        /// <typeparam name="TPipe"></typeparam>
        /// <param name="provider"></param>
        /// <param name="pipe"></param>
        /// <returns></returns>
        public static IDependencyProvider AddConfigurationPipe<TPipe>(this IDependencyProvider provider, TPipe pipe)
            where TPipe : class, IConfigurationPipe
        {
            provider.ResolveDependency<IConfigurationService>()
                .AddToPipeline(pipe);

            return provider;
        }

        /// <summary>
        /// Obtains an instance of an IConfigurationPipe specified by TPipe from the DI container
        /// and adds it to the list of XPike Configuration middleware.
        /// </summary>
        /// <typeparam name="TPipe"></typeparam>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static IDependencyProvider AddConfigurationPipe<TPipe>(this IDependencyProvider provider)
            where TPipe : class, IConfigurationPipe =>
            provider.AddConfigurationPipe(provider.ResolveDependency<TPipe>());
    }
}