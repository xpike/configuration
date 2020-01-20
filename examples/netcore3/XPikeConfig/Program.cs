using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using XPike.Configuration.Microsoft.AspNetCore;
using XPike.Configuration.Memory;
using System.Collections.Generic;
using Example.Library;
using Newtonsoft.Json;
using XPike.IoC.SimpleInjector.AspNetCore;

namespace XPikeConfig
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
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
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); })
                .AddXPikeDependencyInjection();
    }
}