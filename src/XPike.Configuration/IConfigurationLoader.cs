using System.Collections.Generic;
using System.Threading.Tasks;

namespace XPike.Configuration
{
    /// <summary>
    /// Defines a Configuration Loader.
    /// A Configuration Loader is a Configuration Provider that can load its settings in bulk.
    /// 
    /// This is necessary for compatibility with ASP.NET Core's IConfiguration and IOptions.
    /// Providers that do not implement this interface will not work with either of those paradigms.
    /// 
    /// XPike Configuration and XPike Settings are fully compatible with Providers that do not implement this interface, however.
    /// </summary>
    public interface IConfigurationLoader
    {
        /// <summary>
        /// Retrieves all key-value pairs that are available from this Provider.
        /// </summary>
        /// <returns></returns>
        IDictionary<string, string> Load();

        Task<IDictionary<string, string>> LoadAsync();

    }
}