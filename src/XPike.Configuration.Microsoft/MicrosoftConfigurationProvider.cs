using System;
using System.Text;
using Microsoft.Extensions.Configuration;

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

        private string CreateJson(IConfigurationSection section)
        {
            if (section == null)
                return null;

            var sb = new StringBuilder();
            sb.Append("{");

            bool any = false;
            foreach(var item in section.GetChildren())
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
                return _configuration[key] ?? 
                    CreateJson(_configuration.GetSection(key)) ??
                    defaultValue;
            }
            catch (Exception)
            {
                // TODO: Do we want to dump this "panic" state to console/debug output?

                return defaultValue;
            }
        }
    }
}