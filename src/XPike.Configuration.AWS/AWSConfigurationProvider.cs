﻿using Amazon.SimpleSystemsManagement;
using Amazon.SimpleSystemsManagement.Model;
using System;
using System.Net;
using System.Threading.Tasks;

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
          IAWSConfigurationProvider,
          IDisposable
    {
        private readonly AmazonSimpleSystemsManagementClient _client;

        public AWSConfigurationProvider()
            : base()
        {
            _client = new AmazonSimpleSystemsManagementClient();
        }

        public override string GetValueOrDefault(string key, string defaultValue = null) =>
            GetValueOrDefaultAsync(key, defaultValue).GetAwaiter().GetResult();

        public override async Task<string> GetValueOrDefaultAsync(string key, string defaultValue = null)
        {
            try
            {
                var response = await _client.GetParameterAsync(new GetParameterRequest
                    {
                        Name = key,
                        WithDecryption = true
                    })
                    .ConfigureAwait(false);

                return (response.HttpStatusCode == HttpStatusCode.OK ?
                    response?.Parameter?.Value : defaultValue) ?? defaultValue;
            }
            catch (Exception)
            {
                return defaultValue;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                _client?.Dispose();
        }
    }
}