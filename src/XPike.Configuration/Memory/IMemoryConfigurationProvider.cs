namespace XPike.Configuration.Memory
{
    /// <summary>
    /// This interface represents a Configuration Provider that stores hard-coded
    /// settings in memory (such as a Dictionary&lt;string, string&gt;).
    /// </summary>
    public interface IMemoryConfigurationProvider
        : IConfigurationProvider,
          IConfigurationLoader
    {
    }
}