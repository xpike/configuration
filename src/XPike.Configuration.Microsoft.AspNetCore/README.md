# XPike.Configuration.Microsoft.AspNetCore

Provides inter-operability between XPike Configuration and
Microsoft.Extensions.Configuration.IConfiguration.

***Note:*** *Only one of the below extension methods should be used in any given application, to avoid a circular reference.*

## Dependency Package

### Singleton Registrations

- **IXPikeConfigurationSource**  
  => **XPikeConfigurationSource**  

## Extension Methods

### IConfigurationBuilder

- **`AddXPikeConfiguration(IConfigurationService)`**  
  Registers XPike Configuration as a Configuration Source to provide its values to Microsoft.Extensions.Configuration.IConfiguration.

### IServiceCollection

- **`UseMicrosoftConfigurationForXPike()`**  
  Registers Microsoft.Extensions.Configuration to provide its values to XPike Configuration.

## Dependencies

- XPike.IoC
- XPike.Configuration
- XPike.Configuration.Microsoft
