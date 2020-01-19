using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Example.Library;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using XPike.Configuration.Memory;
using XPike.Configuration.Microsoft.AspNetCore;

namespace XPikeConfig22
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseXPikeConfiguration(config =>
                {
                    config.AddProvider(new MemoryConfigurationProvider(new Dictionary<string, string>
                    {
                        {
                            "Example.Library.SomeConfig", JsonConvert.SerializeObject(new SomeConfig
                            {
                                Name = "some value",
                                Groups = new Dictionary<string, Dictionary<string, string>>
                                {
                                    {
                                        "Group1", new Dictionary<string, string>
                                        {
                                            {"item1", "value1"},
                                            {"item2", "value2"}
                                        }
                                    },
                                    {
                                        "Group2", new Dictionary<string, string>
                                        {
                                            {"item1", "group2value1"},
                                            {"item2", "group2value2"}
                                        }
                                    }
                                }
                            })
                        },
                        {"Example.Library.SomeConfig::SomeDate", "10/28/2019 2:21:40 AM"}
                    }));
                })
                .UseStartup<Startup>();
    }
}
