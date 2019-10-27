namespace XPike.Configuration.Null
{
    /// <summary>
    /// This interface represents a Configuration Provider that yields no values.
    /// </summary>
    public interface INullConfigurationProvider
        : IConfigurationProvider,
          IConfigurationLoader
    {
    }
}