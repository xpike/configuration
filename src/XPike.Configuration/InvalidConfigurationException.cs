using System;

namespace XPike.Configuration
{
    /// <summary>
    /// Represents an exception resulting from a failure to load a value from Configuration.
    /// This could be from a missing key, a failed conversion, or any other error in the underlying Provider.
    /// Also used by XPike.Settings in identical scenarios.
    /// </summary>
    public class InvalidConfigurationException
        : Exception
    {
        private const string _MISSING_KEY = "Missing configuration key";
        private const string _KEY_ERROR = "Failed to retrieve configuration key";

        /// <summary>
        /// The Configuration Key that was requested which resulted in an error.
        /// </summary>
        public string ConfigurationKey { get; }

        /// <summary>
        /// Creates a new Invalid Configuration Exception resulting from a Missing Key.
        /// </summary>
        /// <param name="configurationKey"></param>
        public InvalidConfigurationException(string configurationKey)
            : base($"{_MISSING_KEY}: {configurationKey}")
        {
            ConfigurationKey = configurationKey;
        }

        /// <summary>
        /// Creates a new Invalid Configuration Exception resulting from an error (such as
        /// a failed type conversion), but NOT from a missing key - along with a message describing the failure.
        /// </summary>
        /// <param name="configurationKey"></param>
        /// <param name="message"></param>
        public InvalidConfigurationException(string configurationKey, string message)
            : base($"{_KEY_ERROR} '{configurationKey}': {message}")
        {
            ConfigurationKey = configurationKey;
        }

        /// <summary>
        /// Creates a new Invalid Configuration Exception which wraps an underlying Exception.
        /// It is expected that the wrapped Exception will provide further clarification as to the nature of the failure.
        /// </summary>
        /// <param name="configurationKey"></param>
        /// <param name="ex"></param>
        public InvalidConfigurationException(string configurationKey, Exception ex)
            : base($"{_KEY_ERROR}: {configurationKey} ({ex.GetType().Name}: {ex.Message})", ex)
        {
            ConfigurationKey = configurationKey;
        }
    }
}