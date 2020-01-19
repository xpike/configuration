using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace XPike.Configuration.Microsoft
{
    /// <summary>
    /// Provides configuration values to XPike by delegating lookups to the
    /// Microsoft.Extensions.Configuration.IConfiguration service.
    /// </summary>
    public class MicrosoftConfigurationProvider
        : ConfigurationProviderBase,
          IMicrosoftConfigurationProvider
    {
        private readonly IConfiguration _configuration;

        public MicrosoftConfigurationProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override T Deserialize<T>(string value) =>
            JsonConvert.DeserializeObject<T>(value, new AppSettingsArrayJsonConverter());

        private string CreateJson(IConfigurationSection section)
        {
            if (section == null)
                return null;

            var children = section.GetChildren().ToList();
            if (!children.Any())
                return null;

            var sb = new StringBuilder();
            sb.Append("{");

            bool any = false;
            foreach (var item in children)
            {
                sb.AppendLine(any ? "," : string.Empty);

                if (item.Value == null)
                {
                    sb.Append($"\"{item.Key.Replace("\"", "\\\"")}\": {CreateJson(section.GetSection(item.Key))}");
                }
                else
                {
                    sb.Append($"\"{item.Key.Replace("\"", "\\\"")}\": \"{item.Value.Replace("\"", "\\\"")}\"");
                }

                any = true;
            }

            sb.AppendLine("\r\n}");

            return sb.ToString();
        }

        public override string GetValueOrDefault(string key, string defaultValue = null)
        {
            try
            {
                var actualKey = key.Replace(".", ":").Replace("::", ":");

                return _configuration[actualKey] ??
                       CreateJson(_configuration.GetSection(actualKey)) ??
                       defaultValue;
            }
            catch (Exception)
            {
                // TODO: Do we want to dump this "panic" state to console/debug output?

                return defaultValue;
            }
        }

        public override Task<string> GetValueOrDefaultAsync(string key, string defaultValue = null) =>
            Task.FromResult(GetValueOrDefault(key, defaultValue));

        public IDictionary<string, string> Load() =>
            _configuration.AsEnumerable().ToDictionary(x => x.Key, x => x.Value);

        public Task<IDictionary<string, string>> LoadAsync() =>
            Task.FromResult(Load());
    }
}