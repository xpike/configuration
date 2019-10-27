using System;
using XPike.IoC;

namespace XPike.Configuration.AWS
{
    public class Package
        : IDependencyPackage
    {
        public void RegisterPackage(IDependencyCollection dependencyCollection)
        {
            dependencyCollection.LoadPackage(new Configuration.Package());

            dependencyCollection.RegisterSingleton<IAWSConfigurationProvider, AWSConfigurationProvider>();

            dependencyCollection.AddSingletonToCollection<IConfigurationProvider, IAWSConfigurationProvider>(services =>
                services.ResolveDependency<IAWSConfigurationProvider>());
        }
    }
}
