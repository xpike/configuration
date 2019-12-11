namespace XPike.Configuration
{
    /// <summary>
    /// Orchestrates the construction of an XPike Configuration Service.
    /// </summary>
    public interface IXPikeConfigBuilder
    {
        /// <summary>
        /// Gets the Configuration Service once it has been built.
        /// Calling this before Build() will throw an exception.
        /// </summary>
        IConfigurationService ConfigurationService { get; }

        /// <summary>
        /// Adds a Configuration Provider to the mappings.
        /// These will be used to construct the Configuration Service.
        /// Calling this after Build() will throw an exception.
        /// </summary>
        /// <param name="instance"></param>
        IXPikeConfigBuilder AddProvider(IConfigurationProvider instance);

        /// <summary>
        /// Clears the list of registered Configuration Providers.
        /// Calling this after Build() will throw an exception.
        /// </summary>
        IXPikeConfigBuilder ClearProviders();

        /// <summary>
        /// Constructs and returns the Configuration Service using the registered Configuration Providers.
        /// Calling this more than once will throw an exception.
        /// </summary>
        /// <returns></returns>
        IConfigurationService Build();
    }
}