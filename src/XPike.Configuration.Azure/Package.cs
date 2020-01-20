using XPike.IoC;

namespace XPike.Configuration.Azure
{
    /// <summary>
    /// XPike Configuration - Azure App Configuration Services Provider
    /// 
    /// Package Dependencies:
    /// - XPike.Configuration
    /// 
    /// Singleton Registrations:
    /// - IAzureConfigurationProvider => AzureConfigurationProvider 
    /// 
    /// Collection Registrations:
    /// - IConfigurationProvider += IAzureConfigurationProvider
    /// </summary>
    public class Package
        : IDependencyPackage
    {
        private readonly string _connectionString;

        public Package(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void RegisterPackage(IDependencyCollection dependencyCollection)
        {
            dependencyCollection.LoadPackage(new XPike.Configuration.Package());

            dependencyCollection.RegisterSingleton<IAzureConfigurationProvider>(new AzureConfigurationProvider(_connectionString));
            dependencyCollection.AddSingletonToCollection<IConfigurationProvider, IAzureConfigurationProvider>(provider => provider.ResolveDependency<IAzureConfigurationProvider>());
        }
    }
}