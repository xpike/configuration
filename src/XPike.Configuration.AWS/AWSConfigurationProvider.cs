using Amazon.SimpleSystemsManagement;
using Amazon.SimpleSystemsManagement.Model;
using System;
using System.Net;

namespace XPike.Configuration.AWS
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
    public class AWSConfigurationProvider
        : ConfigurationProviderBase,
          IAWSConfigurationProvider
    {
        private readonly AmazonSimpleSystemsManagementClient _client;

        public AWSConfigurationProvider()
            : base()
        {
            _client = new AmazonSimpleSystemsManagementClient();
        }

        /// <summary>
        /// TODO: Make this async, so we don't have to use .GetAwaiter().GetResult().
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public override string GetValueOrDefault(string key, string defaultValue = null)
        {
            try
            {
                var response = _client.GetParameterAsync(new GetParameterRequest
                {
                    Name = key,
                    WithDecryption = true
                }).GetAwaiter().GetResult();

                return (response.HttpStatusCode == HttpStatusCode.OK ?
                    response?.Parameter?.Value : defaultValue) ?? defaultValue;
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }
    }
}