# Change Log

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
