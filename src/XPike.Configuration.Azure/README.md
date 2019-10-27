# XPike.Configuration.Azure

XPike Configuration Provider for Azure App Configuration service.

This implementation uses the `Azure.ApplicationModel.Configuration` library from NuGet,
which is a cross-platform-compatible SDK, so it will work on ASP.NET Core or the .NET Framework.

This version *does not*, however, support some of the more advanced integration capabilities like configuration builders.

## Exported Services

- **`IAzureConfigurationProvider`**  
  **=> `AzureConfigurationProvider`**

## Usage

```
container.LoadPackage(new XPike.Configuration.Azure.Package(connectionString));
```

## Resources

[Quickstart: Create an ASP.NET Core app with Azure App Configuration](https://docs.microsoft.com/en-us/azure/azure-app-configuration/quickstart-aspnet-core-app)

