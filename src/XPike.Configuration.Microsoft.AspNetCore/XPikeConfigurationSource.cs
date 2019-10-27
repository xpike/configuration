using Microsoft.Extensions.Configuration;

namespace XPike.Configuration.Microsoft.AspNetCore
{
    public class XPikeConfigurationSource
        : ConfigurationProvider,
          IXPikeConfigurationSource
    {
        private readonly IConfigurationService _configService;

        public XPikeConfigurationSource(IConfigurationService configService)
        {
            _configService = configService;
        }

        public global::Microsoft.Extensions.Configuration.IConfigurationProvider Build(IConfigurationBuilder builder) =>
            this;

        public override void Load() =>
            Data = _configService.Load();
    }
}