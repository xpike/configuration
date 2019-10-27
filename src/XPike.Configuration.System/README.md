# XPike.Configuration.System

Allows XPike Configuration to use settings from a <code>web.config</code> / <code>app.config</code> file.

When using <code>GetValue\<T\>()</code> or <code>GetValueOrDefault\<T\>()</code> (as opposed to the overloads which return <code>string</code>), a JSON-serialized value is expected.

## Exported Services

- **ISystemConfigurationProvider**  
  Uses System.Configuration.ConfigurationManager to retrieve configuration values.  
  
  ***Note:*** This is added to the collection of registered `IConfigurationProviders` **by default** when using this library's Dependency `Package`.

## Recommended Usage

### .NET Framework

When running under .NET Framework, this just works.

### ASP.NET Core

It is recommended that the <code>.config</code> file be stripped down to contain only the <code>\<appSettings\></code> element and its child elements within the top-level <code>\<configuration\></code> element.

## Dependencies

- XPike.IoC
- XPike.Configuration