# Change Log

## [1.2.1]

- `MicrosoftConfigurationProvider` properly supports `List`, `IList`, and `IEnumerable` deserialization from an array

## [1.2.0]

- `ConfigurationProviderBase` now has an overridable `Deserialize<T>()` method
- `MicrosoftConfigurationProvider` properly supports `Array` deserialization
- Added tests for `NetCoreDictionaryToArrayJsonConverter`, the deserializer used by `MicrosoftConfigurationProvider`
- Added `StaticConfig<T>`, which does not rely on an `IConfigurationService` instance, intended to allow an `IConfig<T>` to be injected from a specific instance such as a hard-coded POCO instantiation

## [1.1.6]

- `MicrosoftConfigurationProvider` now properly supports object deserialization
- Added tests for object deserialization
- When deserializing an object whose key is missing, a default value is now returned if provided, or an `InvalidConfigurationException` is now thrown instead of returning null
- Added support in `ConfigurationServiceBase` for configuration middleware applied after DI container construction via `IDependencyProvider.AddConfigurationPipe<IConfigurationPipe>()` and `IConfigurationService.AddToPipeline(IConfigurationPipe)`

## [1.1.1]

- Changed `IConfig<T>` to be a singleton injection by default.
- Fixed up example project to `await` on calls which are now async.

## [1.1.0]

Added async overloads and related adjustments for performance.

## [1.0.0]

Inital release.

Basic support for:

- System.Configuration (app.config | web.config)
- Microsoft Platform Extensions Configuration System (Microsoft.Extensions.Configuration)
- Azure Application Configuration Service (https://azure.microsoft.com/en-us/services/app-configuration/)
  - WARNING! Microsoft's SDK for this is in Preview and may introduce breaking changes in future versions.
    When Microsoft releases a stable RTM, we will release a new version targeting the stable SDK.
- Amazon Simple System Management Parameter Store (https://docs.aws.amazon.com/systems-manager/latest/userguide/systems-manager-parameter-store.html)
