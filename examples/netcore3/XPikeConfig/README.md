# Using XPike Configuration

## Create a Configuration POCO

You'll want to create a custom object to represent your configuration settings in most cases.
You can create as many of these as you'd like, to separate settings for different areas of your application.
Normally, this would be located in the same project as the business logic which consumes it.

In this example, this is the `SomeConfig` class in the `Example.Library` project:

```csharp
public class SomeConfig
{
    public string Name { get; set; }
}
```

The library that defines your configuration POCO(s) does not need to reference any XPike packages.

## Loading a Configuration POCO

You can load your configuration settings into the POCO in one of two ways - either by using the `IConfigurationService` or through an injection wrapper, `IConfig<T>`.

```csharp
using XPike.Configuration;

public class MyController : ApiController
{
    private readonly IConfigurationService _configService;
    private readonly IConfig<SomeConfig> _config;

    public MyController(IConfigurationService configService, IConfig<SomeConfig> config)
    {
        _configService = configService;
        _config = config;
    }

    [HttpGet]
    public IActionResult GetNameFromIConfig() =>
        Ok(_config.Name);

    [HttpGet]
    public IActionResult GetNameManually() =>
        Ok(_configService.GetValue<SomeConfig>("Example.Library.SomeConfig").Name);
}
```

A few things to note:
- When using `IConfig<T>`, you are obtaining a snapshot of those values from a point in time.
- Because of this, the `IConfigLoader` which enables this is configured by default to be a **transient** injection.
- The default key-naming is `$"{GetType().FullName}::{Field}"`.  Some providers will re-write keys for compatibility.
  - For example, when loading from Microsoft's `IConfiguration`, `'.'` and `'::'` will be changed to `':'`.
- There are ways to customize how the loader works for a specific configuration POCO, but by default it will throw an exception if deserialization fails.

## Read Settings Directly

You can inject the `IConfigurationService` anywhere in your application, and read settings directly.
Based on the provider used, this may come from configuration which was pre-loaded at startup, or may be loaded on-demand at runtime.

```csharp
using XPike.Configuration;

public class MyController : ApiController
{
    private readonly IConfigurationService _configService;

    public MyController(IConfigurationService configService)
    {
        _configService = configService;
    }

    [HttpGet]
    public IActionResult GetName() =>
        Ok(_configuration.GetValueOrDefault("Example.Library::PromotionEndDate", DateTime.Now);
}
```

You should always try to specify a reasonable default value in case a setting is not specified.
For situations where you have a required setting and this is not possible, there is an overload which will throw an exception when no setting is found:

```csharp
    _configService.GetValue<string>("Example.Library::AnotherConfig");
```

## ASP.NET Compatibility

### Bi-Directional Compatibility

This is the recommended approach to ensure consistent values are returned throughout your application, regardless of if `IConfiguration` or `IConfigurationService` is being used.

XPike Configuration Providers are loaded as providers to Microsoft's `IConfiguration`.
And Microsoft's `IConfiguration` is used as the provider for XPike's `IConfigurationService`.

You will need to add a call to `AddXPikeConfiguration()` in `Program.cs`:

```csharp
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
```

The above example is when using ASP.NET Core 3.
If you're using an earlier version of the framework, add the call directly to the `IWebHostBuilder` instead,
just after the call to `CreateDefaultBuilder()`.

### Microsoft -> XPike

TODO

### XPike -> Microsoft

TODO

### Isolated Mode

TODO