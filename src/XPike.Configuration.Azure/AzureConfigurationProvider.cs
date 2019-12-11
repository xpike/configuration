using Azure.ApplicationModel.Configuration;
using System;
using System.Threading.Tasks;

namespace XPike.Configuration.Azure
{
    /// <summary>
    /// Retrieves Configuration values from the Azure App Configuration Service.
    /// 
    /// This is done using the cross-platform-compatible Azure Client SDK.
    /// This implementation does not provide any of the advanced feature set such as
    /// Configuration Builders, that the ASP.NET Core and .NET Framework variants offer.
    /// 
    /// Since this constructor requires a Connection String to be passed in, it must
    /// be registered using:
    /// container.AddSingleton&lt;IConfigurationProvider&gt;(new AzureConfigurationProvider(connectionString));
    /// </summary>
    public class AzureConfigurationProvider
        : ConfigurationProviderBase,
          IAzureConfigurationProvider
    {
        private readonly ConfigurationClient _client;

        public AzureConfigurationProvider(string connectionString)
            : base()
        {
            _client = new ConfigurationClient(connectionString);
        }

        public override string GetValueOrDefault(string key, string defaultValue = null)
        {
            try
            {
                var response = _client.Get(key);
                return response.Value?.Value ?? defaultValue;
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        public override async Task<string> GetValueOrDefaultAsync(string key, string defaultValue = null)
        {
            try
            {
                var response = await _client.GetAsync(key);
                return response.Value?.Value ?? defaultValue;
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }
    }
}