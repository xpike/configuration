using System.Threading.Tasks;

namespace XPike.Configuration
{
    /// <summary>
    /// Defines a Configuration Provider, the most-granular source of configuration values in XPike.
    /// These are the basis of all of the functionality in XPike.Configuration and XPike.Settings.
    /// </summary>
    public interface IConfigurationProvider
    {
        /// <summary>
        /// Retrieves a "required" Configuration Value from the specified Key.
        /// If no value exists, an InvalidConfigurationException will be thrown.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string GetValue(string key);

        Task<string> GetValueAsync(string key);

        /// <summary>
        /// Retrieves a "required" Configuration Value of the desired type T from the specified Key.
        /// If not value exists, or if conversion fails, an InvalidConfigurationException will be thrown.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T GetValue<T>(string key);

        Task<T> GetValueAsync<T>(string key);

        /// <summary>
        /// Retrieves an "optional" Configuration Value from the specified Key.
        /// If no value exists, the specified defaultValue will be returned instead.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        string GetValueOrDefault(string key, string defaultValue = null);

        Task<string> GetValueOrDefaultAsync(string key, string defaultValue = null);

        /// <summary>
        /// Retrieves an "optional" Configuration Value of the desired type T from the specified Key.
        /// If no value exists, or if conversion fails, the specified defaultValue will be returned instead.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        T GetValueOrDefault<T>(string key, T defaultValue = default);

        Task<T> GetValueOrDefaultAsync<T>(string key, T defaultValue = default);
    }
}