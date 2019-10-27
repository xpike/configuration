# XPike.Configuration.Microsoft

Allows XPike Configuration to use Microsoft.Extensions.Configuration to acquire its settings from an <code>appsettings.json</code> file.

## Dependency Package

### Singleton Registrations

- **IMicrosoftConfigurationProvider**  
  => **MicrosoftConfigurationProvider**  
  This provides settings access via an injected <code>IConfiguration</code> object.

  ***Note:*** This is added to the collection of registered `IConfigurationProviders` **by default** when using this library's Dependency `Package`.

- **IXPikeConfigurationSource**  
  => **XPikeConfigurationSource**  
  This is a bridge that allows XPike Configuration to be used as a source for `Microsoft.Extensions.Configuration`.

### Other Mappings

- **IList\<IConfigurationProvider\>**  
  Adds: **IMicrosoftConfigurationProvider**  
  This is done by default, to allow `Microsoft.Extensions.Configuration` to be used as a source for XPike Configuration.

## Interfaces

### For XPike

- **IMicrosoftConfigurationProvider**  
  Implements `XPike.Configuration.IConfigurationProvider`.  
  
  Allows `Microsoft.Extensions.Configuration` to be used as a source for XPike Configuration.  

### For ASP.NET Core

- **IXPikeConfigurationSource**  
  Implements `Microsoft.Extensions.Configuration.IConfigurationSource` and `Microsoft.Extensions.Configuration.IConfigurationProvider`.  
  
  Allows XPike Configuration to be used as a source for `Microsoft.Extensions.Configuration`.

## Recommended Usage

### .NET Framework

This will work - but we do not yet have examples or a recommended approach for settting things up.

### ASP.NET Core

It's recommended that you configure your <code>IConfiguration</code> following ASP.NET Core conventions.

Then, use the <code>XPike.IoC.MicrosoftExtensions</code> package to have XPike IoC
use <code>Microsoft.Extensions.DependencyInjection</code>.

Alternatively, you can do the opposite by using the `IXPikeConfigurationSource` as a provider by registering it with the `IHostBuilder` following ASP.NET Core conventions.

## Dependencies

- XPike.IoC
- XPike.Configuration
