using System;
using System.Threading.Tasks;
using Example.Library;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using XPike.Configuration;

namespace XPikeConfigMS.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfigurationService _configService;
        private readonly IConfiguration _config;
        private readonly IConfig<SomeConfig> _someConfig;
        private readonly IConfig<AnotherConfig> _anotherConfig;
        private readonly IConfigManager<MissingConfig> _missingConfig;

        public HomeController(IConfigurationService configService, 
            IConfiguration config,
            IConfig<SomeConfig> someConfig, 
            IConfig<AnotherConfig> anotherConfig,
            IConfigManager<MissingConfig> missingConfig)
        {
            _configService = configService;
            _config = config;
            _someConfig = someConfig;
            _anotherConfig = anotherConfig;
            _missingConfig = missingConfig;
        }

        public async Task<IActionResult> IndexAsync()
        {
            ViewData["config"] = JsonConvert.SerializeObject(await _configService.GetValueAsync<SomeConfig>("Example.Library.SomeConfig"));
            ViewData["missingConfig"] = _missingConfig.GetConfigOrDefault(new MissingConfig {Name = "Default"})
                .GetLatestValue()
                .Name;

            ViewData["missingConfigAsync"] = (await (await _missingConfig.GetConfigOrDefaultAsync(new MissingConfig {Name = "Default"})).GetLatestValueAsync()).Name;

            // NOTE: Test not entirely applicable - since IConfiguration does not support de-serialization from an individual key which stores a JSON string.
            //ViewData["config2"] = JsonConvert.SerializeObject(_config.GetSection("Example:Library:SomeConfig").Get<SomeConfig>());

            ViewData["config2"] = _config["Example:Library:SomeConfig"];

            ViewData["config3"] = "N/A - IConfiguration can't access properties of a JSON string.";
            ViewData["config4"] = "N/A - IConfigurationService can't access properties of a JSON string.";

            ViewData["config2-1"] = JsonConvert.SerializeObject(await _configService.GetValueAsync<AnotherConfig>("Example.Library.AnotherConfig"));
            ViewData["config2-2"] = JsonConvert.SerializeObject(_config.GetSection("Example:Library:AnotherConfig").Get<SomeConfig>());
            ViewData["config2-3"] = await _configService.GetValueAsync<string>("Example.Library.AnotherConfig::Name");
            ViewData["config2-4"] = _config["Example:Library:AnotherConfig:Name"];

            ViewData["value1"] = await _configService.GetValueAsync<DateTime>("Example.Library.SomeConfig::SomeDate");
            ViewData["value2"] = _config["Example:Library:SomeConfig:SomeDate"];

            ViewData["value2-1"] = await _configService.GetValueAsync<DateTime>("Example.Library.AnotherConfig::AnotherDate");
            ViewData["value2-2"] = _config["Example:Library:AnotherConfig:AnotherDate"];

            ViewData["someconfig"] = JsonConvert.SerializeObject(_someConfig);
            ViewData["anotherconfig"] = JsonConvert.SerializeObject(_anotherConfig);

            return View("Index");
        }
    }
}