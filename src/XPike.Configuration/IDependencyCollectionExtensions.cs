using System;
using XPike.IoC;

namespace XPike.Configuration
{
    public static class IDependencyCollectionExtensions
    {
        // This is intended to be called from an application's root-level ConfigurationPackage.cs
        // In .NET Core this is loaded from within Startup.cs in the setup callback for AddXPikeDependencyInection()
        public static IDependencyCollection AddXPikeConfiguration(this IDependencyCollection dependencyCollection, Action<IXPikeConfigBuilder> configBuilderSetup)
        {
            dependencyCollection.LoadPackage(new XPike.Configuration.Package());

            var builder = new XPikeConfigBuilder();
            configBuilderSetup(builder);

            dependencyCollection.RegisterSingleton<IConfigurationProvider>(builder.Build());

            return dependencyCollection;
        }
    }
}