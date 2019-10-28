using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using XPike.Configuration.Microsoft.AspNetCore;
using XPike.Configuration.Memory;
using System.Collections.Generic;

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
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                              .AddXPikeConfiguration(config =>
                              {
                                  config.AddProvider(new MemoryConfigurationProvider(new Dictionary<string, string>
                                  {
                                      {"Example.Library.SomeConfig", "{\"Name\": \"Name Found!\"}"},
                                      {"Example.Library.SomeConfig::SomeDate", "10/28/2019 2:21:40 AM"}
                                  }));
                              });
                });
    }
}