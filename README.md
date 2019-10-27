# XPike.Configuration

[![Build Status](https://dev.azure.com/xpike/xpike/_apis/build/status/xpike-configuration?branchName=master)](https://dev.azure.com/xpike/xpike/_build/latest?definitionId=3&branchName=master)
![Nuget](https://img.shields.io/nuget/v/XPike.Configuration)

Provides basic application configuration support for XPike.

XPike Configuration is intended for use before Dependency Injection is configured, to provide some basic bootstrap settings.
Because of this, *Configuration Providers should not require injected dependencies.*

Providers are wrapped by the **Configuration Service** which scans them in reverse order, returning the first value it finds.
While Providers can be used directly, the Configuration Service should be your go-to (and it is compatible with .NET Core `IConfiguration`).

Though the Configuration Service does not rely on DI, it is intended to be registered with the container along with any Providers.
This is because XPike Settings (which adds capabilities such as caching and refresh - similar to .NET Core `IOptions`) relys on the Configuration Service as its default Settings Provider. 

For application-oriented settings, it is recommended to use the `XPike.Settings` Package to retrieve a settings POCO.

## Setup

### ASP.NET Core



### .NET Framework


## Exported Services

- **IConfigurationService**  
  => **ConfigurationService**  
  This is the primary interface that should be injected into business logic libraries.
  Allows the use of multiple IConfigurationProvider implementations, with a
  tiered/failover approach.

- **IEnvironmentConfigurationProvider**  
  => **EnvironmentConfigurationProvider**  
  Acquires settings from environment variables using `System.Environment`.  
  
  ***Note:*** This is added to the collection of providers **by default** when using this library's Dependency `Package`.  To remove it, call `IDependencyCollection.ResetCollection<IConfigurationProvider>()` and then register your desired providers.

- **IMemoryConfigurationProvider**  
  => **MemoryConfigurationProvider**  
  Acquires settings from a pre-defined <code>IDictionary\<string, string\></code>.

- **INullConfigurationProvider**  
  => **NullConfigurationProvider**  
  For testing purposes.  Returns null, or any specified default value (when applicable).

## Interfaces

### IConfigurationProvider

**NOTE:** Implementations of this interface must be registered using Collection Registration via `IDependencyContainer.AddSingleton<TService>()`.

- **`string GetValue(string key)`**  
  Throws an `InvalidConfigurationException` if the specified key is not found.

- **`string GetValueOrDefault(string key, string defaultValue = null)`**  
  Returns the specified default value if the key is not found.

- **`T GetValue<T>(string key)`**  
  For intrinsic data types such as `int` and `bool`, uses `System.Convert` to cast the value.  
  For other data types, uses `JsonConvert` to deserialize from a JSON string.  ***This may vary by Provider, however.***  
  Throws an `InvalidConfigurationException` if the specified key is not found.

- **`T GetValue<T>(string key, T defaultValue = default)`  
  Same as above, but returns the specified default value if the key is not found.

### IConfigurationLoader

Some Providers may choose to implement this interface, which enables bulk-loading of configuration.

- **`IDictionary<string, string> Load()`**  
  Returns the complete set of key-value pairs that a Provider can access.

This is intended for compatibility with ASP.NET Core, so that `Microsoft.Extensions.Configuration` can use XPike Configuration as its source for `IOptions<>`.

xPike offers a similar `ISettings<>` which will work with any and all Configuration Providers,
though all of the implementations xPike provides by default are .NET Core compatible.

### IConfigurationService

This is the composite service that should be used to retrieve configuration.

This interface implements both `IConfigurationProvider` and `IConfigurationLoader`,
proxying to the underlying Providers and selecting the value from the highest-priority provider.

Note that calls to its `IConfigurationLoader.Load()` method will only return values from Providers that also implement `IConfigurationLoader`.

## Base Classes

### ConfigurationProviderBase

An abstract base class which adds support for deserialization and error handling, reducing
the surface area that each Provider needs to implement.

At a minimum, the <code>GetValueOrDefault(string key, string defaultValue = null)</code> must
be overridden to implement a new Provider.

## Exceptions

### InvalidConfigurationException

Thrown when a setting can not be found.  An informational message will be applied.
It may also wrap an underlying <code>Exception</code> when present.

- <code>string Key { get; }</code>  
  Specifies the Configuration Key which was not able to be found.

## Dependencies

- XPike.IoC

## Building and Testing

Building from source and running unit tests requires a Windows machine with:

* .Net Core 3.0 SDK
* .Net Framework 4.6.1 Developer Pack

## Issues

Issues are tracked on [GitHub](https://github.com/xpike/xpike-ioc/issues). Anyone is welcome to file a bug,
an enhancement request, or ask a general question. We ask that bug reports include:

1. A detailed description of the problem
2. Steps to reproduce
3. Expected results
4. Actual results
5. Version of the package xPike
6. Version of the .Net runtime

## Contributing

See our [contributing guidelines](https://github.com/xpike/documentation/blob/master/docfx_project/articles/contributing.md)
in our documentation for information on how to contribute to xPike.

## License

xPike is licensed under the [MIT License](LICENSE).
